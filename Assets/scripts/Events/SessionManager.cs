using Audio;
using System;
using UI;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    //UI
    [Header("UI")]
    [SerializeField] private UIViewController _controller;

    //player
    [Header("Player")]
    [SerializeField] private Player _player;
    private Destroyable _playerDestroyable;

    //win condition
    [Header("Win condition")]
    [SerializeField] private int _destroyedObstaclesToWin;
    private int _alreadyDestroyedObstacles;

    [SerializeField] private AudioSourceUtility _source;

    public int DestroyedObstaclesToWin => _destroyedObstaclesToWin;

    public event Action<int> OnWinConditionValueChanged;
    public event Action OnLevelFinish;
    
    private void Start()
    {
        OnWinConditionValueChanged?.Invoke(_alreadyDestroyedObstacles);

        if (_controller == null)
        {
            _controller = FindObjectOfType<UIViewController>();
        }

        if (_player == null)
        {
            _player = FindObjectOfType<Player>();
        }

        _playerDestroyable = _player.GetComponent<Destroyable>();

        _playerDestroyable.OnDeath += OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        _source.PlayOneShot(ClipType.Lose);
        OnLevelFinish?.Invoke();
        _controller.TurnUIPageOn(PageType.Lose, true, () => Time.timeScale = 0.0f);
    }

    public void AddObstacleDestroying()
    {
        ++_alreadyDestroyedObstacles;
        OnWinConditionValueChanged?.Invoke(_alreadyDestroyedObstacles);
        if (_alreadyDestroyedObstacles >= _destroyedObstaclesToWin)
        {
            OnPlayerWin();
        }
    }

    private void OnPlayerWin()
    {
        _source.PlayOneShot(ClipType.Win);
        OnLevelFinish?.Invoke();
        _controller.TurnUIPageOn(PageType.Win, true, () => Time.timeScale = 0.0f);
    }

    private void OnDisable()
    {
        if (_playerDestroyable != null && _player != null)
        {
            _playerDestroyable.OnDeath -= OnPlayerDeath;
        }
    }
}
