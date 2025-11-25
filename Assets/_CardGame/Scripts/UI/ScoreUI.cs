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
        [SerializeField] private Text attemptsText;
        [SerializeField] private Text matchText;

        [SerializeField] private StatsUI statsUI;

        internal int currentAttempts = 0;
        internal int currentMatches = 0;
        internal int currentScore = 0;
        internal int maxStreak = 0;
        private void Awake()
        {
            // Subscribe to score update events
            EventBus.OnScoreUpdate += UpdateUI;
            EventBus.OnAttemptMade += OnAttemptMade;
            EventBus.OnCardsMatched += OnCardsMatched;
        }

        private void OnDestroy()
        {
            // Unsubscribe to prevent memory leaks
            EventBus.OnScoreUpdate -= UpdateUI;
            EventBus.OnAttemptMade -= OnAttemptMade;
            EventBus.OnCardsMatched -= OnCardsMatched;
        }

        void OnEnable()
        {
            UpdateUI(0, 0);
            ResetVariables();
            UpdateMatchText();
            UpdateAttemptText();
        }

        void OnDisable()
        {
            statsUI.UpdateStats(currentAttempts, currentMatches, currentScore, maxStreak);
        }

        void ResetVariables()
        {
            currentAttempts = 0;
            currentMatches = 0;
            currentScore = 0;
            maxStreak = 0;
        }

        void OnCardsMatched(ICard a, ICard b)
        {
            currentMatches++;
            UpdateMatchText();
        }

        void UpdateMatchText()
        {
            matchText.text = $"Matches: {currentMatches}";
        }

        void OnAttemptMade()
        {
            currentAttempts++;
            UpdateAttemptText();
        }

        void UpdateAttemptText()
        {
            attemptsText.text = $"Attempts: {currentAttempts}";
        }

        /// <summary>
        /// Updates the UI with the current score and streak values.
        /// </summary>
        /// <param name="score">The current total score.</param>
        /// <param name="streak">The current streak count.</param>
        private void UpdateUI(int score, int streak)
        {
            currentScore = score;
            if (scoreText != null)
                scoreText.text = $"Score: {score}";

            if (streakText != null)
                streakText.text = $"Streak: {streak}";

            if (maxStreak < streak)
            {
                maxStreak = streak;
            }
        }
    }
}

