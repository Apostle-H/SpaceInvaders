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
    [SerializeField] private PlayerView playerView;
    
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform firePoint;

    [Header("Player Settings")] 
    [SerializeField] private PlayerSettingsSO playerSettingsSO;
    
    [Header("Bullet Pull Settings")]
    [SerializeField] private Transform bulletsParentObject;
    
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int bulletsCount;

    public event VoidHandler OnDie;
    
    private BulletsPull _bulletsPull;
    
    private MovementCommand _movementCommand;
    private ShootCommand _shootCommand;

    private int _currentHP;
    
    private void Awake()
    {
        _bulletsPull = new BulletsPull(bulletsParentObject, bulletPrefab, bulletsCount);
        _bulletsPull.Init();
        
        _movementCommand = new MovementCommand();
        _shootCommand = new ShootCommand();

        _currentHP = playerSettingsSO.HP;
        playerView.DrawHealth(_currentHP);
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
        _shootCommand.Shoot(_bulletsPull.GetBullet(), firePoint);
    }

    public void TakeDamage(int damage)
    {
        _currentHP -= damage;
        playerView.UpdateHealth(_currentHP);

        if (_currentHP <= 0)
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
