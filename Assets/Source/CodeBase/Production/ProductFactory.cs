using Assets.Source.CodeBase.Infrustructure;
using UnityEngine;

namespace Assets.Source.CodeBase.Production
{
    public class ProductFactory
    {
        private AssetProvider _provider;

        public ProductFactory(AssetProvider provider)
        {
            _provider = provider;
        }

        public Product Create(ProductType type)
        {
            Product product = _provider.GetProduct(type);
            return Object.Instantiate(product);
        }
    }
}
