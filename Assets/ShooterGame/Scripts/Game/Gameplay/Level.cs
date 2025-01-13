using UnityEngine;
using ShooterGame.Scripts.Game.Gameplay.Enemy;

public class Level : MonoBehaviour
{
    [SerializeField] EnemyBase[] enemies;

    [SerializeField] Transform[] enemiesSpawnPoints;

    public Wave currentWave;

    public int currentWaveNumber;

    [SerializeField] GameObject player;

    void Start()
    {
        StartLevel();

        
    }

    public void StartLevel(){
        currentWave = new Wave();
        currentWaveNumber = 1;
        Debug.Log("Level started");
        StartWave();
    }

    public void ChangeWave(){
        currentWave = new Wave();
        currentWaveNumber++;
        StartWave();
    }

    public void StartWave()
    {
        
        EventManager.soundPlayed.Invoke(Clip.WaveCleared, player.gameObject.transform.position);

        Debug.Log("Wave "+ currentWaveNumber);
        currentWave.levelInstance = this;
        
        EventManager.enemyDied.AddListener(currentWave.DecreaseEnemiesLeft);

        foreach(Transform spawnpoint in enemiesSpawnPoints){
            if(Random.Range(currentWaveNumber, 100) > 50 + currentWaveNumber || currentWave.enemiesLeft == 0)
            {
                for(int i = 0; i < Random.Range(0, 1 + Mathf.Sqrt(currentWaveNumber)); i++)
                {
                    Instantiate(enemies[Random.Range(0,enemies.Length)], spawnpoint.position, Quaternion.identity);
                    currentWave.enemiesLeft++;
                }
            }
        }
    }

}