using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.CodeBase.MainCharacter
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private DynamicJoystick _joystick;
        [SerializeField] private float _speed;
        [SerializeField] private CharacterAnimator _characterAnimator;
        [SerializeField] private CharacterController _characterController;

        private float _positionY;

        private void Awake() => 
            _positionY = transform.position.y;

        private void Update()
        {
            Vector3 direction = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);

            if (direction.magnitude != 0)
                Move(direction);

            _characterAnimator.SetRun(direction.magnitude);
        }

        private void Move(Vector3 direction)
        {
            _characterController.Move(direction * _speed * Time.deltaTime);
            transform.LookAt(transform.position + direction);

            transform.position = new Vector3(transform.position.x, _positionY, transform.position.z);
        }
    }
}
