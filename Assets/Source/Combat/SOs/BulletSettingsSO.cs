using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletSettingsSO", menuName = "SOs/Combat/BulletSettingsSO")]
public class BulletSettingsSO : ScriptableObject
{
    [field: SerializeField] public float TravelSpeed { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    
    [field: SerializeField] public string TargetTag { get; private set; }
}
