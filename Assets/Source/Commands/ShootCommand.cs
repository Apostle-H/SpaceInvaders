using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCommand
{
    public void Shoot(Bullet bullet, Transform firePoint)
    {
        bullet.ShootSelf(firePoint);
    }
}
