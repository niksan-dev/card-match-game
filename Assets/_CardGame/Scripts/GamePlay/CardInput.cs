using UnityEngine;
using UnityEngine.EventSystems;

namespace Niksan.CardGame
{
    /// <summary>
    /// Handles user input for a card by implementing Unity's pointer click interface.
    /// Relays click events to the game logic via EventBus.
    /// </summary>
    public class CardInput : MonoBehaviour, IPointerClickHandler
    {
        private ICard card;

        /// <summary>
        /// Initializes this input handler with a reference to the associated ICard.
        /// </summary>
        /// <param name="card">The card this input will report clicks for.</param>
        public void Initialize(ICard card)
        {
            this.card = card;
        }

        /// <summary>
        /// Invoked when the card is clicked via Unity EventSystem.
        /// Calls <see cref="OnClick"/>.
        /// </summary>
        /// <param name="eventData">Event data for the click.</param>
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick();
        }

        /// <summary>
        /// Called when the card is clicked.
        /// If the card is already flipped, the click is ignored.
        /// Otherwise, raises the CardClicked event through the EventBus.
        /// </summary>
        public void OnClick()
        {
            if (card == null || card.IsFlipped)
                return;
            Debug.Log("Card clicked: " + card.ID);
            // Notify game logic of the card click TODO: 
            EventBus.RaiseCardClicked(card);
        }
    }

}
