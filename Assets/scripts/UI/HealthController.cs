using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private Destroyable _destroyableModel;

        [SerializeField] private BaseHealthView _view;

        private void Awake()
        {
            if (_destroyableModel == null)
            {
                throw new System.Exception("Destroyable model null reference");
            }

            if (_view == null)
            {
                throw new System.Exception("Destroyable view null reference");
            }

            _destroyableModel.OnHealthChanged += ShowHealthChange;
        }

        private void ShowHealthChange(int value)
        {
            _view.ShowHealth(value, _destroyableModel.MaxHealth);
        }

        private void OnDestroy()
        {
            if (_destroyableModel != null)
            {
                _destroyableModel.OnHealthChanged -= ShowHealthChange;
            }
        }
    }
}
