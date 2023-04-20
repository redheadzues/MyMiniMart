using Assets.Source.CodeBase.Production;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source.CodeBase.Shop
{
    public class Shelf : MonoBehaviour
    {
        [SerializeField] private List<ProductPlace> _productPoints;
        [SerializeField] private ProductType _productType;
        [SerializeField] private ShelfServer _server;

        public ProductType Type => _productType;
        public ShelfServer Server => _server;
        public event Action Updated;

        public int GetFreePlaceCount() =>
            _productPoints.Where(x => x.IsEmpty).Count();

        public bool IsProductAvailable => 
            _productPoints.FirstOrDefault(x => x.CanTake) == null ? false : true;

        public bool AddProduct(Product product)
        {
            if (product.Type != _productType)
                return false;

            ProductPlace place = _productPoints.FirstOrDefault(x => x.IsEmpty);

            if (place != null)
            {
                place.AddProduct(product);
                product.transform.DOMove(place.transform.position, 0.2f).OnComplete(() => TransferEnded(place));
                return true;
            }
            else
                return false;
        }

        public Product GetProduct()
        {
            ProductPlace point = _productPoints.LastOrDefault(x => x.IsEmpty == false && x.CanTake);

            return point != null ? point.GetProduct() : null;
        }

        private void TransferEnded(ProductPlace place)
        {
            place.ProductOnPlace();
            Updated?.Invoke();
        }
    }
}
