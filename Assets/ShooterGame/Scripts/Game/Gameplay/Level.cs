using UnityEngine;
using ShooterGame.Scripts.Game.Gameplay.Enemy;

public class Level : MonoBehaviour
{
    [SerializeField] EnemyBase[] enemies;

    [SerializeField] Transform[] enemiesSpawnPoints;
    
    [SerializeField] Wave[] waves;

    Wave currentWave;

    [System.Serializable]
    public class Wave
    {
        public Wave(EnemyBase[] enemies, int enemiesNumber){

        }

        public int enemiesNumber;
        
        [HideInInspector] public int enemiesLeft;

        public EnemyBase[] enemies;

    }

    void StartLevel()
    {
        currentWave = waves[0];
    }

    public void StartWave(Wave wave)
    {
        foreach(EnemyBase enemyToSummon in currentWave.enemies){
            Instantiate(enemyToSummon, enemiesSpawnPoints[Random.Range(0, enemiesSpawnPoints.Length)].position, Quaternion.identity);
            currentWave.enemiesLeft++;
        }
    }
}
