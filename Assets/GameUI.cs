using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    private PlayerBattleController _playerBS;
    private Level _levelManager;
    [SerializeField] private TextMeshProUGUI currentWaveText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;
    
    [SerializeField] private GameObject firstSpellIcon;
    [SerializeField] private GameObject secondSpellIcon;
    [SerializeField] private GameObject thirdSpellIcon;
    [SerializeField] private GameObject fourthSpellIcon;

    private void Start()
    {
        _playerBS = FindFirstObjectByType<PlayerBattleController>();
        _levelManager = FindFirstObjectByType<Level>();
    }

    private void Update()
    {
        if (_playerBS.currentElementNumber == 0)
        {
            firstSpellIcon.SetActive(true);
            secondSpellIcon.SetActive(false);
            thirdSpellIcon.SetActive(false);
            fourthSpellIcon.SetActive(false);
        }
        if (_playerBS.currentElementNumber == 1)
        {
            firstSpellIcon.SetActive(false);
            secondSpellIcon.SetActive(true);
            thirdSpellIcon.SetActive(false);
            fourthSpellIcon.SetActive(false);
        }
        if (_playerBS.currentElementNumber == 2)
        {
            firstSpellIcon.SetActive(false);
            secondSpellIcon.SetActive(false);
            thirdSpellIcon.SetActive(true);
            fourthSpellIcon.SetActive(false);
        }
        if (_playerBS.currentElementNumber == 3)
        {
            firstSpellIcon.SetActive(false);
            secondSpellIcon.SetActive(false);
            thirdSpellIcon.SetActive(true);
            fourthSpellIcon.SetActive(false);
        }
        
        currentWaveText.text = "Current wave: " + _levelManager.currentWaveNumber;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = "You Died!\nSurvived waves: " + _levelManager.currentWaveNumber;
    }
}
