using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyArmy : MonoBehaviour
{
    [Header("Army Settings")] 
    [SerializeField] private float moveDownTimeDelay;
    [SerializeField] private float shootTimeDelayMin;
    [SerializeField] private float shootTimeDelayMax;

    [Space(15f)]
    
    [SerializeField] private bool randomArmySize;
    [SerializeField] [Range(1, 15)] private int collumns;
    [SerializeField] [Range(1, 5)] private int rows;
    
    [SerializeField] private GameObject[] enemiesPrefabs;

    public event VoidHandler OnArmyDefeat;
    
    private Enemy _lastEnemy;
    private EnemiesPull _enemiesPull;
    private List<Vector2> _frontEnemiesPos = new List<Vector2>();
    
    private void Awake()
    {
        if (randomArmySize)
        {
            collumns = Random.Range(1, 16);
            rows = Random.Range(1, 5);
        }
        
        _enemiesPull = new EnemiesPull(transform, collumns, rows, enemiesPrefabs);
        _enemiesPull.Init();
        
        CalcualteFront();

        _frontEnemiesPos.ForEach(frontEnemyPos => _enemiesPull.Enemies[(int)frontEnemyPos.x, (int)frontEnemyPos.y].OnDie += 
            (score) => PassFrontPos((int)frontEnemyPos.x, (int)frontEnemyPos.y));
        _enemiesPull.Enemies.ForEach(enemy => enemy.OnDie += OneEnemyDie);
    }
    
    private void OnEnable()
    {
        MoveDownArmyHolder();
        StartCoroutine(Shoot());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        _enemiesPull.Enemies.ForEach(enemy => MoveEnemy(enemy, Vector2.zero));
    }

    #region ArmyMove

    private IEnumerator MoveDownArmy()
    {
        yield return new WaitForSeconds(moveDownTimeDelay);

        if (_lastEnemy)
        {
            _lastEnemy.OnStop -= MoveDownArmyHolder;
        }

        _enemiesPull.Enemies.ForEach(enemy => MoveEnemy(enemy, Vector2.down));

        _lastEnemy.OnStop += MoveDownArmyHolder;
    }

    private void MoveEnemy(Enemy enemy, Vector2 direction)
    {
        if (!enemy.IsAlive)
        {
            return;
        }
                
        enemy.PerformMove(direction);
        _lastEnemy = enemy;
    }

    private void MoveDownArmyHolder()
    {
        StartCoroutine(MoveDownArmy());
    }


    #endregion

    #region ArmyShoot

    private void CalcualteFront()
    {
        for (int rowIndex = _enemiesPull.Enemies.GetLength(1) - 1; rowIndex < _enemiesPull.Enemies.GetLength(1); rowIndex++)
        {
            for (int collumnIndex = 0; collumnIndex < _enemiesPull.Enemies.GetLength(0); collumnIndex++)
            {
                _frontEnemiesPos.Add(new Vector2(collumnIndex, rowIndex));
            }
        }
    }

    private IEnumerator Shoot()
    {
        float waitTime = Random.Range(shootTimeDelayMin, shootTimeDelayMax);
        yield return new WaitForSeconds(waitTime);

        Vector2 randomFrontEnemyPos = _frontEnemiesPos[Random.Range(0, _frontEnemiesPos.Count)];
        Enemy randomFrontEnemy =  _enemiesPull.Enemies[(int)randomFrontEnemyPos.x, (int)randomFrontEnemyPos.y];
        
        randomFrontEnemy.PerformShoot();

        StartCoroutine(Shoot());
    }
    
    #endregion

    #region ArmyDeaths

    private void OneEnemyDie(int scoreToAdd)
    {
        Scoring.AddScore(scoreToAdd);
        CheckIfArmyDefeat();
    }
    
    private void PassFrontPos(int collumnIndex, int rowIndex)
    {
        _frontEnemiesPos.Remove(new Vector2(collumnIndex, rowIndex));

        if (rowIndex - 1 < 0)
        {
            return;
        }
        
        _frontEnemiesPos.Add(new Vector2(collumnIndex, rowIndex - 1));
        _enemiesPull.Enemies[collumnIndex, rowIndex - 1].OnDie += 
            (score) => PassFrontPos(collumnIndex,rowIndex - 1);
    }

    private void CheckIfArmyDefeat()
    {
        if (_enemiesPull.Enemies.Any(enemy => enemy.IsAlive))
        {
            return;
        }
        
        OnArmyDefeat?.Invoke();
    }

    #endregion
}
