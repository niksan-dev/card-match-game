// CardData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Card Match/Card Data")]
public class CardData : ScriptableObject
{
    [Header("Visuals")]
    public Sprite frontSprite;

    [Header("Identity")]
    public string cardId;  // same for matching pair
}
