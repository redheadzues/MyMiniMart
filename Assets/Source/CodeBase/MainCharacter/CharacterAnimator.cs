using UnityEngine;

namespace Assets.Source.CodeBase.MainCharacter
{
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void SetRun(float speed)
        {
            _animator.SetFloat("Speed", speed);
        }
    }
}
