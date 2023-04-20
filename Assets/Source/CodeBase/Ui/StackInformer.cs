using Assets.Source.CodeBase.MainCharacter;
using UnityEngine;

namespace Assets.Source.CodeBase.Ui
{
    public class StackInformer : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private GameStack _stack;
        [SerializeField] private Canvas _canvas;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
            _canvas.worldCamera = _camera;
        }

        private void OnEnable() =>
            _stack.StackCountChanged += OnStackCountChanged;

        private void OnDisable() =>
        _stack.StackCountChanged -= OnStackCountChanged;

        private void LateUpdate()
        {
            transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
        }

        private void OnStackCountChanged()
        {
            if (_stack.FreePlaces == 0)
                _canvasGroup.alpha = 1;
            else
                _canvasGroup.alpha = 0;
        }
    }

}