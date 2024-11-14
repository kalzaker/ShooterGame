using UnityEngine;
using System;

namespace ShooterGame.Scripts.Game.MainMenu.Root.View
{
    public class UIMainMenuRootBinder : MonoBehaviour
    {
        public event Action GoToGameplayButtonClicked;

        public void HandleGoToGameplayButtonClick()
        {
            GoToGameplayButtonClicked?.Invoke();
        }
    }
}