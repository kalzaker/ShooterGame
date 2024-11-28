using UnityEngine;

public class EnemyBase : MonoBehaviour, IExplodable
{
    private float currentHP = 100f;

    UnityEngine.AI.NavMeshAgent _agent;

    void Start(){
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeSpeed(float value){
        _agent.speed *= value;
        Debug.Log(_agent.speed);
    }
}
