using Pool;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class WaweSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _spawnPoints;
    [Range(1, 5)]
    [SerializeField] private int _obstaclesNumberInOneWawe;
    [SerializeField] private float _timeBetweenWawes;

    private float _timeFromLastWawe;

    private void Awake()
    {
        if (_spawnPoints == null)
        {
            throw new Exception("spawn points null exception");
        }

        _timeFromLastWawe = 0.0f;
    }

    private void Update()
    {
        if (_timeFromLastWawe > 0)
        {
            _timeFromLastWawe -= Time.deltaTime;
        }
        else
        {
            _timeFromLastWawe = _timeBetweenWawes;
            SpawnWawe();
        }
    }

    private async void SpawnWawe()
    {
        for (int i = 0; i < _obstaclesNumberInOneWawe; i++)
        {
            SpawnByIndex(UnityEngine.Random.Range(0, _spawnPoints.Length));
            await Task.Delay(300);
        }
    }

    private void SpawnByIndex(int index)
    {
        GameObject obstacle = PoolManager.Instance.GetPoolingObjectByType(PoolingItemType.Obstacle);
        
        obstacle.transform.position = _spawnPoints[index].transform.position;
    }
}
