using UnityEngine;

namespace ShooterGame.Scripts.Game.Gameplay.Enemy
{
    public class EnemyBase : MonoBehaviour, IExplodable
    {
        public void ChangeSpeed(float value){
            GetComponent<EnemyMove>().moveSpeed *= value;
        }
    }
}
