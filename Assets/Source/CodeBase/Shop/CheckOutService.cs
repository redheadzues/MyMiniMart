using Assets.Source.CodeBase.Customers;
using Assets.Source.CodeBase.Production;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.CodeBase.Shop
{
    public class CheckOutService : MonoBehaviour
    {
        [SerializeField] private List<CustomerPlace> _queuePlaces;
        [SerializeField] private ProductPlace _packingPlace;
        [SerializeField] private CashZone _cashZone;
        [SerializeField] private ProductionPlace _productionPlace;


        private CustomerQueue _customerQueue;
        private bool _canServe;
        private Coroutine _serveCoroutine;

        public CustomerQueue Queue => _customerQueue;

        private void OnEnable() => 
            _queuePlaces[0].CanServiced += ServeCustomer;

        private void OnDisable() => 
            _queuePlaces[0].CanServiced -= ServeCustomer;

        private void Start() => 
            _customerQueue = new CustomerQueue(_queuePlaces);

        public void SetServeState(bool canServe)
        {
            _canServe = canServe;
            ServeCustomer();
        }

        private void PushQueue()
        {
            for(int i = 1; i < _queuePlaces.Count; i++)
            {
                if (_queuePlaces[i].IsEmpty == false)
                {
                    _queuePlaces[i - 1].SetCustomer(_queuePlaces[i].Customer);
                    _queuePlaces[i].Customer.TakePlaceInQueue(_queuePlaces[i - 1]);
                }
                else
                {
                    Customer customer = _customerQueue.GetCustomer();

                    if(customer != null)
                    {
                        customer.TakePlaceInQueue(_queuePlaces[i - 1]);
                        _queuePlaces[i - 1].SetCustomer(customer);
                    }
                }

            }
        }

        private void ServeCustomer()
        {
            if (_queuePlaces[0].IsEmpty || _canServe == false)
                return;

            if (_serveCoroutine != null)
                StopCoroutine(_serveCoroutine);

            _serveCoroutine = StartCoroutine(OnServe());
        }

        private IEnumerator OnServe()
        {
            while (_queuePlaces[0].Customer.Cart.ProductCount > 0)
            {
                Product product = _queuePlaces[0].Customer.Cart.GetProduct();
                Tween packingTween = product.transform.DOMove(_packingPlace.transform.position, 0.2f);

                yield return packingTween.WaitForCompletion();

                for(int i = 0; i < product.Price; i++)
                {
                    Product money = _productionPlace.CreateAndGetProduct();
                    _cashZone.TakeMoney(money);
                }

                Destroy(product.gameObject);
            }

            Customer customer = _queuePlaces[0].Customer;
            _queuePlaces[0].FreePlace();
            Destroy(customer.gameObject);

            PushQueue();
        }

    }
}
