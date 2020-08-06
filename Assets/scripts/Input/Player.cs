using GameInput;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player settings")]
    [SerializeField] private float _speed = 5f;
    
    //cache
    private Transform _transform;
    private JoyStickController _controller;


    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        _controller = JoyStickController.Instance;
    }

    private void Update()
    {
        _transform.position += new Vector3(_controller.Horizontal(), 0f, _controller.Vertical()) * Time.deltaTime * _speed;
    }
}
