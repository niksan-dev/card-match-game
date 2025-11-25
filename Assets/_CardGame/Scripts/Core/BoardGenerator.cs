using System.Collections.Generic;
using Niksan.CardGame.Data;
using Niksan.CardGame.Utils;
using UnityEngine;
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
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private RectTransform boardPanel;
        [SerializeField] private GridLayoutGroup gridLayout;
        private List<BasicCard> spawnedCards = new List<BasicCard>();
        [Header("UI Settings")]
        private float hudHeight = 150f; // Optional: Reserved space for HUD if needed

        [Header("Level Configs")]
        [SerializeField] private List<LevelConfig> levelConfigs;
        void Start()
        {
            GenerateBoard(levelConfigs[0]);
        }
        /// <summary>
        /// Generates a board based on the provided level configuration.
        /// </summary>
        /// <param name="config">Level configuration containing grid and card data.</param>
        public void GenerateBoard(LevelConfig config)
        {
            ClearBoard();

            // Set grid layout to use fixed number of columns
            gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayout.constraintCount = config.columns;

            int total = config.TotalCards;

            // Calculate usable panel size (excluding padding and spacing)
            float panelWidth = boardPanel.rect.width
                               - gridLayout.padding.left
                               - gridLayout.padding.right
                               - hudHeight
                               - gridLayout.spacing.x * (config.columns - 1);

            float panelHeight = boardPanel.rect.height
                                - gridLayout.padding.top
                                - hudHeight
                                - gridLayout.padding.bottom
                                - gridLayout.spacing.y * (config.rows - 1);

            // Determine card size based on smallest cell (square shape)
            float cellWidth = panelWidth / config.columns;
            float cellHeight = panelHeight / config.rows;
            float size = Mathf.Min(cellWidth, cellHeight);

            // Apply calculated cell size
            gridLayout.cellSize = new Vector2(size, size);

            // Get shuffled card face pairs
            var pairs = CardUtility.GenerateShuffledPairs(config.cardFaces, total / 2);
            spawnedCards.Clear();
            // Instantiate and initialize cards
            foreach (var face in pairs)
            {
                Debug.Log($"CardFactory.Instance : {CardFactory.Instance}");
                // var cardGO 
                // Log= Instantiate(cardPrefab, boardPanel);
                var cardFromFactory = CardFactory.Instance.CreateCard(face, Vector2.zero);
                spawnedCards.Add(cardFromFactory);
                var card = cardFromFactory.GetComponent<ICard>();
                cardFromFactory.transform.SetParent(boardPanel);
                card.SetData(face);
            }
            Vector2 gridSize = new Vector2(config.columns, config.rows);
            PlaceCardsManually(spawnedCards, boardPanel, size, gridSize);
        }


        void PlaceCardsManually(List<BasicCard> cards, Transform parent, float size, Vector2 gridSize)
        {
            int rows = (int)gridSize.x;
            int columns = (int)gridSize.y;

            float spacingX = 20f;
            float spacingY = 20f;

            // Total board size
            float boardWidth = columns * size + (columns - 1) * spacingX;
            float boardHeight = rows * size + (rows - 1) * spacingY;

            // Starting point (top-left corner)
            float startX = -boardWidth / 2 + size / 2;
            float startY = boardHeight / 2 - size / 2;

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
                Destroy(child.gameObject);
            }
        }
    }

}
