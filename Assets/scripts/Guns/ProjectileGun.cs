using System.Collections;
using UnityEngine;
using Extensions;
using Audio;
using Pool;

namespace Guns
{
    public class ProjectileGun : GunBase
    {
        [SerializeField] private GameObject _projectile;

        [SerializeField] private GameObject _mainEntity;

        private Coroutine _shootingCR;
        private PoolingItemType _projectileType;

        protected override void Awake()
        {
            if (_mainEntity.ContainsComponent<Player>())
            {
                _projectileType = PoolingItemType.PlayerProjectile;
            }
            else
            {
                _projectileType = PoolingItemType.EnemyProjectile;
            }

            base.Awake();
        }

        public override void StartShoot()
        {
            if (_shooting == false)
            {
                if (_transform == null)
                {
                    _transform = transform;
                }
                _shootingCR = StartCoroutine(Shooting());
            }
        }

        protected IEnumerator Shooting()
        {
            _shooting = true;
            while (_shooting)
            {
                Shoot();
                yield return new WaitForSeconds(_shootCooldown);
            }
        }

        public override void Shoot()
        {
            if (_timeBetweenShots <= 0.0f)
            {
                _timeBetweenShots = _shootCooldown;

                GameObject projectile = PoolManager.Instance.GetPoolingObjectByType(_projectileType);
                projectile.transform.position = _transform.position;

                projectile.transform.rotation = Quaternion.identity;
                projectile.transform.Rotate(new Vector3(90f, 0f, 0f));

                projectile.HandleComponent<ProjectileBase>((component) =>
                {
                    component.SetDirection(_direction);
                });

                if (_mainEntity != null)
                {
                    _mainEntity.HandleComponent<AudioSourceUtility>((component) =>
                    {
                        component.PlayOneShot(ClipType.Shot);
                    });
                }
            }
        }

        public override void StopShoot()
        {
            if (_shootingCR != null)
            {
                StopCoroutine(_shootingCR);
            }
            _shooting = false;
        }
    }
}
