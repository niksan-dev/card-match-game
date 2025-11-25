
using System;
using UnityEngine;

namespace Niksan.CardGame
{
    public static class EventBus
    {
        public static event Action OnClickPlay;
        public static event Action<int, int> OnScoreUpdate;
        public static event Action<ICard> OnCardClicked;
        public static event Action<ICard, ICard> OnCardsMatched;
        public static event Action<ICard, ICard> OnCardsMismatched;
        public static event Action OnLevelCompleted;
        public static event Action<ICard> OnCardFlipped;
        public static event Action OnGameStarted;

        public static void RaiseCardsMatched(ICard a, ICard b) => OnCardsMatched?.Invoke(a, b);
        public static void RaiseCardsMismatched(ICard a, ICard b) => OnCardsMismatched?.Invoke(a, b);
        public static void RaiseLevelCompleted() => OnLevelCompleted?.Invoke();
        public static void RaiseCardFlipped(ICard card) => OnCardFlipped?.Invoke(card);
        public static void RaiseGameStarted() => OnGameStarted?.Invoke();

        public static void RaiseCardClicked(ICard card) => OnCardClicked?.Invoke(card);

        public static void RaiseScoreUpdate(int score, int streak) => OnScoreUpdate?.Invoke(score, streak);
        public static void RaiseClickPlay() => OnClickPlay?.Invoke();
    }
}