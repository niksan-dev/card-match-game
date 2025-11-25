using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Niksan.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;


        [SerializeField] private GameObject mainMenuObject;
        [SerializeField] private GameObject saveAndExitPopupObject;
        [SerializeField] private GameObject loadSavedPopupObject;
        [SerializeField] private GameObject inGameObject;
        [SerializeField] private GameObject gameOverObject;
        [SerializeField] private ScreenBase[] screens;

        private void Awake()
        {
            Instance = this;
            InitAllScreens();
        }

        private void OnEnable()
        {
            ShowMainMenu();
        }

        void InitAllScreens()
        {
            foreach (var screen in screens)
            {
                screen.Init(this);
            }
        }

        public void ShowSaveAndExitPopup(bool enable)
        {
            // Implementation for showing Save and Exit Popup
            saveAndExitPopupObject.SetActive(enable);
        }

        public void ShowPreviouslySavedPopup(bool enable)
        {
            // Implementation for showing Load Previously Saved Popup
            loadSavedPopupObject.SetActive(enable);
        }

        public void ShowMainMenu()
        {
            HideAll();
            mainMenuObject.SetActive(true);
        }

        public void ShowGameOver()
        {
            HideAll();
            gameOverObject.SetActive(true);
        }

        public void ShowInGame()
        {
            HideAll();
            inGameObject.SetActive(true);
        }

        void HideAll()
        {
            mainMenuObject.SetActive(false);
            inGameObject.SetActive(false);
            gameOverObject.SetActive(false);
            saveAndExitPopupObject.SetActive(false);
            loadSavedPopupObject.SetActive(false);
        }
    }
}
