using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private PlayerInvoker invoker;
    [SerializeField] private BulletsPull bulletsPull;
    
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform firePoint;

    [Header("Player Settings")] 
    [SerializeField] private PlayerSettingsSO playerSettingsSO;
    
    private MovementCommand _movementCommand;
    private ShootCommand _shootCommand;
    
    private void Awake()
    {
        _movementCommand = new MovementCommand();
        _shootCommand = new ShootCommand();
    }

    public void PerformMove(InputAction.CallbackContext ctx)
    {
        Vector2 direction = Vector2.right * ctx.ReadValue<float>();
        _movementCommand.Move(rb, direction, playerSettingsSO.MovementSpeed);
    }

    public void PerformStop(InputAction.CallbackContext ctx)
    {
        _movementCommand.Stop(rb);
    }

    public void PerformShoot(InputAction.CallbackContext ctx)
    {
        _shootCommand.Shoot(bulletsPull.GetBullet(), firePoint);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
