using UnityEngine;

namespace ShooterGame.Scripts.Game.Gameplay
{
    public class LevelChanger : MonoBehaviour
    {
        [SerializeField] private GameObject[] _levelsList;

        public void NextLevel(Vector3 pos, GameObject currentLevel)
        {
            GameObject nextLevel = _levelsList[Random.Range(0, _levelsList.Length)];
            Instantiate(nextLevel, pos, Quaternion.identity);
            
            Destroy(currentLevel);
        }
    }
}
