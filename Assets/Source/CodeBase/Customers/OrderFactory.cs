using Assets.Source.CodeBase.Production;
using Assets.Source.CodeBase.Shop;
using System;
using System.Collections.Generic;

namespace Assets.Source.CodeBase.Customers
{
    public class OrderFactory
    {
        private readonly Store _store;
        private readonly int _maxOrders;
        private readonly int _maxProductCountInOrder;

        private List<ProductType> _includedProducts = new List<ProductType>();

        public OrderFactory(Store store, int maxOrders, int maxProductCountInOrder)
        {
            _store = store;
            _maxOrders = maxOrders;
            _maxProductCountInOrder = maxProductCountInOrder;
        }

        public Queue<Order> CreateQueueOrders()
        {
            _includedProducts.Clear();
            int maxOrdersCount = Math.Min(_store.AvailableProducts.Count, _maxOrders);
            int ordersCount = UnityEngine.Random.Range(1, maxOrdersCount+1);
            int currentOrdersCount = 0;


            Queue<Order> orders = new Queue<Order>();

            while(currentOrdersCount < ordersCount)
            {
                int productIndex = UnityEngine.Random.Range(0, _store.AvailableProducts.Count);

                if (_includedProducts.Contains(_store.AvailableProducts[productIndex]) == false)
                {
                    _includedProducts.Add(_store.AvailableProducts[productIndex]);
                    int productCount = UnityEngine.Random.Range(1, _maxProductCountInOrder + 1);

                    Order order = new Order(_store.AvailableProducts[productIndex], productCount);
                    orders.Enqueue(order);
                    currentOrdersCount++;
                }
            }

            return orders;
        }
    }
}
