using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

namespace GameNameSpace
{
    public class ResultTable : MonoBehaviour
    {
        public bool toDelete;
        public TextMeshProUGUI[] levels;
        public TextMeshProUGUI[] numSteps;
        public TextMeshProUGUI[] percentages;

        private void Start()
        {
            if (toDelete)
            {
                for (int i = 0; i < SceneManager.sceneCountInBuildSettings - 3; i++)
                {
                    PlayerPrefs.DeleteKey("level" + (i + 1));
                }
            }
            Debug.Log(SceneManager.sceneCountInBuildSettings - 3 + "  gfgfgfg");
            UpdateResults();
        }

        public void UpdateResults()
        {

            int levelsCount = SceneManager.sceneCountInBuildSettings - 3; // Получаем количество уровней
            for (int i = 0; i < levelsCount; i++)
            {
                levels[i].text = "";
                numSteps[i].text = "";
                percentages[i].text = "";

                string key = "level" + (i + 1);
                float percentage = PlayerPrefs.GetFloat(key, -1); // Получаем сохраненный процент для уровня
                int steps = PlayerPrefs.GetInt(key + "_steps", -1); // Получаем количество шагов для лучшего результата



                if (percentage != -1 && steps != -1)
                {
                    levels[i].text = (i + 1).ToString();
                    numSteps[i].text = steps.ToString();
                    percentages[i].text = percentage.ToString("F0");
                }
            }
        }
    }
}