using Extensions;
using Pool;
using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Destroyable : MonoBehaviour
{
    //settings
    [Header("Health settings")]
    [SerializeField] private bool _hasDeathEffect = false;
    [SerializeField] private int _maxHealth;
    private int _health;

    [Header("Application settings")]
    [SerializeField] private bool _isPoolObject;
    [SerializeField] private GameObject _entity;

    public int MaxHealth => _maxHealth;
    public int Health => _health;

    public GameObject Entity => _entity;

    //DamageEvents
    public event Action<int> OnHealthChanged;
    public event Action OnDeath;

    private void Start()
    {
        if (_entity == null)
        {
            throw new Exception("Damageable Entity null exception");
        }
        _health = _maxHealth;
        ApplyDamage(0);
    }

    public void ApplyDamage(int damageAmount)
    {
        _health -= damageAmount;

        OnHealthChanged?.Invoke(_health);

        if (_health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        if (_hasDeathEffect)
        {
            GameObject explosion = PoolManager.Instance.GetPoolingObjectByType(PoolingItemType.Explosion);
            explosion.transform.position = transform.position;
            explosion.transform.rotation = transform.rotation;
        }

        OnDeath?.Invoke();
        Destroy();
    }

    public void Destroy()
    {
        if (_isPoolObject)
        {
            ApplyDamage(-_maxHealth);
            _entity.HandleComponent<PoolObject>((component) =>
            {
                component.ReturnToPool();
            });
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
