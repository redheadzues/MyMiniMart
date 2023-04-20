using Assets.Source.CodeBase.Customers;
using Assets.Source.CodeBase.Production;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.CodeBase.Shop
{
    public class ShelfServer : MonoBehaviour
    {
        [SerializeField] private List<CustomerPlace> _queuePlaces;
        [SerializeField] private Shelf _shelf;

        private Customer _customer;
        private CustomerQueue _customerQueue;

        public CustomerQueue Queue => _customerQueue;

        private void Awake() => 
            _customerQueue = new CustomerQueue(_queuePlaces);

        private void OnEnable()
        {
            foreach(CustomerPlace place in _queuePlaces)
            {
                place.CanServiced += ServeOrders;
                place.Released += OnPlaceReleased;  
            }

            _shelf.Updated += ServeOrders;
        }

        private void OnDisable()
        {
            foreach (CustomerPlace place in _queuePlaces)
            {
                place.CanServiced -= ServeOrders;
                place.Released -= OnPlaceReleased;
            }

            _shelf.Updated -= ServeOrders;
        }


        private void ServeOrders()
        {
            while(ServeCustomer())
            {
            }
        }

        private bool ServeCustomer()
        {
            bool isServed = false;

            foreach (CustomerPlace place in _queuePlaces)
            {
                if(place.ReadyToService)
                {
                    Product product = _shelf.GetProduct();

                    if (product != null)
                    {
                        place.Customer.CurrentOrder.TakeProduct(product);
                        isServed = true;
                    }
                    else
                        return false;
                }
            }

            return isServed;

        }

        private void OnPlaceReleased(CustomerPlace place)
        {
            _customer = _customerQueue.GetCustomer();

            if (_customer != null)
            {
                _customer.TakePlaceInQueue(place);
                place.SetCustomer(_customer);
            }
        }
    }
}
