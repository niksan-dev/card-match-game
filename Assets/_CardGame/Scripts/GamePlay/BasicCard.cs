using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Niksan.CardGame.Data;
namespace Niksan.CardGame
{
    /// <summary>
    /// Represents a basic memory game card that can be flipped, revealed, or hidden.
    /// Implements the ICard, IFlippable, and IRevealable interfaces.
    /// </summary>
    public class BasicCard : MonoBehaviour, ICard
    {

        private CanvasGroup canvasGroup;

        public CardState cardState = CardState.FaceDown;
        [Header("Card Visuals")]
        [SerializeField] private Image frontImage;         // Image component to show the front face
        [SerializeField] private GameObject frontRoot;     // Parent GameObject for the front side
        [SerializeField] private GameObject backRoot;      // Parent GameObject for the back side

        [Header("Flip Animation Settings")]
        [SerializeField] private float flipDuration = 0.25f;  // Duration for the flip animation

        private Card data;
        private bool isFlipped = false;

        /// <summary>
        /// Returns true if the card is currently showing its front face.
        /// </summary>
        public bool IsFlipped => isFlipped;

        /// <summary>
        /// The ID of the card, derived from its data.
        /// </summary>
        public int ID => data?.id ?? -1;

        /// <summary>
        /// Sets up the card's visuals and logic from the assigned CardData.
        /// </summary>
        /// <param name="cardData">The data representing this card's identity and appearance.</param>
        public void SetData(Card cardData)
        {
            canvasGroup.alpha = 1;
            this.data = cardData;
            frontImage.sprite = cardData.faceSprite;
            GetComponent<CardInput>()?.Initialize(this);
            HideInstant(); // Card starts hidden (back side shown)
        }

        private void Awake()
        {
            // Optional initialization if needed
            canvasGroup = GetComponent<CanvasGroup>();
        }

        /// <summary>
        /// Triggered by CardInput when the player clicks on this card.
        /// Should notify game logic via GameManager/EventBus.
        /// </summary>
        public void OnClicked()
        {
            // Logic handled externally via input component and GameController
        }

        /// <summary>
        /// Reveals the card with an animated flip.
        /// </summary>
        public void Reveal()
        {
            if (isFlipped) return;
            cardState = CardState.FaceUp;
            StartCoroutine(Flip(true));
        }

        /// <summary>
        /// Hides the card with an animated flip to back.
        /// </summary>
        public void Hide()
        {
            if (!isFlipped) return;
            cardState = CardState.FaceDown;
            StartCoroutine(Flip(false));
        }

        /// <summary>
        /// Instantly hides the card without animation (used on setup).
        /// </summary>
        private void HideInstant()
        {
            isFlipped = false;
            frontRoot.SetActive(false);
            backRoot.SetActive(true);
            transform.localScale = Vector3.one;
        }

        /// <summary>
        /// Coroutine that performs a flipping animation over time.
        /// </summary>
        /// <param name="showFront">Whether to show the front of the card.</param>
        /// <returns>Coroutine IEnumerator.</returns>
        public IEnumerator Flip(bool showFront)
        {
            isFlipped = showFront;

            float halfDuration = flipDuration / 2f;

            // Shrink (scale x from 1 to 0)
            yield return ScaleXOverTime(1f, 0f, halfDuration);

            // Switch front/back visuals
            transform.localScale = new Vector3(0f, 1f, 1f);
            frontRoot.SetActive(showFront);
            backRoot.SetActive(!showFront);

            // Expand (scale x from 0 to 1)
            yield return ScaleXOverTime(0f, 1f, halfDuration);
        }

        /// <summary>
        /// Smoothly animates the X scale of the card over a given duration.
        /// </summary>
        private IEnumerator ScaleXOverTime(float from, float to, float duration)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                float scale = Mathf.Lerp(from, to, elapsed / duration);
                transform.localScale = new Vector3(scale, 1f, 1f);
                elapsed += Time.deltaTime;
                yield return null;
            }

            // Ensure scale is perfectly set at end
            transform.localScale = new Vector3(to, 1f, 1f);
        }

        public void Disappear()
        {
            Reveal();
            canvasGroup.alpha = 0.5f;
            cardState = CardState.Matched;
            // transform.localScale = Vector3.zero;

            //add back to object pool if implemented
            // ObjectPooler.Instance.ReturnToPool(this.gameObject);
        }

    }

}


