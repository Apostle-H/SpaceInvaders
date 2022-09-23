using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class Scoring
{
    public static int Score { get; private set; }

    public static VoidHandler OnScoreChanged;
    
    public static void AddScore(int scoreToAdd)
    {
        Score += scoreToAdd;
        
        OnScoreChanged?.Invoke();
    }
}
