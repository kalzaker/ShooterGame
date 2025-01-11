using UnityEngine;
using ShooterGame.Scripts.Game.Gameplay.Enemy;

[System.Serializable]
public class Wave
{
    [HideInInspector]public int enemiesLeft;

    [HideInInspector]public Level levelInstance;

    public void DecreaseEnemiesLeft(){
        enemiesLeft--;
        Debug.Log("Enemies left: " + enemiesLeft);
        if(enemiesLeft == 0)
        {
            WaveCleared();
        }
    }

    void WaveCleared(){
        levelInstance.ChangeWave();
    }
}
