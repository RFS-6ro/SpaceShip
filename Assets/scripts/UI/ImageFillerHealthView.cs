using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ImageFillerHealthView : BaseHealthView
    {
        [SerializeField] private Image _fillerImage;

        private void Awake()
        {
            if (_fillerImage == null)
            {
                _fillerImage = GetComponent<Image>();
            }
        }

        public override void ShowHealth(int newHealth, int maxHealth = 0)
        {
            if (_fillerImage == null)
            {
                _fillerImage = GetComponent<Image>();
            }

            float fillPerCent = (float)newHealth / (float)maxHealth;

            _fillerImage.fillAmount = fillPerCent;
            if (fillPerCent > 0.7)
            {
                _fillerImage.color = Color.green;
            }
            else if (fillPerCent > 0.4)
            {
                _fillerImage.color = Color.yellow;
            }
            else
            {
                _fillerImage.color = Color.red;
            }
        }
    }
}
