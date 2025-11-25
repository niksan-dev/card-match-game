using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Niksan.CardGame
{
    public class ProgressionManager : MonoBehaviour
    {
        private string fileName = "playerData.dat";
        [SerializeField] private PlayerProgress playerProgress;

        public void Initialize(int maxLevels)
        {
            playerProgress = LoadScoreAndLevel();
            Debug.Log($"Initializing progression manager :{JsonUtility.ToJson(playerProgress)}");
            if (playerProgress.levelScores == null || playerProgress.levelScores.Length == 0)
            {
                playerProgress = new PlayerProgress();
                playerProgress.currentLevel = 0;
                playerProgress.levelScores = new int[maxLevels];
                Debug.Log($"Initialized progression manager maxLevels: {maxLevels}");
                for (int i = 0; i < maxLevels; i++)
                {
                    playerProgress.levelScores[i] = -1;
                }
            }

            foreach (var score in playerProgress.levelScores)
            {
                Debug.Log($"Level score loaded======>>>>>: {score}");
            }

            BinarySaveLoadSystem.Save(playerProgress, fileName);
        }

        public void SaveScoreAndLevel(int level, int score)
        {
            playerProgress.currentLevel = level + 1;
            playerProgress.levelScores[level] = score;

            BinarySaveLoadSystem.Save(playerProgress, fileName);
        }

        public int GetLevelScore(int level)
        {
            return playerProgress.levelScores[level];
        }

        public PlayerProgress LoadScoreAndLevel()
        {
            playerProgress = BinarySaveLoadSystem.Load<PlayerProgress>(fileName);
            return playerProgress;
        }



    }
}
[Serializable]
public class PlayerProgress
{
    public int currentLevel = 0;
    public int[] levelScores;

}
