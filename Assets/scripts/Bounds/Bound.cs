using Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Bound : MonoBehaviour
{
    private void Reset()
    {
        var collider = GetComponent<Collider>();
        if (collider != null)
            collider.isTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.ContainsComponent<Destroyable>())
        {
            int damage = other.gameObject.ContainsComponent<Player>() ? 1 : 100;

            other.gameObject.HandleComponent<Destroyable>((component) =>
            {
                component.Destroy();
            });
        }
    }
}
