using UnityEngine;
using ShooterGame.Scripts.Game.Gameplay.Enemy;

public class Level : MonoBehaviour
{
    [SerializeField] EnemyBase[] enemies;

    [SerializeField] Portal portal;

    [SerializeField] Transform[] enemiesSpawnPoints;

    [SerializeField] Wave[] waves;

    public Transform playerSpawnPoint;

    public Wave currentWave;

    int currentWaveNumber;

    void Start(){

        Debug.Log(waves);
        StartLevel();
    }

    public void StartLevel(){
        currentWave = waves[0];
        currentWaveNumber = 1;
        Debug.Log("Level started");
        StartWave();
    }

    public void ChangeWave(){
        currentWave = waves[currentWaveNumber];
        currentWaveNumber++;
        StartWave();
    }

    public void StartWave()
    {
        Debug.Log("Wave "+ currentWaveNumber);
        currentWave.levelInstance = this;
        
        EventManager.enemyDied.AddListener(currentWave.DecreaseEnemiesLeft);

        foreach(Transform spawnpoint in enemiesSpawnPoints){
            if(Random.Range(currentWave, 100) < 50)
            for(int i = 0; i < Mathf.sqrt(wavesNumber)){
                Instantiate(enemies[Random.Range(0,enemies.Length)], spawnpoint.position, Quaternion.identity);
                currentWave.enemiesLeft++;
            }
        }
    }

}