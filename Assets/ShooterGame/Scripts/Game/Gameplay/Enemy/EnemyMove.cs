using Fragsurf.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace ShooterGame.Scripts.Game.Gameplay.Enemy
{
    public class EnemyMove : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Rigidbody _rigidbody;
        private Transform _target;

        [SerializeField] private bool canMove = true;
        [SerializeField] private float stopDistance = 10f;

        private void Start()
        {
            _target = FindFirstObjectByType<SurfCharacter>().transform;
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (canMove)
                Move();
            else
                transform.LookAt(_target);
        }

        private void Move()
        {
            if(Vector3.Distance(transform.position, _target.position) > stopDistance)
                _agent.SetDestination(_target.position);
            else
                _agent.ResetPath();
        }
    }
}
