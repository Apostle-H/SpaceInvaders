using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseView : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject losePanel;
    [SerializeField] private TextMeshProUGUI loseTextUI;
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private Button restartBtn;

    [Header("Texts")]
    [SerializeField] private string loseText;
    
    private void Awake()
    {
        restartBtn.onClick.AddListener(Game.Restart);
    }

    public void Lose(int score)
    {
        if (losePanel.activeSelf)
        {
            return;
        }
        
        losePanel.SetActive(true);

        loseTextUI.text = loseText;
        scoreUI.text += score.ToString();
    }
}
