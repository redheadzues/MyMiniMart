using Assets.Source.CodeBase.Infrustructure.SceneData;
using System;
using UnityEngine;

namespace Assets.Source.CodeBase
{
    public class BuyZone : MonoBehaviour
    {
        private int _currentInvest;
        private GameBuildFactory _gameBuildFactory;
        private LocationStaticData _data;

        public int RemainingInvest => _data.Price - _currentInvest;
        public event Action InvestedCountChanged;

        public void Construct(GameBuildFactory factory, LocationStaticData data)
        {
            _gameBuildFactory = factory;
            _data = data;
            InvestedCountChanged?.Invoke();
        }

        public void Invest(int count)
        {
            _currentInvest += count;
            InvestedCountChanged?.Invoke();

            if (RemainingInvest <= 0)
            {
                _gameBuildFactory.CreateBuild(_data);
                Destroy(gameObject);
            }
        }
    }
}
