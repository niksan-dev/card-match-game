using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLevel : MonoBehaviour
{

    [SerializeField] private Button saveButton;


    void Awake()
    {
        saveButton.onClick.AddListener(OnClickSave);
    }

    private void OnClickSave()
    {
        Debug.Log("Save button clicked.");
        // Add your save logic here
    }
}