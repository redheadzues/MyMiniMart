using Assets.Source.CodeBase.Production;
using DG.Tweening;
using System;
using UnityEngine;

namespace Assets.Source.CodeBase.MainCharacter
{
    public class Wallet : MonoBehaviour
    {
        private int _money;

        public event Action<int> MoneyChanged;
        public int Money => _money;

        public void TakeMoney(Product money)
        {
            if(money.Type == ProductType.Money)
            {
                money.transform.DOMove(transform.position, 0.2f).OnComplete(() => Destroy(money.gameObject)).OnComplete(AddMoney);
            }
        }

        public bool TrySpendMoney(int count)
        {
            if(_money - count > 0)
            {
                _money -= count;
                MoneyChanged?.Invoke(_money);
                return true;
            }
            else
                return false;
        }

        private void AddMoney()
        {
            _money++;
            MoneyChanged?.Invoke(_money);
        }
    }
}
