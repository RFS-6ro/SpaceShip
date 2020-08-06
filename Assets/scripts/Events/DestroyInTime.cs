using Extensions;
using Pool;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class DestroyInTime : MonoBehaviour
{
    [SerializeField] private float _lifetime;
    [SerializeField] private bool _isPoolObject;
    
    private async void DestroySelf()
    {
        await Task.Delay(Convert.ToInt32(_lifetime * 1000));
        if (_isPoolObject)
        {
            gameObject.HandleComponent<PoolObject>((component) => component.ReturnToPool());
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
