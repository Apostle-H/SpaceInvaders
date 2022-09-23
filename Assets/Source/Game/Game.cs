using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [Header("Scripts")] 
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private Player player;
    
    [SerializeField] private EnemyArmy enemyArmy;
    [SerializeField] private LoseZone loseZone;
    
    [Header("UI")]
    [SerializeField] private WinView winView;
    [SerializeField] private LoseView loseView;

    public GameState GameState { get; private set; }

    private void Awake()
    {
        Play();
        inputHandler.GameInput.restart.started += (ctx) => Restart();
        
        enemyArmy.OnArmyDefeat += Win;

        loseZone.OnTriggered += Lose;
        player.OnDie += Lose;
    }

    private void Win()
    {
        Stop();
        
        winView.Win(Scoring.Score);
    }

    private void Lose()
    {
        Stop();

        loseView.Lose(Scoring.Score);
    }

    private void Play()
    {
        GameState = GameState.play;
    }

    public static void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Stop()
    {
        GameState = GameState.stop;

        inputHandler.enabled = false;
        enemyArmy.enabled = false;
    }
}
