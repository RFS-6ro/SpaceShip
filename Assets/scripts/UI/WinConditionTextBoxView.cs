using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class WinConditionTextBoxView : TextBoxView
    {
        [SerializeField] private SessionManager _sessionManager;

        private void Start()
        {
            if (_sessionManager == null)
            {
                _sessionManager = FindObjectOfType<SessionManager>();
            }

            _sessionManager.OnWinConditionValueChanged += OnConditionChange;
        }

        private void OnConditionChange(int arg1)
        {
            SetText(arg1.ToString() + "/" + _sessionManager.DestroyedObstaclesToWin.ToString());
        }

        private void OnDisable()
        {
            if (_sessionManager != null)
            {
                _sessionManager.OnWinConditionValueChanged -= OnConditionChange;
            }
        }
    }
}
