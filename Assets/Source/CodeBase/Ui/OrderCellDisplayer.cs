using Assets.Source.CodeBase.Customers;
using Assets.Source.CodeBase.Production;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Source.CodeBase.Ui
{
    public class OrderCellDisplayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private Order _order;

        public void AddOrder(Order order)
        {
            _order = order;
            _order.ProductGetted += OnProductGetted;
            _order.Completed += OnCompleted;
            _text.text = $"{_order.ProductType} {_order.CurrentRequiredCount}";
        }

        private void OnCompleted(Order order)
        {
            _order.ProductGetted -= OnProductGetted;
            _order.Completed -= OnCompleted;
            Destroy(gameObject);
        }

        private void OnProductGetted(Product product)
        {
            _text.text = $"{_order.ProductType} {_order.CurrentRequiredCount}";
        }
    }
}
