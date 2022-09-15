using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-100)]
public class InputHandler : MonoBehaviour
{
    private MainInputAction _mainInputAction;
    
    public MainInputAction.PlayerInputActions PlayerInput { get { return _mainInputAction.playerInput; } }

    private void Awake()
    {
        _mainInputAction = new MainInputAction();
    }

    private void OnEnable()
    {
        _mainInputAction.Enable();
    }

    private void OnDisable()
    {
        _mainInputAction.Disable();
    }
}
