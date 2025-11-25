using System;
using System.Collections;
using System.Collections;
using System.Collections.Generic;
using Niksan.CardGame;
using UnityEngine;
using UnityEngine.UI;

namespace Niksan.UI
{
    public class InGame : ScreenBase
    {
        [SerializeField] private Button btnBackToMenu;


        void Start()
        {
            btnBackToMenu.onClick.AddListener(() =>
            {
                Debug.Log("[InGame] Back to Main Menu clicked.");
                uIManager.ShowMainMenu();
            });
        }
        private void OnEnable()
        {
            EventBus.RaiseScoreUpdate(0, 0);
            GameManager.Instance.StartGame();
        }


    }
}
