using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Niksan.CardGame.Data
{
    [CreateAssetMenu(menuName = "CardGame/LevelsData")]
    public class LevelsData : ScriptableObject
    {
        public List<LevelConfig> levels;
    }
}