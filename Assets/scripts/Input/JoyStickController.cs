using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GameInput
{
    public class JoyStickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] private Image _joyStickBG;
        [SerializeField] private Image _joyStickTG;
        private Transform _transform;

        private Vector2 _inputVector2;

        [SerializeField] private Vector2 _fullScreenSize;
        [SerializeField] private Vector2 _normalSize;

        private Vector2 _canvasCenter;

        private bool _inputLost;

        public static JoyStickController Instance
        {
            get;
            protected set;
        }

        private void Awake()
        {
            Instance = this;
            _transform = transform;
        }

        private void Start()
        {
            _canvasCenter = new Vector2(_fullScreenSize.x / 2, _fullScreenSize.y / 2);
            _inputLost = false;

            _joyStickBG.color = Color.clear;
            _joyStickTG.color = Color.clear;

            _normalSize = _joyStickBG.rectTransform.sizeDelta;

            _joyStickBG.rectTransform.localPosition = Vector2.zero;
            _transform.position = _canvasCenter;
            SetJoystickBackgroundSize(new Vector2(_fullScreenSize.x, _fullScreenSize.x));
        }

        public virtual void OnPointerDown(PointerEventData ped)
        {
            SetJoystickBackgroundSize(_normalSize);
            _transform.position = ped.position;
            OnDrag(ped);
        }

        public virtual void OnPointerUp(PointerEventData ped)
        {
            _inputVector2 = Vector2.zero;
            _joyStickTG.rectTransform.anchoredPosition = Vector2.zero;
            SetJoystickBackgroundSize(new Vector2(_fullScreenSize.x, _fullScreenSize.x));
            _joyStickBG.rectTransform.localPosition = Vector2.zero;
            _transform.position = _canvasCenter;
        }

        public virtual void OnDrag(PointerEventData ped)
        {
            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joyStickBG.rectTransform, ped.position, ped.pressEventCamera, out pos))
            {
                pos.x = (pos.x / _joyStickBG.rectTransform.sizeDelta.x);
                pos.y = (pos.y / _joyStickBG.rectTransform.sizeDelta.y);

                _inputVector2 = new Vector2(pos.x * 2, pos.y * 2);
                _inputVector2 = (_inputVector2.magnitude > 1.0f) ? _inputVector2.normalized : _inputVector2;

                _joyStickTG.rectTransform.anchoredPosition = new Vector2(_inputVector2.x * (_joyStickBG.rectTransform.sizeDelta.x / 2), _inputVector2.y * (_joyStickBG.rectTransform.sizeDelta.y / 2));
            }
        }

        private void OnDisable()
        {
            _inputLost = true;
        }

        private void OnEnable()
        {
            _inputLost = false;
        }

        public float Horizontal()
        {
            if (_inputLost)
                return 0;

            if (_inputVector2.x != 0)
                return _inputVector2.x;
            else
                return Input.GetAxis("Horizontal");
        }

        public float Vertical()
        {
            if (_inputLost)
                return 0;

            if (_inputVector2.y != 0)
                return _inputVector2.y;
            else
                return Input.GetAxis("Vertical");
        }

        private void SetJoystickBackgroundSize(Vector2 newSize)
        {
            _joyStickBG.rectTransform.sizeDelta = newSize;
        }
    }
}
