using Guns;
using UnityEngine;

[RequireComponent(typeof(Destroyable))]
public class EnemyBehaviour : MonoBehaviour
{
    //settings
    [Header("ship settings")]
    [SerializeField] protected float _speed;
    [SerializeField] protected GunBase _gun;

    //cache
    protected Transform _transform;
    protected Vector3 _direction;

    private void Awake()
    {
        if (_gun == null)
        {
            throw new System.Exception("Enemy gun null exception");
        }

        _gun.SetShootDirection(Vector3.back);

        _transform = transform;
        SetDirection(Vector3.back);
    }

    private void FixedUpdate()
    {
        _gun.Shoot();
        _transform.position = Vector3.MoveTowards(_transform.position, _transform.position + _direction * _speed * Time.fixedDeltaTime, _speed);
    }

    public void SetDirection(Vector3 newDirection)
    {
        _direction = newDirection;
    }
}
