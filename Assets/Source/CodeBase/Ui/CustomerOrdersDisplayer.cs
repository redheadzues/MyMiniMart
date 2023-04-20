using Assets.Source.CodeBase.Customers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.CodeBase.Ui
{
    public class CustomerOrdersDisplayer : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private OrderCellDisplayer _cellDisplayerPrefab;
        [SerializeField] private HorizontalLayoutGroup _container;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
            _canvas.worldCamera = _camera;
        }

        public void AddOrder(Order order)
        {
            OrderCellDisplayer cell = Instantiate(_cellDisplayerPrefab, _container.transform);
            cell.AddOrder(order);

        }

        private void LateUpdate()
        {
            _canvas.transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
        }

    }
}
