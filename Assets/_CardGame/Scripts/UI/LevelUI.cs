using System.Collections;
using System.Collections.Generic;
using Niksan.CardGame;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Text levelText;
    [SerializeField] private Text scoreText;

    private int levelId;
    private Button btnLevel;

    void Awake()
    {
        btnLevel = GetComponent<Button>();
        btnLevel.onClick.AddListener(() =>
        {
            Debug.Log("Level " + levelId + " clicked.");
            GameManager.Instance.currentLevel = levelId;
            GameManager.Instance.UIManager.ShowInGame();
            // Here you can add code to load the level or perform other actions
        });
    }

    void OnEnable()
    {
        SetScore(0);
    }


    public void SetLevelName(string levelName)
    {
        levelText.text = levelName;
    }

    public void SetLevelId(int _levelId)
    {
        levelId = _levelId;
        SetScore(0);
    }

    public void SetScore(int score)
    {
        int savedScore = GameManager.Instance.ProgressionManager.GetLevelScore(levelId);
        scoreText.text = "Score: " + (score > savedScore ? score.ToString() : savedScore.ToString());
    }
}
