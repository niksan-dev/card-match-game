using System.Collections;
using System.Collections.Generic;
using Niksan.CardGame.Data;
using Niksan.UI;
using UnityEngine;

namespace Niksan.CardGame
{
    /// <summary>
    /// GameManager handles the overall game flow including starting levels, reacting to events,
    /// playing sounds, and updating progression.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [Header("References")]
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private ProgressionManager progressionManager;
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private BoardGenerator boardGenerator;
        [SerializeField] private MatchFinder matchFinder;
        [SerializeField] private List<LevelConfig> levels;

        private int totalCards;
        public int currentLevel = 0;

        /// <summary>
        /// Total number of levels based on LevelConfig list.
        /// </summary>
        public int maxLevels => levels.Count;

        /// <summary>
        /// Unity Awake lifecycle method. Initializes singleton and loads saved progression.
        /// </summary>
        private void Awake()
        {
            Instance = this;
            InitProgressionManager();
        }

        /// <summary>
        /// Initializes progression manager and loads saved current level.
        /// </summary>
        private void InitProgressionManager()
        {
            progressionManager.Initialize(maxLevels);
            currentLevel = progressionManager.LoadScoreAndLevel().currentLevel;
        }

        /// <summary>
        /// Starts the current level by generating the board and initializing match finder.
        /// </summary>
        public void StartGame()
        {
            if (currentLevel >= maxLevels)
            {
                Debug.LogError("No More Levels Available");
                return;
            }

            totalCards = levels[currentLevel].TotalCards;
            matchFinder.Init(totalCards);
            boardGenerator.GenerateBoard(levels[currentLevel]);
        }

        /// <summary>
        /// Subscribes to relevant game events.
        /// </summary>
        private void OnEnable()
        {
            EventBus.OnCardsMatched += HandleMatch;
            EventBus.OnCardsMismatched += HandleMismatch;
            EventBus.OnLevelCompleted += HandleLevelComplete;
            EventBus.OnCardFlipped += HandleCardFlip;
        }

        /// <summary>
        /// Unsubscribes from all game events to prevent memory leaks.
        /// </summary>
        private void OnDisable()
        {
            EventBus.OnCardsMatched -= HandleMatch;
            EventBus.OnCardsMismatched -= HandleMismatch;
            EventBus.OnLevelCompleted -= HandleLevelComplete;
            EventBus.OnCardFlipped -= HandleCardFlip;
        }

        /// <summary>
        /// Handles when a card is matched.
        /// </summary>
        private void HandleMatch(ICard a, ICard b)
        {
            Debug.Log("Match!");
            soundManager.PlaySound(SoundType.MATCH);
        }

        /// <summary>
        /// Handles when a card is mismatched.
        /// </summary>
        private void HandleMismatch(ICard a, ICard b)
        {
            Debug.Log("Mismatch!");
            soundManager.PlaySound(SoundType.MISMATCH);
        }

        /// <summary>
        /// Handles the completion of a level and triggers the game over UI after a short delay.
        /// </summary>
        private void HandleLevelComplete()
        {
            Debug.Log("Level Done!");
            progressionManager.SaveScoreAndLevel(currentLevel, scoreManager.CurrentScore);
            StartCoroutine(DelayToShowGameOver());
        }

        /// <summary>
        /// Coroutine to delay game over screen display.
        /// </summary>
        private IEnumerator DelayToShowGameOver()
        {
            yield return new WaitForSeconds(2);
            UIManager.Instance.ShowGameOver();
        }

        /// <summary>
        /// Handles when a card is flipped.
        /// </summary>
        private void HandleCardFlip(ICard card)
        {
            Debug.Log("Card Flipped: " + card.ID);
            soundManager.PlaySound(SoundType.FLIP);
        }
    }
}
