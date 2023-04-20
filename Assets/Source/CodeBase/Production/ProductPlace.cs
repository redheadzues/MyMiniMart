using UnityEngine;

namespace Assets.Source.CodeBase.Production
{
    public class ProductPlace : MonoBehaviour
    {
        private Product _product;
        private bool _isEmpty = true;
        private bool _canTake = false;

        public bool IsEmpty => _isEmpty;
        public bool CanTake => _canTake;    


        public void AddProduct(Product product)
        {
            _product = product;
            _isEmpty = false;
        }

        public Product GetProduct()
        {
            Product product = _product;
            _product = null;
            _isEmpty = true;
            _canTake = false;

            return product;
        }

        public void ProductOnPlace()
        {
            _canTake = true;
        }
    }
}
