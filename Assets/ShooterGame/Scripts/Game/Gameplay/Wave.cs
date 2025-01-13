using UnityEngine;
using ShooterGame.Scripts.Game.Gameplay.Enemy;

[System.Serializable]
public class Wave
{
    [HideInInspector]public int enemiesLeft = 0;

    [HideInInspector]public Level levelInstance;

    public void DecreaseEnemiesLeft(EnemyBase enemy){
        Debug.Log(enemiesLeft);
        enemiesLeft--;
        Debug.Log("Enemies left: " + enemiesLeft);
        if(enemiesLeft == 0)
        {
            levelInstance.ChangeWave();
        }
    }
}
