using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidProjectile : ProjectileBase
{
    protected override void Awake()
    {
        base.Awake();
        SetDirection(Vector3.back);
    }
}
