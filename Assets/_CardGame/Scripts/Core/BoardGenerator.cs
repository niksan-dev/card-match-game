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

            // Instantiate and initialize cards
            foreach (var face in pairs)
            {
                var cardGO = Instantiate(cardPrefab, boardPanel);
                var card = cardGO.GetComponent<ICard>();
                card.SetData(face);
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
