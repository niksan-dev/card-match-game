using System.Collections.Generic;
using Niksan.CardGame.Data;
using Niksan.CardGame.Utils;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace Niksan.CardGame
{
    /// <summary>
    /// Responsible for generating the game board based on the level configuration.
    /// It instantiates card pairs and sets grid layout size dynamically.
    /// </summary>
    public class BoardGenerator : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private SpriteData spriteData;
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private RectTransform boardPanel;
        //   [SerializeField] private GridLayoutGroup gridLayout;
        private List<BasicCard> spawnedCards = new List<BasicCard>();
        [Header("UI Settings")]
        private float hudHeight = 100f; // Optional: Reserved space for HUD if needed

        [Header("Level Configs")]
        [SerializeField] private LevelsData levelsData;

        private float padding = 20f;
        void Start()
        {
            GenerateBoard(levelsData.levels[GameManager.Instance.currentLevel]);
        }
        /// <summary>
        /// Generates a board based on the provided level configuration.
        /// </summary>
        /// <param name="config">Level configuration containing grid and card data.</param>
        public void GenerateBoard(LevelConfig config)
        {

            Debug.Log("Generating board with config: " + config.columns + "x" + config.rows);
            ClearBoard();

            // Set grid layout to use fixed number of columns
            //There is an issue with grid layout and dynamic resizing
            //I'm commenting this out and manually placing cards for now
            // gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            // gridLayout.constraintCount = config.columns;

            int total = config.TotalCards;

            // Calculate usable panel size (excluding padding and spacing)
            float panelWidth = GetWidth(config);

            float panelHeight = GetHeight(config);


            Debug.Log($"Panel Width: {panelWidth}, Panel Height: {panelHeight}");
            // Determine card size based on smallest cell (square shape)
            float cellWidth = panelWidth / config.columns;
            Debug.Log($"Cell Width: {cellWidth}");
            float cellHeight = panelHeight / config.rows;
            Debug.Log($"Cell Height: {cellHeight}");
            float size = Mathf.Min(cellWidth, cellHeight);
            Debug.Log($"Cell Size: {size}");
            // Apply calculated cell size
            // gridLayout.cellSize = new Vector2(size, size);

            // Get shuffled card face pairs
            var pairs = CardUtility.GenerateShuffledPairs(spriteData.sprites, total / 2);
            spawnedCards.Clear();
            // Instantiate and initialize cards
            foreach (var face in pairs)
            {
                // Debug.Log($"CardFactory.Instance : {CardFactory.Instance}");
                // var cardGO 
                // Log= Instantiate(cardPrefab, boardPanel);
                var cardFromFactory = CardFactory.Instance.CreateCard(face, Vector2.zero);
                spawnedCards.Add(cardFromFactory);
                var card = cardFromFactory.GetComponent<ICard>();
                RectTransform rectTransform = cardFromFactory.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.sizeDelta = new Vector2(size, size);
                }
                cardFromFactory.transform.SetParent(boardPanel);
                card.SetData(face);
                card.Reveal();
            }
            Vector2 gridSize = new Vector2(config.columns, config.rows);
            PlaceCardsManually(spawnedCards, boardPanel, size, gridSize);
            StartCoroutine(HideAllCardsAfterDelay(2f));
        }

        IEnumerator HideAllCardsAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            foreach (var card in spawnedCards)
            {
                card.Hide();
            }


            SaveLevelData();
        }

        void SaveLevelData()
        {
            LevelSaveData levelSaveDataRoot = new LevelSaveData();
            foreach (var card in spawnedCards)
            {
                CardSaveData saveData = new CardSaveData
                {
                    id = card.ID,
                    state = card.cardState
                };

                levelSaveDataRoot.cardsData.Add(saveData);

            }

            levelsData.levels[GameManager.Instance.currentLevel].isSaved = true;
        }

        float GetWidth(LevelConfig config)
        {
            Debug.Log("Screen Width: " + Screen.width);
            float panelWidth = Screen.width
                               - padding * 2
                               - padding * (config.columns - 1);

            return panelWidth;
        }
        float GetHeight(LevelConfig config)
        {
            Debug.Log("Screen Height: " + Screen.height);
            float panelHeight = Screen.height
                               - padding * 2
                               - hudHeight
                               - padding * (config.rows - 1);
            return panelHeight;
        }

        void PlaceCardsManually(List<BasicCard> cards, Transform parent, float size, Vector2 gridSize)
        {
            int rows = (int)gridSize.y;
            int columns = (int)gridSize.x;

            float spacingX = 20f;
            float spacingY = 20f;

            // Total board size
            float boardWidth = columns * size + (columns - 1) * spacingX;
            float boardHeight = rows * size + (rows - 1) * spacingY;

            // Starting point (top-left corner)
            float startX = -boardWidth / 2 + size / 2;
            float startY = boardHeight / 2 - size / 2;
            startY -= hudHeight;

            for (int i = 0; i < cards.Count; i++)
            {
                int row = i / columns;
                int col = i % columns;

                float posX = startX + col * (size + spacingX);
                float posY = startY - row * (size + spacingY);

                // For UI (RectTransform)
                RectTransform rt = cards[i].GetComponent<RectTransform>();
                if (rt != null)
                {
                    rt.SetParent(parent, false);
                    rt.sizeDelta = new Vector2(size, size);
                    rt.anchoredPosition = new Vector2(posX, posY);
                }
                else
                {
                    // For world space
                    cards[i].transform.SetParent(parent);
                    cards[i].transform.localPosition = new Vector3(posX, posY, 0f);
                    cards[i].transform.localScale = Vector3.one;
                }
            }
        }

        /// <summary>
        /// Clears all child objects from the board panel.
        /// </summary>
        private void ClearBoard()
        {
            foreach (Transform child in boardPanel)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

}
