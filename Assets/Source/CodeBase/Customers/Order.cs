using Assets.Source.CodeBase.Production;
using System;

namespace Assets.Source.CodeBase.Customers
{
    public class Order
    {
        private readonly ProductType _type;
        private readonly int _requiredCount;
        private int _currentCount;

        public ProductType ProductType => _type;
        public bool IsComplete => _currentCount == _requiredCount;
        public int CurrentRequiredCount => _requiredCount - _currentCount;
        public event Action<Product> ProductGetted;
        public event Action<Order> Completed; 

        public Order(ProductType type, int requiredCount)
        {
            _type = type;
            _requiredCount = requiredCount;
        }

        public void TakeProduct(Product product)
        {
            if(product.Type == _type && IsComplete == false)
            {
                _currentCount++;
                ProductGetted?.Invoke(product);
            }
 

            if (IsComplete)
                Completed?.Invoke(this);
        }
    }
}
