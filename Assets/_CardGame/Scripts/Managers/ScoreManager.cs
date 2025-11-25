
using System;
using UnityEngine;

namespace Niksan.CardGame
{
   /// <summary>
    /// Tracks and updates the player's score and match streaks.
    /// </summary>
    public class ScoreManager : MonoBehaviour
    {
        internal int CurrentScore { get; private set; }
        private int matchStreak;

        private const int PER_MATCH_POINTS = 100;
        private const int BONUS_MULTIPLIER = 15;

        #region Unity Events

        private void OnEnable()
        {
            EventBus.OnCardsMatched += OnCardsMatched;
            EventBus.OnCardsMismatched += OnCardsMismatched;
            ResetScore();
        }

        private void OnDisable()
        {
            EventBus.OnCardsMatched -= OnCardsMatched;
            EventBus.OnCardsMismatched -= OnCardsMismatched;
            ResetScore();
        }

        #endregion

        /// <summary>
        /// Called when two matching cards are found.
        /// </summary>
        private void OnCardsMatched(ICard cardA, ICard cardB)
        {
            AddMatchScore();
        }

        /// <summary>
        /// Called when two flipped cards do not match.
        /// </summary>
        private void OnCardsMismatched(ICard cardA, ICard cardB)
        {
            ResetStreak();
        }

        /// <summary>
        /// Resets both score and streak.
        /// </summary>
        public void ResetScore()
        {
            CurrentScore = 0;
            matchStreak = 0;
            EventBus.RaiseScoreUpdate(CurrentScore, matchStreak);
        }

        /// <summary>
        /// Increases score and streak when a match occurs.
        /// </summary>
        private void AddMatchScore()
        {
            matchStreak++;
            int bonus = matchStreak * BONUS_MULTIPLIER;
            int scoreToAdd = PER_MATCH_POINTS + bonus;
            CurrentScore += scoreToAdd;

            EventBus.RaiseScoreUpdate(CurrentScore, matchStreak);
        }

        /// <summary>
        /// Resets the match streak when a mismatch occurs.
        /// </summary>
        private void ResetStreak()
        {
            matchStreak = 0;
            EventBus.RaiseScoreUpdate(CurrentScore, matchStreak);
        }
    }
}
