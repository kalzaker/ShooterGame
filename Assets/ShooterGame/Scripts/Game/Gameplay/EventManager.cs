using UnityEngine;
using UnityEngine.Events;
using ShooterGame.Scripts.Game.Gameplay.Enemy;

public static class EventManager
{
    public static UnityEvent enemyDied = new UnityEvent();

    public static UnityEvent levelEnded = new UnityEvent();
}
