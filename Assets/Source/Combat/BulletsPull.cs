using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BulletsPull : MonoBehaviour
{
    [Header("Pull Settings")]
    [SerializeField] private Transform bulletsParentObject;
    
    [Header("Bullets")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private uint bulletsCount;

    private Queue<Bullet> _bulletsPull = new Queue<Bullet>();

    private void Awake()
    {
        for (int bulletsNum = 0; bulletsNum < bulletsCount; bulletsNum++)
        {
            Bullet tempBullet = Instantiate(bulletPrefab, bulletsParentObject).GetComponent<Bullet>();
            tempBullet.OnBulletDisappear += ReturnBullet;
            
            tempBullet.gameObject.SetActive(false);
            
            _bulletsPull.Enqueue(tempBullet);
        }
    }

    public Bullet GetBullet()
    {
        Bullet bulletToReturn = _bulletsPull.Dequeue();
        return bulletToReturn;
    }

    private void ReturnBullet(Bullet bulletToReturn)
    {
        _bulletsPull.Enqueue(bulletToReturn);
    }
    
}
