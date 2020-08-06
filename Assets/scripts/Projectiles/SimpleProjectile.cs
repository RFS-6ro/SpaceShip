using Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : ProjectileBase
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.ContainsComponent<Destroyable>())
        {
            collision.gameObject.HandleComponent<Destroyable>((component) =>
            {
                if (component.Entity.ContainsComponent<AsteroidProjectile>() || 
                        component.Entity.ContainsComponent<EnemyBehaviour>())
                {
                    if (_damage >= component.Health)
                    {
                        FindObjectOfType<SessionManager>().AddObstacleDestroying();
                    }
                }

                component.ApplyDamage(_damage);
            });
            
            gameObject.HandleComponent<Destroyable>((component) =>
            {
                component.ApplyDamage(_damage);
            });
        }
    }
}
