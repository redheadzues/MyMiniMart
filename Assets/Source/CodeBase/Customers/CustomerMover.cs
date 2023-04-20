using Assets.Source.CodeBase.MainCharacter;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Source.CodeBase.Customers
{
    public class CustomerMover : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private CharacterAnimator _animator;

        public void Move(Vector3 point) =>
            _navMeshAgent.SetDestination(point);

        private void Update() => 
            _animator.SetRun(_navMeshAgent.velocity.magnitude);
    }
}
