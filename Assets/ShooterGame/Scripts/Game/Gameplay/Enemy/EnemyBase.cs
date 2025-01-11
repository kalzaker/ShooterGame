using UnityEngine;

namespace ShooterGame.Scripts.Game.Gameplay.Enemy
{
    public class EnemyBase : MonoBehaviour, IExplodable
    {
        [SerializeField] float collisionDamage;

        public void ChangeSpeed(float value){
            GetComponent<EnemyMove>().moveSpeed *= value;
        }

        void OnCollision(Collision coll){
            if(coll.gameObject.TryGetComponent<Health>(out Health player) && coll.gameObject.CompareTag("Player")){
                player.TakeDamage(collisionDamage);
            }
        }
    }
}
