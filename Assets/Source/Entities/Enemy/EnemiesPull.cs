using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesPull : MonoBehaviour
{
    [Header("Army Settings")] 
    [SerializeField] [Range(1, 15)] private int collumns;
    [SerializeField] [Range(1, 5)] private int rows;
    
    [SerializeField] private GameObject[] enemiesPrefabs;

    public Enemy[,] Enemies { get; private set; }

    private void Awake()
    {
        Enemies = new Enemy[collumns, rows];

        for (int rowIndex = 0; rowIndex < Enemies.GetLength(1); rowIndex++)
        {
            int randomEnemyPrefabIndex = Random.Range(0, enemiesPrefabs.Length);
            for (int collumnIndex = 0; collumnIndex < Enemies.GetLength(0); collumnIndex++)
            {
                Enemy tempEnemy = Instantiate(enemiesPrefabs[randomEnemyPrefabIndex], transform).GetComponent<Enemy>();
                
                Vector2 enemyPos = CalcualteEnemyPos(collumnIndex, rowIndex);
                tempEnemy.transform.position = enemyPos;

                Enemies[collumnIndex, rowIndex] = tempEnemy;
            }
        }
    }
    
    private Vector2 CalcualteEnemyPos(int collumnIndex, int rowIndex)
    {
        bool areEnemiesEven = collumns % 2 == 0;
        
        float enemyPosX = transform.position.x + (collumnIndex - (collumns / 2)) + (areEnemiesEven ? 0.5f : 0);
        float enemyPosY = transform.position.y - rowIndex;

        return new Vector2(enemyPosX, enemyPosY);
    }
}
