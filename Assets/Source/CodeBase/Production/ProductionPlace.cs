using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source.CodeBase.Production
{
    public class ProductionPlace : MonoBehaviour
    {
        [SerializeField] private List<ProductPlace> _productPoints;
        [SerializeField] private ProductType _productType;
        [SerializeField] private float _createTime;

        private ProductFactory _factory;
        private Coroutine _createCoroutine;

        public ProductType Type => _productType;
        public event Action ReadyToCreateProduct;

        public void Construct(ProductFactory factory)
        {
            _factory = factory;
            ReadyToCreateProduct?.Invoke();
        }

        public Product GetProduct()
        {         
            foreach(ProductPlace point in _productPoints)
            {
                if(point.IsEmpty == false)
                {
                    Product product = point.GetProduct();
                    CreateProduct();

                    return product;
                }
            }

            return null;
        }

        public Product CreateAndGetProduct() => 
            _factory.Create(_productType);

        public void CreateProduct()
        {
            ProductPlace point = GetFreePoint();

            if (point != null)
                StartCreateCoroutine(point);
        }

        private ProductPlace GetFreePoint() =>
            _productPoints.FirstOrDefault(x => x.IsEmpty);

        private void StartCreateCoroutine(ProductPlace point)
        {
            if(_createCoroutine == null)
                _createCoroutine = StartCoroutine(OnCreateProduct(point));
        }


        private IEnumerator OnCreateProduct(ProductPlace point)
        {
            yield return new WaitForSeconds(_createTime);
            Product product = _factory.Create(_productType);
            product.transform.position = transform.position - Vector3.up;

            Tween moveTween = product.transform.DOMove(point.transform.position, 1);

            yield return moveTween.WaitForCompletion();

            point.AddProduct(product);
            
            _createCoroutine = null;
            ReadyToCreateProduct?.Invoke();
        }
    }
}
