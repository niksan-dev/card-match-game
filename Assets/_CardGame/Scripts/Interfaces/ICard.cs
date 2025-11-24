using UnityEngine;
using Niksan.CardGame.Data;
namespace Niksan.CardGame
{

    /// <summary>
    /// Represents a card in the memory matching game.
    /// Inherits flip and reveal capabilities.
    /// </summary>
    public interface ICard : IFlippable, IRevealable
    {
        /// <summary>
        /// Gets the unique identifier for the card.
        /// Cards with the same ID are considered a matching pair.
        /// </summary>
        int ID { get; }

        /// <summary>
        /// Sets the data associated with this card, such as its ID and face sprite.
        /// Should be called during card initialization.
        /// </summary>
        /// <param name="data">The card data to assign.</param>
        void SetData(Card data);

        /// <summary>
        /// Handles logic when the card is clicked by the player.
        /// Typically invokes GameManager to process matching logic.
        /// </summary>
        void OnClicked();
    }
}
