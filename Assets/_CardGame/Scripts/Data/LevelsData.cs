using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Niksan.CardGame.Data
{
    [CreateAssetMenu(menuName = "CardGame/LevelsData")]
    public class LevelsData : ScriptableObject
    {
        public List<global::LevelConfig> levels;
    }
}

[Serializable]
public class LevelConfig
{
    public LevelDifficulty displayName;
    public int rows;
    public int columns;
    public int pointsPerMatch;

    public int bonusMultiplier;
    public int TotalCards => rows * columns;
    public Color colorBg;
    public Color colorCardBack;
    public bool isSaved = false;
}