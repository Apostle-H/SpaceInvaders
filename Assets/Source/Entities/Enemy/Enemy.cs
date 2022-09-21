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

    public bool isAlive { get; private set; } = true;

    private float moveStartPosY;

    private int currentHP;

    protected void Awake()
    {
        _movementCommand = new MovementCommand();
        _shootCommand = new ShootCommand();

        currentHP = enemySettingsSO.HP;
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude < enemySettingsSO.MovementSpeed)
        {
            return;
        }

        float pathLenght = moveStartPosY - transform.position.y;
        if (Mathf.Abs(pathLenght) >= 1f)
        {
            _movementCommand.Stop(rb);
            transform.position = new Vector2(transform.position.x, moveStartPosY - 1f);
            OnStop?.Invoke();
        }
    }

    public void PerformMove(Vector2 direction)
    {
        moveStartPosY = Mathf.Ceil(transform.position.y);
        _movementCommand.Move(rb, direction, enemySettingsSO.MovementSpeed);
    }
    
    public void PerformShoot()
    {
        _shootCommand.Shoot(Instantiate(enemySettingsSO.BulletPrefab).GetComponent<Bullet>(), firePoint);
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isAlive = false;
        OnDie?.Invoke(enemySettingsSO.ScoreOnDefeat);

        renderer.enabled = false;
        collider.enabled = false;
    }
}
