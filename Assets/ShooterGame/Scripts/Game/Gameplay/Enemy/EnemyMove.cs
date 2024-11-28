using Fragsurf.Movement;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace ShooterGame.Scripts.Game.Gameplay.Enemy
{
    public class EnemyMove : MonoBehaviour
    {
        [HideInInspector]public float moveSpeed;

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

        IEnumerator UpdateMoveSpeed(){
            _agent.speed = moveSpeed;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
