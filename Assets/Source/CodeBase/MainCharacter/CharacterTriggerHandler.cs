using Assets.Source.CodeBase.Production;
using Assets.Source.CodeBase.Shop;
using System.Collections;
using UnityEngine;

namespace Assets.Source.CodeBase.MainCharacter
{
    public class CharacterTriggerHandler : MonoBehaviour
    {
        [SerializeField] private GameStack _stack;
        [SerializeField] private Wallet _wallet;
        [SerializeField] private int _spendMoneyPerFrame;
        
        private Coroutine _takeMoneyCoroutine;
        private Coroutine _investCoroutine;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ProductionPlace productionPlace))
                AddProductsInStack(productionPlace);

            if(other.TryGetComponent(out Shelf shelf))
                UnloadProductOnShelf(shelf);

            if (other.TryGetComponent(out CheckOutService checkOut))
                checkOut.SetServeState(true);

            if (other.TryGetComponent(out BuyZone buyZone))
                StartInvest(buyZone);

            if (other.TryGetComponent(out CashZone cashZone))
                TakeMoney(cashZone);
        }



        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out CheckOutService checkOut))
                checkOut.SetServeState(false);

            if (other.TryGetComponent(out BuyZone buyZone))
                StopInvest();
        }

        private void AddProductsInStack(ProductionPlace productionPlace)
        {
            int freePlaces = _stack.FreePlaces;

            for (int i = 0; i < freePlaces; i++)
            {
                Product product = productionPlace.GetProduct();

                if (product != null)
                    _stack.AddProduct(product);
                else
                    return;
            }
        }

        private void UnloadProductOnShelf(Shelf shelf)
        {
            int freePlaces = shelf.GetFreePlaceCount();

            for(int i = 0; i < freePlaces; i++)
            {
                Product product = _stack.GetProduct(shelf.Type);

                if (product != null)
                    shelf.AddProduct(product);
                else
                    return;
            }
        }

        private void StartInvest(BuyZone buyZone)
        {
            StopInvest();

            _investCoroutine = StartCoroutine(OnInvest(buyZone));
        }

        private void StopInvest()
        {
            if (_investCoroutine != null)
                StopCoroutine(_investCoroutine);
        }

        private IEnumerator OnInvest(BuyZone buyZone)
        {
            while(buyZone.RemainingInvest > 0 && _wallet.TrySpendMoney(_spendMoneyPerFrame))
            {
                buyZone.Invest(_spendMoneyPerFrame);
                yield return null;
            }
        }

        private IEnumerator OnTakeMoney(CashZone cashZone)
        {
            while(true)
            {
                Product money = cashZone.GetMoney();

                if (money != null)
                    _wallet.TakeMoney(money);
                else
                    break;

                yield return null;
            }
        }

        private void TakeMoney(CashZone cashZone)
        {
            StopTakeMoney();

            _takeMoneyCoroutine = StartCoroutine(OnTakeMoney(cashZone));
        }

        private void StopTakeMoney()
        {
            if (_takeMoneyCoroutine != null)
                StopCoroutine(_takeMoneyCoroutine);
        }
    }
}
