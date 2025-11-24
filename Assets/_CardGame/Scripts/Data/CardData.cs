using UnityEngine;

namespace Niksan.CardGame.Data
{
    /// <summary>
    /// Stores data for a single memory card, including ID and front-facing sprite.
    /// Used to match cards and assign visuals.
    /// </summary>
    [CreateAssetMenu(menuName = "CardGame/CardData")]
    public class CardData : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private Sprite frontSprite;

        /// <summary>
        /// The unique identifier for matching this card with its pair.
        /// Cards with the same ID are considered a match.
        /// </summary>
        public int ID => id;

        /// <summary>
        /// The sprite shown when the card is revealed (front face).
        /// </summary>
        public Sprite FrontSprite => frontSprite;

        /// <summary>
        /// (Optional) You can use this method to initialize new data at runtime if needed.
        /// </summary>
        public void Initialize(int newId, Sprite newFrontSprite)
        {
            this.id = newId;
            this.frontSprite = newFrontSprite;
        }
    }
}