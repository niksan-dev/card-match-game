using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Niksan.CardGame
{
    /// <summary>
    /// Handles logic for matching flipped cards and tracking game state.
    /// </summary>
    public class MatchFinder : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("Delay before checking matched cards.")]
        public float checkDelay = 0.5f;

        private Queue<ICard> flipQueue = new Queue<ICard>();   // Cards waiting for match check
        private HashSet<ICard> matchedCards = new HashSet<ICard>(); // Successfully matched cards

        private int totalCards; // Total cards in current level

        #region Unity Events

        private void OnEnable()
        {
            EventBus.OnCardClicked += HandleCardClick;
        }

        private void OnDisable()
        {
            EventBus.OnCardClicked -= HandleCardClick;
        }

        #endregion

        /// <summary>
        /// Initializes the MatchFinder for a new level.
        /// </summary>
        /// <param name="total">Total number of cards on the board.</param>
        public void Init(int total)
        {
            totalCards = total;
            matchedCards.Clear();
            flipQueue.Clear();
        }

        /// <summary>
        /// Handles when a card is clicked. Adds it to the queue for matching.
        /// </summary>
        private void HandleCardClick(ICard clicked)
        {
            if (clicked.IsFlipped || matchedCards.Contains(clicked))
                return;

            clicked.Reveal();
            flipQueue.Enqueue(clicked);

            // Only process when at least 2 cards are flipped
            if (flipQueue.Count >= 2)
            {
                StartCoroutine(ProcessQueue());
            }
        }

        /// <summary>
        /// Processes pairs of cards to determine matches or mismatches.
        /// </summary>
        private IEnumerator ProcessQueue()
        {
            while (flipQueue.Count >= 2)
            {
                ICard first = flipQueue.Dequeue();
                ICard second = flipQueue.Dequeue();

                yield return new WaitForSeconds(checkDelay);

                if (first.ID == second.ID)
                {
                    // Match found
                    matchedCards.Add(first);
                    matchedCards.Add(second);
                    EventBus.RaiseCardsMatched(first, second);

                    if (matchedCards.Count >= totalCards)
                    {
                        EventBus.RaiseLevelCompleted();
                    }
                }
                else
                {
                    // No match — hide both
                    first.Hide();
                    second.Hide();
                    EventBus.RaiseCardsMismatched(first, second);
                }
            }
        }

        /// <summary>
        /// Resets all internal states (used when restarting the level).
        /// </summary>
        public void ResetMatches()
        {
            matchedCards.Clear();
            flipQueue.Clear();
        }
    }
}
