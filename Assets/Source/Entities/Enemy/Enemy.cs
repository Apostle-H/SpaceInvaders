using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEntity
{
    #region SerializedFields

    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D collider;
    [SerializeField] private Renderer renderer;
    [SerializeField] private Transform firePoint;

    [Header("Enemy Settings")]
    [SerializeField] private EnemySettingsSO enemySettingsSO;

    #endregion

    #region Events
    
    public event VoidHandler OnStop;
    public event IntHandler OnDie;
    
    #endregion

    #region Commands
    
    private MovementCommand _movementCommand;
    private ShootCommand _shootCommand;
    
    #endregion

    public bool IsAlive { get; private set; } = true;

    private float _moveStartPosY;

    private int _currentHP;

    protected void Awake()
    {
        _movementCommand = new MovementCommand();
        _shootCommand = new ShootCommand();

        _currentHP = enemySettingsSO.HP;
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude < enemySettingsSO.MovementSpeed)
        {
            return;
        }

        float pathLenght = _moveStartPosY - transform.position.y;
        if (Mathf.Abs(pathLenght) >= 1f)
        {
            _movementCommand.Stop(rb);
            transform.position = new Vector2(transform.position.x, _moveStartPosY - 1f);
            OnStop?.Invoke();
        }
    }

    public void PerformMove(Vector2 direction)
    {
        _moveStartPosY = Mathf.Ceil(transform.position.y);
        _movementCommand.Move(rb, direction, enemySettingsSO.MovementSpeed);
    }
    
    public void PerformShoot()
    {
        _shootCommand.Shoot(Instantiate(enemySettingsSO.BulletPrefab).GetComponent<Bullet>(), firePoint);
    }

    public void TakeDamage(int damage)
    {
        _currentHP -= damage;

        if (_currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        IsAlive = false;
        OnDie?.Invoke(enemySettingsSO.ScoreOnDefeat);

        renderer.enabled = false;
        collider.enabled = false;
    }
}
