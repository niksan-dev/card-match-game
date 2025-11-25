using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Niksan.CardGame
{

    /// <summary>
    /// Displays the current score and streak on the UI.
    /// Listens to score updates via the EventBus.
    /// </summary>
    public class ScoreUI : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Text scoreText;
        [SerializeField] private Text streakText;

        private void Awake()
        {
            // Subscribe to score update events
            EventBus.OnScoreUpdate += UpdateUI;
        }

        private void OnDestroy()
        {
            // Unsubscribe to prevent memory leaks
            EventBus.OnScoreUpdate -= UpdateUI;
        }

        void OnEnable()
        {
            UpdateUI(0, 0);
        }

        /// <summary>
        /// Updates the UI with the current score and streak values.
        /// </summary>
        /// <param name="score">The current total score.</param>
        /// <param name="streak">The current streak count.</param>
        private void UpdateUI(int score, int streak)
        {
            if (scoreText != null)
                scoreText.text = $"Score: {score}";

            if (streakText != null)
                streakText.text = $"Streak: {streak}";
        }
    }
}

