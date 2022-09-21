using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyArmy : MonoBehaviour
{
    [Header("Scripts")] 
    [SerializeField] private EnemiesPull enemiesPull;
    [SerializeField] private Scoring scoring;

    [SerializeField] private float moveDownTimeDelay;
    [SerializeField] private float shootTimeDelayMin;
    [SerializeField] private float shootTimeDelayMax;

    public event VoidHandler OnArmyDefeat;
    
    private Enemy _lastEnemy;
    private List<Vector2> _frontEnemiesPos = new List<Vector2>();
    
    private void Awake()
    {
        CalcualteFront();

        _frontEnemiesPos.ForEach(frontEnemyPos => enemiesPull.Enemies[(int)frontEnemyPos.x, (int)frontEnemyPos.y].OnDie += 
            (score) => PassFrontPos((int)frontEnemyPos.x, (int)frontEnemyPos.y));
        enemiesPull.Enemies.ForEach(enemy => enemy.OnDie += OneEnemyDie);
    }

    private void OnEnable()
    {
        MoveDownArmyHolder();
        StartCoroutine(Shoot());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        enemiesPull.Enemies.ForEach(enemy => MoveEnemy(enemy, Vector2.zero));
    }

    #region ArmyMove

    private IEnumerator MoveDownArmy()
    {
        yield return new WaitForSeconds(moveDownTimeDelay);

        if (_lastEnemy)
        {
            _lastEnemy.OnStop -= MoveDownArmyHolder;
        }

        enemiesPull.Enemies.ForEach(enemy => MoveEnemy(enemy, Vector2.down));

        _lastEnemy.OnStop += MoveDownArmyHolder;
    }

    private void MoveEnemy(Enemy enemy, Vector2 direction)
    {
        if (!enemy.isAlive)
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
        for (int rowIndex = enemiesPull.Enemies.GetLength(1) - 1; rowIndex < enemiesPull.Enemies.GetLength(1); rowIndex++)
        {
            for (int collumnIndex = 0; collumnIndex < enemiesPull.Enemies.GetLength(0); collumnIndex++)
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
        Enemy randomFrontEnemy =  enemiesPull.Enemies[(int)randomFrontEnemyPos.x, (int)randomFrontEnemyPos.y];
        
        randomFrontEnemy.PerformShoot();

        StartCoroutine(Shoot());
    }
    
    #endregion

    #region ArmyDeaths

    private void OneEnemyDie(int scoreToAdd)
    {
        scoring.AddScore(scoreToAdd);
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
        enemiesPull.Enemies[collumnIndex, rowIndex - 1].OnDie += 
            (score) => PassFrontPos(collumnIndex,rowIndex - 1);
    }

    private void CheckIfArmyDefeat()
    {
        if (enemiesPull.Enemies.Any(enemy => enemy.isAlive))
        {
            return;
        }
        
        OnArmyDefeat?.Invoke();
    }

    #endregion
}
