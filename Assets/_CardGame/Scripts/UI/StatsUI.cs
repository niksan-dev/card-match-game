using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField] Text txtAttempts;
    [SerializeField] Text txtMatches;
    [SerializeField] Text txtScore;
    [SerializeField] Text txtMaxStreak;

    void OnEnable()
    {
        // UpdateStats(0, 0, 0);
    }

    public void UpdateStats(int attempts, int matches, int score, int streak)
    {
        txtAttempts.text = $"{attempts}";
        txtMatches.text = $"{matches}";
        txtScore.text = $"{score}";
        txtMaxStreak.text = $"{streak}";
    }
}
