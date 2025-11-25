using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Niksan.CardGame.Data
{
    [CreateAssetMenu(menuName = "CardGame/SpriteData")]
    public class SpriteData : ScriptableObject
    {
        public List<SpriteDataEntry> sprites;
    }
}


[System.Serializable]

public class SpriteDataEntry
{
    public int id;
    public Sprite sprite;
}
