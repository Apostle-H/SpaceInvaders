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
    [SerializeField] private PlayerInvoker playerInvoker;
    
    [SerializeField] private EnemyArmy enemyArmy;
    [SerializeField] private LoseZone loseZone;
    
    [SerializeField] private Scoring scoring;

    [Header("UI")]
    [SerializeField] private WinView winView;
    [SerializeField] private LoseView loseView;

    public GameState _gameState { get; private set; }

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
        
        winView.Win(scoring.score);
    }

    private void Lose()
    {
        Stop();

        loseView.Lose(scoring.score);
    }

    private void Play()
    {
        _gameState = GameState.play;
    }

    public static void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Stop()
    {
        _gameState = GameState.stop;

        playerInvoker.enabled = false;
        enemyArmy.enabled = false;
    }
}
