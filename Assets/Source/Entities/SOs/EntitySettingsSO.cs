using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySettingsSO : ScriptableObject
{
    [field: SerializeField] public int HP { get; private set; }
}
