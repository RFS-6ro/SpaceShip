using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LoseUIPage : UIPage
    {
        public void OnHomeClick()
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(0);
        }
    }
}
