using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameNameSpace
{
    public class LevelManager : MonoBehaviour
    {
        public Button[] buttons;
        int unlockedLevels;
        public bool toDelete;
        private void Start()
        {
            Debug.Log($"Buttons length {buttons.Length}");
            //Debug.Log($"Super start unlockedLevels is {unlockedLevels}");
            if (toDelete)
            {
                PlayerPrefs.DeleteKey("levels");
            }
            if (PlayerPrefs.GetInt("levels") == 1)
            {

            }
            unlockedLevels = PlayerPrefs.GetInt("levels", 1);

            Debug.Log($"Your levels is {unlockedLevels}");
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = i < unlockedLevels;
            }
        }

        public void UnlockNextLevel()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            int unlockedLvls = PlayerPrefs.GetInt("levels");
            if (currentScene >= unlockedLvls)
            {
                Debug.Log($"Loooooook hereee you bool seqqqq is {currentScene >= unlockedLvls}");
                PlayerPrefs.SetInt("levels", currentScene);
                Debug.Log($"{PlayerPrefs.GetInt("levels")}");
            }
        }
        public void loadScene(int levelIndex)
        {
            SceneManager.LoadScene(levelIndex);
        }

    }
}