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

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent<Health>(out Health target))
        {
            if(other.gameObject.CompareTag("Enemy")){
                return;
            }
            target.TakeDamage(damage);
        }


        Destroy(gameObject);
        
    }
}
