using Assets.Source.CodeBase.Customers;
using Assets.Source.CodeBase.Infrustructure.SceneData;
using Assets.Source.CodeBase.Production;
using Assets.Source.CodeBase.Shop;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source.CodeBase.Infrustructure
{
    public class AssetProvider
    {
        private const string ShelfPath = "Shelfs";
        private const string ProductionPlacePath = "ProductionPlaces";
        private const string ProductsPath = "Products";
        private const string CustomersPath = "Customers";
        private const string BuildDataPath = "StaticData/BuildData";
        private const string BuyZone = "BuyZone";

        private Dictionary<ProductType, Shelf> _shelfs;
        private Dictionary<ProductType, ProductionPlace> _productionPlaces;
        private Dictionary<ProductType, Product> _products;
        private List<Customer> _customers;
        private BuildStaticData _buildData;
        private BuyZone _buyZone;

        public void LoadAssets()
        {
            _shelfs = Resources.LoadAll<Shelf>(ShelfPath).ToDictionary(x => x.Type, x => x);
            _productionPlaces = Resources.LoadAll<ProductionPlace>(ProductionPlacePath).ToDictionary(x => x.Type, x => x);
            _products = Resources.LoadAll<Product>(ProductsPath).ToDictionary(x => x.Type, x => x);
            _customers = Resources.LoadAll<Customer>(CustomersPath).ToList();
            _buildData = Resources.Load<BuildStaticData>(BuildDataPath);
            _buyZone = Resources.Load<BuyZone>(BuyZone);
        }

        public BuyZone GetBuyZone() =>
            _buyZone;

        public Shelf GetShelf(ProductType type) =>
            _shelfs.TryGetValue(type, out Shelf shelf) ? shelf : null;

        public ProductionPlace GetProductPlace(ProductType type) =>
            _productionPlaces.TryGetValue(type, out ProductionPlace place) ? place : null;

        public Product GetProduct(ProductType type) =>
            _products.TryGetValue(type, out Product product) ? product : null;

        public Customer GetRandomCustomer() =>
            _customers[Random.Range(0, _customers.Count)];

        public BuildStaticData GetBuildStaticData() =>
            _buildData;
    }
}
