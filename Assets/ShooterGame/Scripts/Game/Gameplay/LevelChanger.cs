using UnityEngine;

namespace ShooterGame.Scripts.Game.Gameplay
{
    public class LevelChanger : MonoBehaviour
    {
        [SerializeField] private GameObject[] _levelsList;

        GameObject nextLevel;

        public void NextLevel(Vector3 pos, GameObject currentLevel)
        {
            nextLevel = _levelsList[Random.Range(0, _levelsList.Length)];
            Instantiate(nextLevel, pos, Quaternion.identity);
            
            Destroy(currentLevel);
        }

        public void TeleportPlayer(GameObject player){
            Debug.Log("TELEPORTED");
            Debug.Log(nextLevel.GetComponent<Level>().playerSpawnPoint.position);
            player.transform.position = nextLevel.GetComponent<Level>().playerSpawnPoint.position;
        }
    }
}
