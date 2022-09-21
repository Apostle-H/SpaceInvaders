using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseZone : MonoBehaviour
{
    [SerializeField] private string triggerTag;

    public event VoidHandler OnTriggered;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag(triggerTag))
        {
            return;
        }
        
        OnTriggered?.Invoke();
    }
}
