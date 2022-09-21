using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinView : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMeshProUGUI winTextUI;
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private Button restartBtn;

    [Header("Texts")]
    [SerializeField] private string winText;

    private void Awake()
    {
        restartBtn.onClick.AddListener(Game.Restart);
    }

    public void Win(int score)
    {
        if (winPanel.activeSelf)
        {
            return;
        }

        
        winPanel.SetActive(true);

        winTextUI.text = winText;
        scoreUI.text += score.ToString();
    }
}
