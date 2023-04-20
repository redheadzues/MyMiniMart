using Assets.Source.CodeBase.Production;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source.CodeBase.MainCharacter
{
    public class GameStack : MonoBehaviour
    {
        [SerializeField] private int _maxCount;

        private List<Product> _stackProduct = new List<Product>();
        private float _topPositionY;

        public int FreePlaces => _maxCount - _stackProduct.Count;
        public bool IsEmpty => _stackProduct.Count == 0;
        public event Action StackCountChanged;

        public void AddProduct(Product product)
        {
            product.transform.SetParent(transform);
            Vector3 stackPosition = GetTopPositionOnStack(product.Height);
            product.transform.DOLocalMove(stackPosition, 0.2f);
            product.transform.localRotation = Quaternion.identity;
            _stackProduct.Add(product);
            StackCountChanged?.Invoke();
        }

        public Product GetProduct(ProductType type)
        {
            Product product = _stackProduct.FirstOrDefault(x => x.Type == type);

            if (product != null)
            {
                _stackProduct.Remove(product);
                product.transform.SetParent(null);
                StackCountChanged?.Invoke();
                AlignStack();
            }

            return product;
        }

        private void AlignStack()
        {
            _topPositionY = 0;

            for (int i = 0; i < _stackProduct.Count; i++)
            {
                _stackProduct[i].transform.localPosition = GetTopPositionOnStack(_stackProduct[i].Height);
            }
        }

        private Vector3 GetTopPositionOnStack(float height)
        {
            if (_stackProduct.Count == 0)
            {
                _topPositionY = height/2;
                return Vector3.zero;
            }
            else
                _topPositionY += height;

            return Vector3.zero + Vector3.up * (_topPositionY - height/2);

        }
    }
}
