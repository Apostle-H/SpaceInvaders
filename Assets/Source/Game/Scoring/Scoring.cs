using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public int score { get; private set; }

    public event VoidHandler OnScoreChanged;
    
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        
        OnScoreChanged?.Invoke();
    }
}
