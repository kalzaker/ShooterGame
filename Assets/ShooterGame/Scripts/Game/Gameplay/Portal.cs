using System;
using ShooterGame.Scripts.Game.Gameplay;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private LevelChanger _levelChanger;
    [SerializeField] private GameObject currentLevel;

    private void Start()
    {
        _levelChanger = FindFirstObjectByType<LevelChanger>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered");
            Vector3 pos = new Vector3(transform.position.x + 200f, transform.position.y, transform.position.z);
            
            _levelChanger.NextLevel(pos, currentLevel);
            _levelChanger.TeleportPlayer(other.gameObject);
        }
    }
}
