using ShooterGame.Scripts.Game.Gameplay.Enemy;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private int damage;
    [SerializeField] private bool damageEnemy, damagePlayer;
    
    
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && damagePlayer)
        {
            //Damage Player
        }

        if (other.gameObject.CompareTag("Enemy") && damageEnemy)
        {
            other.gameObject.GetComponent<EnemyBase>().TakeDamage(damage);
        }
        
        Destroy(gameObject);
    }
}
