using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletSettingsSO", menuName = "SOs/Player/BulletSettingsSO")]
public class BulletSettingsSO : ScriptableObject
{
    [field: SerializeField] public float TravelSpeed { get; private set; }
}
