using System.Collections;
using System.Collections.Generic;
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
    }

    private void OnClickNewGame()
    {
        Debug.Log("New Game button clicked.");
        // Add your new game logic here
    }
}