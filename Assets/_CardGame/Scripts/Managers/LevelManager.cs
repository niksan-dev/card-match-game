using System.Collections;
using System.Collections.Generic;
using Niksan.CardGame.Data;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelUI levelUI;
    [SerializeField] private Transform levelsParent;
    [SerializeField] private LevelsData levelsData;

    void Start()
    {
        int levelId = 0;
        foreach (var level in levelsData.levels)
        {
            Debug.Log($"Level with {level.TotalCards} cards loaded.");

            GameObject levelGO = Instantiate(levelUI.gameObject, Vector3.zero, Quaternion.identity);
            levelGO.transform.localScale = Vector3.one;
            levelGO.transform.SetParent(levelsParent);
            levelGO.GetComponent<LevelUI>().SetLevelName(level.displayName);
            levelGO.GetComponent<LevelUI>().SetLevelId(levelId);
            levelId++;
        }
    }
}
