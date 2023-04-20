using Assets.Source.CodeBase.Production;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source.CodeBase.Customers
{
    public class CustomerCart : MonoBehaviour
    {
        [SerializeField] private List<ProductPlace> _productPlaces;

        private int _currentPlaceIndex;

        public int ProductCount => _productPlaces.Where(x => x.IsEmpty == false).ToList().Count;

        public void AddProduct(Product product)
        {
            if(_currentPlaceIndex < _productPlaces.Count - 1)
            {
                _productPlaces[_currentPlaceIndex].AddProduct(product);
                product.transform.SetParent(_productPlaces[_currentPlaceIndex].transform);
                product.transform.DOLocalMove(Vector3.zero, 0.2f);
                _currentPlaceIndex++;
            }
        }

        public Product GetProduct()
        {
            if(_currentPlaceIndex >= 0)
            {
                _currentPlaceIndex--;
                Product product = _productPlaces[_currentPlaceIndex].GetProduct();
                product.transform.SetParent(null);


                return product;
            }

            return null;
        }
    }
}
