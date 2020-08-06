using SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuUIViewController : MonoBehaviour
    {
        [SerializeField] private Button[] _levels;

        private int _currentLevel;

        private void Awake()
        {
            _currentLevel = SaveLoadSystem.Load().LastCompletedLevel;

            for (int levelIndex = 0; levelIndex < _currentLevel; levelIndex++)
            {
                _levels[levelIndex].image.color = Color.green;
                _levels[levelIndex].interactable = false;
            }

            for (int levelIndex = _currentLevel + 1; levelIndex < _levels.Length; levelIndex++)
            {
                _levels[levelIndex].interactable = false;
            }
        }

        public void OnConcreteLevelClick(Button button)
        {
            SceneManager.LoadScene(button.name);
        }

        public void OnResetClick()
        {
            SaveLoadSystem.Save(new Save());
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
