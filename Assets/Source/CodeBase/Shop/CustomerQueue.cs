using Assets.Source.CodeBase.Customers;
using System.Collections.Generic;

namespace Assets.Source.CodeBase.Shop
{
    public class CustomerQueue
    {
        private List<CustomerPlace> _queuePlaces;
        private Queue<Customer> _virtualQueue;

        public CustomerQueue(List<CustomerPlace> queuePlaces)
        {
            _queuePlaces = queuePlaces;
            _virtualQueue = new Queue<Customer>();
        }

        public Customer GetCustomer() =>
            _virtualQueue.Count > 0 ? _virtualQueue.Dequeue() : null;

        public bool HasFreePlace()
        {
            foreach (CustomerPlace place in _queuePlaces)
            {
                if (place.IsEmpty)
                    return true;
            }

            return false;
        }

        public void TakePlace(Customer customer)
        {
            CustomerPlace place = FindFreePlace();

            if (place != null)
            {
                place.SetCustomer(customer);
                customer.TakePlaceInQueue(place);
            }
            else
                _virtualQueue.Enqueue(customer);
        }

        private CustomerPlace FindFreePlace()
        {
            foreach(CustomerPlace place in _queuePlaces)
            {
                if (place.IsEmpty)
                    return place;
            }

            return null;
        }
    }
}
