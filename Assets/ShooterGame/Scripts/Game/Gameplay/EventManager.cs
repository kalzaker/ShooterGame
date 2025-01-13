using UnityEngine;
using UnityEngine.Events;
using ShooterGame.Scripts.Game.Gameplay.Enemy;

public static class EventManager
{
    public static UnityEvent<EnemyBase> enemyDied = new UnityEvent<EnemyBase>();

    public static UnityEvent levelEnded = new UnityEvent();

    public static UnityEvent<Clip, Vector3> soundPlayed = new UnityEvent<Clip, Vector3>();
}
