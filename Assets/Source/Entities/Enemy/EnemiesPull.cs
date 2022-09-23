using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesPull
{
    private Transform _enemiesParentObject;
    
    private int _collumns;
    private int _rows;
    
    private GameObject[] _enemiesPrefabs;

    public Enemy[,] Enemies { get; private set; }

    public EnemiesPull(Transform enemiesParentObject, int collumns, int rows, GameObject[] enemiesPrefabs)
    {
        _enemiesParentObject = enemiesParentObject;
        _collumns = collumns;
        _rows = rows;
        _enemiesPrefabs = enemiesPrefabs;
    }
    
    public void Init()
    {
        Enemies = new Enemy[_collumns, _rows];

        for (int rowIndex = 0; rowIndex < Enemies.GetLength(1); rowIndex++)
        {
            int randomEnemyPrefabIndex = Random.Range(0, _enemiesPrefabs.Length);
            for (int collumnIndex = 0; collumnIndex < Enemies.GetLength(0); collumnIndex++)
            {
                Enemy tempEnemy = GameObject.Instantiate(_enemiesPrefabs[randomEnemyPrefabIndex], _enemiesParentObject.transform).GetComponent<Enemy>();
                
                Vector2 enemyPos = CalcualteEnemyPos(collumnIndex, rowIndex);
                tempEnemy.transform.position = enemyPos;

                Enemies[collumnIndex, rowIndex] = tempEnemy;
            }
        }
    }
    
    private Vector2 CalcualteEnemyPos(int collumnIndex, int rowIndex)
    {
        bool areEnemiesEven = _collumns % 2 == 0;
        
        float enemyPosX = _enemiesParentObject.transform.position.x + (collumnIndex - (_collumns / 2)) + (areEnemiesEven ? 0.5f : 0);
        float enemyPosY = _enemiesParentObject.transform.position.y - rowIndex;

        return new Vector2(enemyPosX, enemyPosY);
    }
}
