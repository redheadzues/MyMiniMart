using Assets.Source.CodeBase.Shop;
using System;
using UnityEngine;

namespace Assets.Source.CodeBase.Production
{
    public class ProductProcessor : MonoBehaviour
    {
        [SerializeField] private ProductionPlace _productionPlace;
        [SerializeField] private Shelf _shelf;

        private bool _canCreateProduct;

        private void OnEnable()
        {
            _productionPlace.ReadyToCreateProduct += OnReadyToCreate;
            _shelf.Updated += OnshelfUpdated;
        }

        private void OnDisable()
        {
            _productionPlace.ReadyToCreateProduct -= OnReadyToCreate;
            _shelf.Updated -= OnshelfUpdated;
        }

        private void OnshelfUpdated() => 
            CreateProduct();

        private void OnReadyToCreate()
        {
            _canCreateProduct = true;
            CreateProduct();
        }

        private void CreateProduct()
        {
            if (_shelf.IsProductAvailable && _canCreateProduct)
            {
                Product product = _shelf.GetProduct();
                Destroy(product.gameObject);

                _canCreateProduct = false;
                _productionPlace.CreateProduct();

            }

        }
    }
}
