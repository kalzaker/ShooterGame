using UnityEngine;
using ShooterGame.Scripts.Game.Gameplay.Enemy;

public class Level : MonoBehaviour
{
    [SerializeField] EnemyBase[] enemies;

    [SerializeField] Transform[] enemiesSpawnPoints;
    
    [SerializeField] Wave[] waves;

    public Wave currentWave;

    int currentWaveNumber;

    public void StartLevel(){
        currentWave = waves[0];
        currentWaveNumber = 1;
        StartWave();
    }

    public void ChangeWave(){
        if(currentWaveNumber == waves.Length + 1){
            EndLevel();
            return;
        }
        currentWave = waves[currentWaveNumber];
        currentWaveNumber++;
        StartWave();
    }

    public void StartWave()
    {
        currentWave.levelInstance = this;

        foreach(EnemyBase enemyToSummon in currentWave.enemies){
            Instantiate(enemyToSummon, enemiesSpawnPoints[Random.Range(0, enemiesSpawnPoints.Length)].position, Quaternion.identity);
            currentWave.enemiesLeft++;
        }
    }

    void EndLevel(){

    }
}