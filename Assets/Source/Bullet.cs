using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private BulletSettingsSO bulletSettingsSO;

    public event BulletHandler OnBulletDisappear;

    private void OnBecameInvisible()
    {
        OnBulletDisappear?.Invoke(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnBulletDisappear?.Invoke(this);
    }

    public void ShootSelf(Transform firePoint)
    {
        transform.position = firePoint.position;
        transform.rotation = firePoint.rotation;

        rb.velocity = transform.up * bulletSettingsSO.TravelSpeed;
    }
}
