using Assets.Source.CodeBase.Production;
using Assets.Source.CodeBase.MainCharacter;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.CodeBase.Shop
{
    public class CashZone : MonoBehaviour
    {
        [SerializeField] private List<GameStack> _stacksMoney;

        private int _stackIndex;

        public void TakeMoney(Product product)
        {
            if (product.Type != ProductType.Money)
                return;

            _stacksMoney[_stackIndex].AddProduct(product);
            _stackIndex++;
            _stackIndex = (int)Mathf.Repeat(_stackIndex, _stacksMoney.Count - 1);
        }

        public Product GetMoney()
        {
            foreach(GameStack stack in _stacksMoney)
            {
                if (stack.IsEmpty == false)
                    return stack.GetProduct(ProductType.Money);
            }

            return null;
        }
    }
}
