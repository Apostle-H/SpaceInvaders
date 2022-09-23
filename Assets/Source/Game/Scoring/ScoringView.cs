using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoringView : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreUI;
    
    private void Awake()
    {
        Scoring.OnScoreChanged += UpdateUI;
    }

    private void UpdateUI()
    {
        scoreUI.text = Scoring.Score.ToString();
    }
}
