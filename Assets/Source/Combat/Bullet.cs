using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private BulletSettingsSO bulletSettingsSO;

    public event BulletHandler OnBulletDisappear;

    private void OnBecameInvisible()
    {
        Return();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(bulletSettingsSO.TargetTag))
        {
            IEntity target = col.GetComponent<IEntity>();
            target.TakeDamage(bulletSettingsSO.Damage);
            
            Return();
        }
    }

    public void ShootSelf(Transform firePoint)
    {
        gameObject.SetActive(true);
        
        transform.position = firePoint.position;
        transform.rotation = firePoint.rotation;

        rb.velocity = transform.up * bulletSettingsSO.TravelSpeed;
    }

    private void Return()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        
        gameObject.SetActive(false);
        OnBulletDisappear?.Invoke(this);
    }
}
