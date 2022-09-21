using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInvoker : MonoBehaviour
{
    [Header("Scripts")] 
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private Player player;
    

    private void OnEnable()
    {
        inputHandler.PlayerInput.movement.started += player.PerformMove;
        inputHandler.PlayerInput.movement.canceled += player.PerformStop;

        inputHandler.PlayerInput.shoot.started += player.PerformShoot;
    }

    private void OnDisable()
    {
        inputHandler.PlayerInput.movement.started -= player.PerformMove;
        inputHandler.PlayerInput.movement.canceled -= player.PerformStop;
        
        inputHandler.PlayerInput.shoot.started -= player.PerformShoot;
    }
}
