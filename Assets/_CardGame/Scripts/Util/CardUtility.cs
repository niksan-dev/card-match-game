using System.Collections.Generic;
using UnityEngine;

namespace Niksan.CardGame.Utils
{
    public static class CardUtility
    {
        public static List<Card> GenerateShuffledPairs(Sprite[] availableFaces, int pairCount)
        {
            if (availableFaces.Length < pairCount)
            {
                Debug.LogError("Not enough unique card faces to generate the requested pairs!");
                return new List<Card>();
            }

            List<Card> result = new List<Card>();
            List<Sprite> shuffled = new List<Sprite>(availableFaces);
            Shuffle(shuffled);

            for (int i = 0; i < pairCount; i++)
            {
                var sprite = shuffled[i];
                var dataA = new Card(i, sprite);
                var dataB = new Card(i, sprite); // Duplicate with same ID

                result.Add(dataA);
                result.Add(dataB);
            }

            Shuffle(result);
            return result;
        }

        private static void Shuffle<T>(List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}

[System.Serializable]
public class Card
{
    public int id;
    public Sprite faceSprite;

    public Card(int id, Sprite sprite)
    {
        this.id = id;
        this.faceSprite = sprite;
    }
}