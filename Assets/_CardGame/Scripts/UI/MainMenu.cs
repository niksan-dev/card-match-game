using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Niksan.UI
{
    public class MainMenu : ScreenBase
    {
        [SerializeField] Button playButton;
        private void Awake()
        {
            playButton.onClick.AddListener(OnClickPlay);
        }
        void OnClickPlay()
        {
            Debug.Log("[OnClickPlay] OnClickPlay]");
            uIManager.ShowInGame();  
        }
    }
}
