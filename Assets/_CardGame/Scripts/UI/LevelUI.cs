using System.Collections;
using System.Collections.Generic;
using Niksan.CardGame;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Text levelText;
    [SerializeField] private Text scoreText;

    [SerializeField] private Text txtGridSize;

    [SerializeField] private Image imgBg;
    [SerializeField] private Image imgOverlay;
    [SerializeField] private GameObject saveIcon;
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
            GameManager.Instance.LevelsData.levels[levelId].isSaved = false;
        });
    }

    void OnEnable()
    {
        SetScore(0);
    }

    public void SetData(string levelName, int levelId, int col, int row)
    {
        saveIcon.SetActive(GameManager.Instance.LevelsData.levels[levelId].isSaved);
        SetLevelName(levelName);
        SetLevelId(levelId);
        SetGridSize(col, row);
    }

    public void SetColor(Color textColor, Color bgColor)
    {
        imgBg.color = bgColor;
        imgOverlay.color = textColor; // Semi-transparent overlay
        levelText.color = bgColor;
        scoreText.color = bgColor;
        txtGridSize.color = textColor;
    }

    void SetGridSize(int col, int row)
    {
        txtGridSize.text = col + "x" + row;
    }


    void SetLevelName(string levelName)
    {
        levelText.text = levelName;
    }

    void SetLevelId(int _levelId)
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
