using System;
using System.Collections;
using System.Collections.Generic;
using Niksan.CardGame;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace Niksan.UI
{
    public class GameOver : ScreenBase
    {
        [SerializeField] Button replayButton;
        [SerializeField] Button nextButton;
        [SerializeField] Button mainMenuButton;
        private void Awake()
        {
            nextButton.onClick.AddListener(OnClickNext);
            replayButton.onClick.AddListener(OnClicReplay);
            mainMenuButton.onClick.AddListener(() =>
            {
                GameManager.Instance.UIManager.ShowMainMenu();
            });
        }

        private void OnEnable()
        {
            //no more levels available to play
            //disable next button
            nextButton.gameObject.SetActive(GameManager.Instance.currentLevel < GameManager.Instance.maxLevels - 1);
        }

        void OnClickNext()
        {
            GameManager.Instance.currentLevel++;
            Debug.Log("[OnClickNext] OnClickNext]");
            uIManager.ShowInGame();
        }

        void OnClicReplay()
        {
            Debug.Log("[OnClicReplay] OnClicReplay]");
            uIManager.ShowInGame();
        }
    }
}