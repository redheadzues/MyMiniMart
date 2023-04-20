using Assets.Source.CodeBase.Production;
using Assets.Source.CodeBase.Shop;
using UnityEngine;

namespace Assets.Source.CodeBase.Customers
{
    public class Customer : MonoBehaviour
    {
        [SerializeField] public CustomerMover _mover;
        [SerializeField] private CustomerCart _cart;

        private Store _store;
        private CustomerOrders _orders;
        private CustomerPlace _currentPlace;
        private Order _currentOrder;

        public Order CurrentOrder => _currentOrder;
        public CustomerCart Cart => _cart;

        public void StartShopping(CustomerOrders orders, Store store)
        {
            _store = store;
            _orders = orders;
            ProcessOrders();
        }        

        private void ProcessOrders()
        {
            Order order = _orders.GetOrder();

            if(order != null)
            {
                _currentOrder = order;
                _currentOrder.Completed += OnOrderCompleted;
                _currentOrder.ProductGetted += OnProductGetted;
                Shelf shelf = _store.GetShelf(order.ProductType);
                shelf.Server.Queue.TakePlace(this);
            }
            else
            {
                CheckOutService checkOut = _store.GetCheckOutService();
                checkOut.Queue.TakePlace(this);
            }
        }


        public void TakePlaceInQueue(CustomerPlace place)
        {
            if(_currentPlace != null)
                _currentPlace.FreePlace();

            _currentPlace = place;
            _mover.Move(_currentPlace.transform.position);
        }

        private void OnProductGetted(Product product) => 
            _cart.AddProduct(product);

        private void OnOrderCompleted(Order order)
        {
            order.Completed -= OnOrderCompleted;
            order.ProductGetted -= OnProductGetted;
            ProcessOrders();
        }
    }
}
