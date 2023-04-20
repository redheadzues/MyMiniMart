using Assets.Source.CodeBase.Customers;
using Assets.Source.CodeBase.Infrustructure.SceneData;
using Assets.Source.CodeBase.Production;
using Assets.Source.CodeBase.Shop;
using System.Collections;
using UnityEngine;

namespace Assets.Source.CodeBase.Infrustructure.Roots
{
    public class CompositeRoot : MonoBehaviour, ICorouteineRunner
    {
        [SerializeField] private Transform _spawnPoint;

        private AssetProvider _assetProvider;
        private Store _store;
        private ProductFactory _productFactory;

        private void Awake()
        {
            CreateAndLoadAssets();
            _productFactory = new ProductFactory(_assetProvider);
            CreateShopZone();
            CreateCustomerSystem();
        }

        private void CreateAndLoadAssets()
        {
            _assetProvider = new AssetProvider();
            _assetProvider.LoadAssets();
        }

        private void CreateShopZone()
        {
            _store = new Store();
            GameBuildFactory buildFactory = new GameBuildFactory(_store, _assetProvider, _productFactory);
            BuildStaticData buildData = _assetProvider.GetBuildStaticData();

            foreach (LocationStaticData build in buildData.LocationData)
            {
                if (build.IsOwned)
                    buildFactory.CreateBuild(build);
                else
                    buildFactory.CreateBuyZone(build);
            }
        }

        private void CreateCustomerSystem()
        {
            OrderFactory orderFactory = new OrderFactory(_store, 3, 4);
            CustomerFactory customerFactory = new CustomerFactory(orderFactory, _spawnPoint, _store, _assetProvider);
            CustomerSpawner spawner = new CustomerSpawner(5, customerFactory, this, _store);

        }
    }

    public interface ICorouteineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}