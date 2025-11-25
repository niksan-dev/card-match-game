using System;
using System.Collections;
using System.Collections;
using System.Collections.Generic;
using Niksan.CardGame;
using UnityEngine;

namespace Niksan.UI
{
    public class InGame : ScreenBase
    {
        private void OnEnable()
        {
            EventBus.RaiseScoreUpdate(0,0);
            GameManager.Instance.StartGame();
        }
    }
}
