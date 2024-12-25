using UnityEngine;
using ShooterGame.Scripts.Game.Gameplay.Enemy;

[System.Serializable]
public class Wave
{
    public int enemiesLeft;

    public EnemyBase[] enemies;

    public Level levelInstance;

    public void DecreaseEnemiesLeft(){
        enemiesLeft--;
        if(enemiesLeft == 0)
        {
            WaveCleared();
        }
    }

    void WaveCleared(){
        levelInstance.StartWave();
    }
}
