using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettingsSO", menuName = "SOs/Enemies/EnemySettingsSO")]
public class EnemySettingsSO : EntitySettingsSO
{
    [field: SerializeField] public GameObject BulletPrefab { get; private set; }
    [field: SerializeField] public int ScoreOnDefeat { get; private set; }
}
