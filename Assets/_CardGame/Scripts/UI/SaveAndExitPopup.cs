using System.Collections;
using System.Collections.Generic;
using Niksan.CardGame;
using UnityEngine;
using UnityEngine.UI;

public class SaveAndExitPopup : MonoBehaviour
{

    [SerializeField] private BoardGenerator boardGenerator;
    [SerializeField] private Button btnClose;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button exitButton;

    void Awake()
    {
        saveButton.onClick.AddListener(OnClickSave);
        exitButton.onClick.AddListener(OnClickExit);
        btnClose.onClick.AddListener(() => gameObject.SetActive(false));
    }

    private void OnClickSave()
    {
        Debug.Log("Save button clicked.");
        // Add your save logic here

        boardGenerator.SaveLevelData();
        GameManager.Instance.UIManager.ShowMainMenu();
    }

    private void OnClickExit()
    {
        Debug.Log("Exit button clicked.");
        // Add your exit logic here
        GameManager.Instance.LevelsData.levels[GameManager.Instance.currentLevel].isSaved = false;
        GameManager.Instance.UIManager.ShowMainMenu();
    }
}