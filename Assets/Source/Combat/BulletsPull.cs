using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BulletsPull
{
    private Transform _bulletsParentObject;
    
    private GameObject _bulletPrefab;
    private int _bulletsCount;

    private Queue<Bullet> _bulletsPull = new Queue<Bullet>();

    public BulletsPull(Transform bulletsParentObject, GameObject bulletPrefab, int bulletCount)
    {
        _bulletsParentObject = bulletsParentObject;
        _bulletPrefab = bulletPrefab;
        _bulletsCount = bulletCount;
    }
    
    public void Init()
    {
        for (int bulletsNum = 0; bulletsNum < _bulletsCount; bulletsNum++)
        {
            Bullet tempBullet = GameObject.Instantiate(_bulletPrefab, _bulletsParentObject).GetComponent<Bullet>();
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
