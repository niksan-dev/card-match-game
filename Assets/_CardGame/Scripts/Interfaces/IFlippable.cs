using System.Collections;
using UnityEngine;

namespace Niksan.CardGame
{
    /// <summary>
    /// Interface that defines flip behavior for a card or flippable object.
    /// </summary>
    public interface IFlippable
    {
        /// <summary>
        /// Gets whether the card is currently showing its front (flipped).
        /// </summary>
        bool IsFlipped { get; }

        /// <summary>
        /// Performs a flip animation to show either the front or back of the card.
        /// </summary>
        /// <param name="isFront">True to flip to the front (face) side, false to flip to the back.</param>
        /// <returns>An IEnumerator for coroutine execution.</returns>
        IEnumerator Flip(bool isFront);
    }
}
