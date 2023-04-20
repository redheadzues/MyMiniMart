using System.Collections.Generic;

namespace Assets.Source.CodeBase.Customers
{
    public class CustomerOrders
    {
        private Queue<Order> _orders;

        public CustomerOrders(Queue<Order> orders)
        {
            _orders = orders;
        }

        public Order GetOrder()
        {
            if( _orders.Count > 0 )
                return _orders.Dequeue();
            else
                return null;
        }
    }
}
