using Assets.Source.CodeBase.Infrustructure;
using Assets.Source.CodeBase.Shop;
using Assets.Source.CodeBase.Ui;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.CodeBase.Customers
{
    public class CustomerFactory
    {
        private readonly OrderFactory _orderFactory;
        private readonly Transform _spawnPoint;
        private readonly Store _store;
        private readonly AssetProvider _provider;

        public CustomerFactory(OrderFactory orderFactory, Transform spawnPoint, Store store, AssetProvider provider)
        {
            _orderFactory = orderFactory;
            _spawnPoint = spawnPoint;
            _store = store;
            _provider = provider;
        }

        public void CreateCustomer()
        {
            Customer customer = _provider.GetRandomCustomer();
            Customer createdCustomer = Object.Instantiate(customer, _spawnPoint.position, Quaternion.identity);

            Queue<Order> orders = _orderFactory.CreateQueueOrders();
            CustomerOrders customerOrders = new CustomerOrders(orders);

            CustomerOrdersDisplayer displayer = createdCustomer.GetComponent<CustomerOrdersDisplayer>();


            foreach (Order order in orders)
                displayer.AddOrder(order);


            createdCustomer.StartShopping(customerOrders, _store);
        }

    }
}
