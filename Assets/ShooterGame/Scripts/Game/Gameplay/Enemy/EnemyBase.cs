using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    private float currentHP = 100f;

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
