using UnityEngine;
using ShooterGame.Scripts.Game.Gameplay.Enemy;

[System.Serializable]
public class Wave
{
    public int enemiesNumber;

    public int enemiesLeft;

    EnemyBase[] enemies;

    void DecreaseEnemiesLeft(){
        enemiesLeft--;
        if(enemiesLeft == 0)
        {
            WaveCleared();
        }
    }

    void WaveCleared(){

    }
}
