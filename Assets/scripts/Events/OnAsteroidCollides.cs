using UnityEngine;
using Extensions;
using System.Threading.Tasks;
using System;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Destroyable))]
public class OnAsteroidCollides : MonoBehaviour
{   
    [SerializeField] private AsteroidProjectile _entity;

    private void Awake()
    {
        if (_entity == null)
        {
            throw new Exception("Asteroid proj null exception");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.ContainsComponent<Player>())
        {
            int damage = _entity.GetComponent<AsteroidProjectile>().Damage;
            collision.gameObject.HandleComponent<Destroyable>((component) =>
            {
                component.ApplyDamage(damage);
            });

            DestroySelf();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.ContainsComponent<Bound>())
        {
            DestroySelf(1.5f);
        }
    }

    private async void DestroySelf(float time = 0f)
    {
        await Task.Delay(Convert.ToInt32(time * 1000));
        
        gameObject.HandleComponent<Destroyable>((component) =>
        {
            component.ApplyDamage(component.Health);
        });
    }
}
