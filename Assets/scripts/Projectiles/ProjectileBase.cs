using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    //settings
    [Header("projectile settings")]
    [SerializeField] protected float _speed;
    [SerializeField] protected int _damage;
    
    //cache
    protected Transform _transform;
    protected Vector3 _direction;

    public int Damage => _damage;

    protected virtual void Awake()
    {
        _transform = transform;
    }

    protected virtual void FixedUpdate()
    {
        _transform.position = Vector3.MoveTowards(_transform.position, _transform.position + _direction * _speed * Time.fixedDeltaTime, _speed);
    }

    public void SetDirection(Vector3 newDirection)
    {
        _direction = newDirection;
    }
}
