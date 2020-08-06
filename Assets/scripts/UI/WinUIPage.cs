using SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class WinUIPage : UIPage
    {
        public void OnHomeClick()
        {
            Save newSave = SaveLoadSystem.Load();
            newSave.LastCompletedLevel++;
            SaveLoadSystem.Save(newSave);

            Time.timeScale = 1.0f;
            SceneManager.LoadScene(0);
        }
    }
}
