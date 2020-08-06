using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Guns
{
    public abstract class GunBase : MonoBehaviour
    {
        //settings
        [Header("weapon settings")]
        [SerializeField] protected float _shootCooldown;
        protected float _timeBetweenShots;

        //cache
        protected Transform _transform;
        protected Vector3 _direction;

        protected bool _shooting;
        public bool IsShooting => _shooting;

        protected virtual void Awake()
        {
            _timeBetweenShots = 0.0f;
            _transform = transform;
        }

        protected virtual void Update()
        {
            if (_timeBetweenShots > 0)
            {
                _timeBetweenShots -= Time.deltaTime;
            }
        }

        public abstract void StartShoot();

        public abstract void Shoot();

        public void SetShootDirection(Vector3 direction)
        {
            _direction = direction;
        }

        public abstract void StopShoot();
    }
}
