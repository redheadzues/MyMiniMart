using Assets.Source.CodeBase.Customers;
using System;
using UnityEngine;

namespace Assets.Source.CodeBase.Shop
{
    public class CustomerPlace : MonoBehaviour
    {
        private Customer _customer;
        private bool _readyToService;

        public Customer Customer => _customer;
        public bool IsEmpty => _customer == null;
        public bool ReadyToService => _readyToService;
        public event Action<CustomerPlace> Released;
        public event Action CanServiced;


        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Customer customer))
            {
                if (customer == _customer)
                {
                    _readyToService = true;
                    CanServiced?.Invoke();
                }
            }
        }

        public void SetCustomer(Customer customer)
        {
            _customer = customer;
            _customer.CurrentOrder.Completed += OnOrderCompleted;
        }

        private void OnOrderCompleted(Order order)
        {
            _readyToService = false;
            order.Completed -= OnOrderCompleted;    
        }

        public void FreePlace()
        {
            _customer = null;
            Released?.Invoke(this);
        }
    }
}
