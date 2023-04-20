using Assets.Source.CodeBase.Production;
using System;
using UnityEngine;

namespace Assets.Source.CodeBase.Infrustructure.SceneData
{
    [Serializable]
    public class LocationStaticData
    {
        public ProductLocationType LocationType;
        public ProductType ProductType;
        public bool IsOwned;
        public int Price;
        public Vector3 Position;

        public LocationStaticData(ProductLocationType locationType, ProductType productType, bool isOwned, Vector3 position, int price)
        {
            LocationType = locationType;
            ProductType = productType;
            IsOwned = isOwned;
            Position = position;
            Price = price;
        }
    }

}
