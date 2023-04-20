using Assets.Source.CodeBase.Infrustructure.Roots;
using Assets.Source.CodeBase.Shop;
using System.Collections;
using UnityEngine;

namespace Assets.Source.CodeBase.Customers
{
    public class CustomerSpawner
    {
        private readonly float _spawnTime;
        private readonly CustomerFactory _factory;
        private readonly ICorouteineRunner _runner;
        private readonly Store _store;

        public CustomerSpawner(float spawnTime, CustomerFactory factory, ICorouteineRunner runner, Store store)
        {
            _spawnTime = spawnTime;
            _factory = factory;
            _runner = runner;
            _store = store;

            _runner.StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            while(true)
            {
                if(_store.HasFreePlace())
                    _factory.CreateCustomer();

                yield return new WaitForSeconds(_spawnTime);
            }
        }
    }
}
