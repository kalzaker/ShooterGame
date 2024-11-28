using Fragsurf.Movement;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public float moveSpeed;
    
    private NavMeshAgent _agent;
    private Rigidbody _rigidbody;
    private Transform _target;

    private void Start()
    {
        _target = FindFirstObjectByType<SurfCharacter>().transform;
        _agent = GetComponent<NavMeshAgent>();
        
        
    }

    private void Update()
    {
        _agent.SetDestination(_target.position);
    }
}
