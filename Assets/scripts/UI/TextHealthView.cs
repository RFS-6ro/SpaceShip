using UnityEngine;
using TMPro;

namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class TextHealthView : BaseHealthView
    {
        [SerializeField] private TMP_Text _textbox;

        private void Awake()
        {
            if (_textbox == null)
            {
                _textbox = GetComponent<TMP_Text>();
            }
        }

        public override void ShowHealth(int newHealth, int maxHealth = 0)
        {
            if (_textbox == null)
            {
                _textbox = GetComponent<TMP_Text>();
            }

            _textbox.text = newHealth.ToString();
        }
    }
}
