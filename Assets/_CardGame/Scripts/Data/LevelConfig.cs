using UnityEngine;

namespace Niksan.CardGame.Data
{
    /// <summary>
    /// Configuration data for each memory card game level,
    /// including grid dimensions and available card faces.
    /// </summary>
    [CreateAssetMenu(menuName = "CardGame/Level Config", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [Header("Grid Settings")]
        [Tooltip("Number of rows in the card grid.")]
        public int rows = 2;

        [Tooltip("Number of columns in the card grid.")]
        public int columns = 2;

        [Header("Optional Info")]
        [Tooltip("Unique identifier used internally.")]
        public string levelID = "Level_1";

        [Tooltip("Label to show in UI or menus (e.g., '4 x 4').")]
        public string displayName = "2 x 2";

        [Tooltip("Points per matched pair in this level.")]
        public int pointsPerPair = 10;

        [Tooltip("Difficulty rating of the level.")]
        public LevelDifficulty difficulty = LevelDifficulty.Easy;

        public int streakMultiplier = 5;

        public Color bgColor = Color.white;
        /// <summary>
        /// Total number of cards in the level (rows * columns).
        /// </summary>
        public int TotalCards => rows * columns;

        /// <summary>
        /// Whether the total number of cards is even (required for pairing).
        /// </summary>
        public bool IsEvenPairCount => TotalCards % 2 == 0;

        /// <summary>
        /// Validates that there are enough card face sprites for the required number of pairs.
        /// </summary>
        //public bool HasEnoughUniqueFaces => cardFaces != null && cardFaces.Length >= TotalCards / 2;
    }
}

public enum LevelDifficulty
{
    Easy,
    Medium,
    Hard,
    SuperHard,
    ExtremeHard
}