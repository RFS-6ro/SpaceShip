using Guns;
using UnityEngine;

namespace GameInput
{
    public class GunInput : MonoBehaviour
    {
        [SerializeField] private GunBase _gun;

        //cache
        private JoyStickController _controller;

        private void Start()
        {
            if (_gun == null)
            {
                throw new System.Exception("Gun null exception");
            }

            _controller = JoyStickController.Instance;
        }
        private void Update()
        {
            if (_gun == null)
            {
                throw new System.Exception("Gun null exception");
            }

            _gun.SetShootDirection(Vector3.forward);

            //_gun.Shoot();

#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_gun.IsShooting == false)
                {
                    _gun.StartShoot();
                }
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (_gun.IsShooting)
                {
                    _gun.StopShoot();
                }
            }
#endif
        }

        public void SetTargetGun(GunBase gun)
        {
            if (gun != null)
            {
                _gun = gun;
            }
        }


    }
}
