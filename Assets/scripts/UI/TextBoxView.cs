using UnityEngine;
using TMPro;

namespace UI
{
    public class TextBoxView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textbox;

        protected void SetText(string msg)
        {
            if (_textbox == null)
            {
                _textbox = GetComponent<TMP_Text>();
            }

            _textbox.text = msg;
        }
    }
}
