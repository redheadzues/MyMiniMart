using Assets.Source.CodeBase;
using Assets.Source.CodeBase.Infrustructure;
using Assets.Source.CodeBase.Infrustructure.SceneData;
using Assets.Source.CodeBase.Production;
using Assets.Source.CodeBase.Shop;
using UnityEngine;

public class GameBuildFactory
{
    private Store _store;
    private AssetProvider _provider;
    private ProductFactory _productFactory;

    public GameBuildFactory(Store store, AssetProvider provider, ProductFactory productFactory)
    {
        _store = store;
        _provider = provider;
        _productFactory = productFactory;
    }

    public void CreateBuild(LocationStaticData buildData)
    {
        switch (buildData.LocationType)
        {
            case ProductLocationType.Shelf:
                CreateShelf(buildData.ProductType, buildData.Position);
                    break;
            case ProductLocationType.ProductionPlace:
                CreateProductionPlace(buildData.ProductType, buildData.Position);
                break;
        }
    }

    public void CreateBuyZone(LocationStaticData buildData)
    {
        BuyZone buyzone = _provider.GetBuyZone();

        BuyZone createdBuyZone = Object.Instantiate(buyzone, buildData.Position, Quaternion.identity);

        createdBuyZone.Construct(this, buildData);
    }

    private void CreateShelf(ProductType type, Vector3 position)
    {
        Shelf shelf = _provider.GetShelf(type);

        Shelf createdShelf = Object.Instantiate(shelf, position, Quaternion.identity);

        _store.AddShelf(createdShelf);
    }

    private void CreateProductionPlace(ProductType type, Vector3 position)
    {
        ProductionPlace place = _provider.GetProductPlace(type);

        ProductionPlace createdPlace = Object.Instantiate(place, position, Quaternion.identity);
        
        createdPlace.Construct(_productFactory);

        if (createdPlace.TryGetComponent(out CheckOutService checkout))
            _store.AddCheckOutService(checkout);
    }
}
