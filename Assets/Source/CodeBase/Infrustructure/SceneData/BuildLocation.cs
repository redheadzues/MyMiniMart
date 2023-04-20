using Assets.Source.CodeBase.Production;
using UnityEngine;

namespace Assets.Source.CodeBase.Infrustructure.SceneData
{
    public class BuildLocation : MonoBehaviour
    {
        [SerializeField] private ProductLocationType _locationType;
        [SerializeField] private ProductType _productType;
        [SerializeField] private bool _isOwned;
        [SerializeField] private int _price;

        public ProductLocationType LocationType => _locationType;
        public ProductType ProductType => _productType;
        public bool IsOwned => _isOwned;
        public int Price => _price;
    }

}
