using UnityEngine;

public class EscUI : MonoBehaviour
{
    [SerializeField] private GameObject _escUI;
    private bool _isEscUIActive = false;

    private void Awake()
    {
        _escUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isEscUIActive)
            {
                _isEscUIActive = true;
                EscOpen();
            }
            else
            {
                _isEscUIActive = false;
                EscClose();
            }
        }
    }
        
    public void EscOpen()
    {
        Time.timeScale = 0;
        _escUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void EscClose()
    {
        Time.timeScale = 1;
        _escUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void EscSwitch()
    {
        _escUI.SetActive(!_escUI.activeSelf);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
