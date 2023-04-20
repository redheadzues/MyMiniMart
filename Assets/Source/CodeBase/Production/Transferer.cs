using DG.Tweening;
using System;
using UnityEngine;

namespace Assets.Source.CodeBase.Production
{
    public class Transferer : MonoBehaviour
    {
        [SerializeField] private Product _product;

        private Action<Product> _finishAction;
        private bool _needDestroy;

        public void MoveAtPosition(Vector3 position, float duration, Action<Product> finishAction = null,  bool needDestroy = false)
        {
            _needDestroy = needDestroy;
            _finishAction = finishAction;
            transform.DOMove(position, duration).OnComplete(OnMoveFinished);       
        }

        private void OnMoveFinished()
        {
            _finishAction?.Invoke(_product);

            if(_needDestroy)
                Destroy(gameObject);
        }
    }
}