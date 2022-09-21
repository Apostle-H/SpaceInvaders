using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Player : MonoBehaviour, IEntity
{
    [Header("Scripts")]
    [SerializeField] private PlayerInvoker invoker;
    [SerializeField] private BulletsPull bulletsPull;
    [SerializeField] private PlayerView playerView;
    
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform firePoint;

    [Header("Player Settings")] 
    [SerializeField] private PlayerSettingsSO playerSettingsSO;

    public event VoidHandler OnDie;
    
    private MovementCommand _movementCommand;
    private ShootCommand _shootCommand;

    private int currentHP;
    
    private void Awake()
    {
        _movementCommand = new MovementCommand();
        _shootCommand = new ShootCommand();

        currentHP = playerSettingsSO.HP;
        playerView.DrawHealth(currentHP);
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

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        playerView.UpdateHealth(currentHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
        OnDie?.Invoke();
    }
}
