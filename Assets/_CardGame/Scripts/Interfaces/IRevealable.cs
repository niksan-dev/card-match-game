using UnityEngine;

namespace Niksan.CardGame
{
    /// <summary>
    /// Interface for objects that can be revealed or hidden.
    /// Typically used for showing or hiding the front face of a card.
    /// </summary>
    public interface IRevealable
    {
        /// <summary>
        /// Reveals the front face of the card or visual object.
        /// Often used when a card is selected or flipped by the player.
        /// </summary>
        void Reveal();

        /// <summary>
        /// Hides the front face and shows the back side of the card.
        /// Often used after a mismatch or during board initialization.
        /// </summary>
        void Hide();

        /// <summary>
        /// Makes the object disappear from the board entirely.
        /// Often called when the object is successfully matched
        /// or removed during gameplay animations.
        /// </summary>
        void Disappear();
    }
}
