using System.Collections;
using System.Collections.Generic;
using Niksan.CardGame.Data;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelUI levelUI;
    [SerializeField] private Transform levelsParent;
    [SerializeField] private LevelsData levelsData;

    private List<LevelUI> levelUIs = new List<LevelUI>();

    void Awake()
    {
        int levelId = 0;
        foreach (var level in levelsData.levels)
        {
            Debug.Log($"Level with {level.TotalCards} cards loaded.");

            GameObject levelGO = Instantiate(levelUI.gameObject, Vector3.zero, Quaternion.identity);

            levelGO.transform.SetParent(levelsParent);
            levelGO.transform.localScale = Vector3.one;
            levelGO.GetComponent<LevelUI>().SetData(level.displayName.ToString(), levelId, level.columns, level.rows);
            levelGO.GetComponent<LevelUI>().SetColor(level.colorBg, level.colorCardBack);
            levelId++;
            levelUIs.Add(levelGO.GetComponent<LevelUI>());
        }
    }

    void OnEnable()
    {
        int i = 0;
        foreach (var level in levelsData.levels)
        {
            levelUIs[i].SetData(level.displayName.ToString(), i, level.columns, level.rows);
            i++;
        }
    }
}
