using UnityEngine;

namespace Assets.Source.CodeBase.Production
{
    public class AutomaticProductProcessor : MonoBehaviour
    {
        [SerializeField] private ProductionPlace _productionPlace;

        private void OnEnable() => 
            _productionPlace.ReadyToCreateProduct += OnReadyToCreate;

        private void OnDisable() => 
            _productionPlace.ReadyToCreateProduct -= OnReadyToCreate;

        private void OnReadyToCreate() => 
            _productionPlace.CreateProduct();
    }
}
