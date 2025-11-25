using System.Collections;
using System.Collections.Generic;
using Niksan.CardGame;
using UnityEngine;
using UnityEngine.UI;

public class LoadSavedPopup : MonoBehaviour
{
    [SerializeField] private Button btnClose;
    [SerializeField] private Button saveGameButton;
    [SerializeField] private Button newGameButton;

    void Awake()
    {
        saveGameButton.onClick.AddListener(OnClickSaveGame);
        newGameButton.onClick.AddListener(OnClickNewGame);
        btnClose.onClick.AddListener(() => gameObject.SetActive(false));
    }

    private void OnClickSaveGame()
    {
        Debug.Log("Save Game button clicked.");
        // Add your save logic here

        // GameManager.Instance.currentLevel = levelId;
        GameManager.Instance.UIManager.ShowInGame();
        GameManager.Instance.LevelsData.levels[GameManager.Instance.currentLevel].isSaved = true;
        GameManager.Instance.UIManager.ShowPreviouslySavedPopup(false);
    }

    private void OnClickNewGame()
    {
        Debug.Log("New Game button clicked.");
        LevelSaveDataRoot levelSaveDataRoot = BinarySaveLoadSystem.Load<LevelSaveDataRoot>(GameManager.Instance.levelSaveFileName);
        LevelSaveData levelSaveData = levelSaveDataRoot.levelsData.Find(l => l.levelID == GameManager.Instance.currentLevel);
        if (levelSaveData != null)
            levelSaveDataRoot.levelsData.Remove(levelSaveData);
        BinarySaveLoadSystem.Save(levelSaveDataRoot, GameManager.Instance.levelSaveFileName);
        // Add your new game logic here
        GameManager.Instance.LevelsData.levels[GameManager.Instance.currentLevel].isSaved = false;
        GameManager.Instance.UIManager.ShowInGame();
        GameManager.Instance.UIManager.ShowPreviouslySavedPopup(false);
    }
}