using UnityEngine;
using UnityEngine.Events;
using ShooterGame.Scripts.Game.Gameplay.Enemy;

public class EventManager
{
    public UnityEvent<EnemyBase> enemyDied = new UnityEvent<EnemyBase>();

    public UnityEvent levelEnded = new UnityEvent();
}
