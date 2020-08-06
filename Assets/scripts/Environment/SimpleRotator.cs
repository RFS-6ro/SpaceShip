using UnityEngine;

public class SimpleRotator : MonoBehaviour
{
    [Header("Rotating Settings")]
    [SerializeField] private float _rotateAnglePerFrame;
    [SerializeField] private bool _isRandomRotation;
    [SerializeField] private bool _isRightRotation;

    //cache
    private Transform _transform;
    private Quaternion _quaternion;
    private int _rotationSide;

    public float RotateSpeed { get => _rotateAnglePerFrame; set => _rotateAnglePerFrame = value; }

    private void Awake()
    {
        _transform = transform;
        _transform.Rotate(new Vector3(0f, Random.Range(0f, 90f), 0f), Space.Self);

        _rotationSide = 0;
        if (_isRandomRotation)
        {
            while (_rotationSide == 0)
            {
                _rotationSide = Random.Range(-1, 2);
            }
        }
        else
        {
            _rotationSide = _isRightRotation ? 1 : -1;
        }
    }

    private void Update()
    {
        _transform.Rotate(new Vector3(0f, _rotateAnglePerFrame * _rotationSide, 0f), Space.Self);
    }
}
