using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoringView : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private Scoring scoring;
    
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreUI;
    
    private void Awake()
    {
        scoring.OnScoreChanged += UpdateUI;
    }

    private void UpdateUI()
    {
        scoreUI.text = scoring.score.ToString();
    }
}
