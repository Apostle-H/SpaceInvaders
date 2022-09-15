using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettingsSO", menuName = "SOs/Player/PlayerSettingsSO")]
public class PlayerSettingsSO : ScriptableObject
{
    [field: SerializeField] public float MovementSpeed { get; private set; }
}
