using System;
using System.Collections.Generic;




[Serializable]
public class CardSaveData
{
    public int id;
    public CardState state;    // FaceDown / FaceUp / Matched
}

[Serializable]
public class LevelSaveData
{
    public int levelID;
    public int currentScore;
    public int totalMatchesFound;
    public int currentStreak;
    public int currentAttempts;
    public List<CardSaveData> cardsData = new List<CardSaveData>();
}


[Serializable]
public class LevelSaveDataRoot
{
    public List<LevelSaveData> levelsData = new List<LevelSaveData>();
}
