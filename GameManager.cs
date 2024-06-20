using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

namespace GameNameSpace
{
    public class GameManager : MonoBehaviour
    {
        public int idealSteps = 10;
        public LevelManager levelManager;
        int currLvl;
        Player player;
        bool readyForInput;
        public GameObject chooseLevel;
        public GameObject backButton;
        public GameObject resetButton;
        public TextMeshProUGUI movesNum;
        public TextMeshProUGUI currLvlText;
        public TextMeshProUGUI idealStepsText;

        //public Text currLevel;


        private void Start()
        {

            FindPlayer();
        }
        // Update is called once per frame
        void Update()
        {
            Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            movement.Normalize();
            if (player)
            {
                if (isLevelCompleted())
                {
                    player.enabled = false;
                    LevelCompleted();
                }
                if (movement.sqrMagnitude > 0.3)
                {
                    if (readyForInput)
                    {
                        readyForInput = false;
                        player.Move(movement);
                    }
                }
                else
                {
                    readyForInput = true;
                }
            }

            if (player.moveNums > 0)
            {
                backButton.SetActive(true);
            }
            else
            {
                backButton.SetActive(false);
            }
            movesNum.text = "Количество шагов: " + player.moveNums.ToString();
            currLvlText.text = (SceneManager.GetActiveScene().buildIndex - 1) + " уровень";
            currLvl = SceneManager.GetActiveScene().buildIndex;
            idealStepsText.text = "Идеальное количество шагов: " + idealSteps.ToString();
        }
        bool isLevelCompleted()
        {
            Box[] boxes = FindObjectsOfType<Box>();
            foreach (var box in boxes)
            {
                if (!box.arrived)
                {
                    return false;
                }
            }
            return true;
        }
        public void Back()
        {
            if (player.moves.Count > 0)
            {
                if (player.moves.Peek().withBox)
                {
                    player.transform.position = new Vector3(player.moves.Peek().fromPos.x, player.moves.Peek().fromPos.y, player.moves.Peek().fromPos.z);
                    Debug.Log($"Moving back to {player.transform.position} with box");
                    player.moves.Peek().movedBox.transform.position = new Vector3(player.moves.Peek().boxPos.x, player.moves.Peek().boxPos.y, player.moves.Peek().boxPos.z);
                }
                else
                {
                    player.transform.position = new Vector3(player.moves.Peek().fromPos.x, player.moves.Peek().fromPos.y, player.moves.Peek().fromPos.z);
                    Debug.Log($"Moving back to {player.transform.position} without box");

                }
                player.moves.Pop();
                --player.moveNums;
            }
            else
            {
                backButton.SetActive(false);
                Debug.Log("No moves!!!!!");
            }
        }
        public void LevelCompleted()
        {
            float percentage = (float)idealSteps / player.moveNums * 100;
            FindObjectOfType<EndLevelUI>().ShowEndLevelPanel(player.moveNums, percentage);
            UpdateBestScore(player.moveNums, percentage);
            levelManager.UnlockNextLevel();
        }
        public void NextButton()
        {
            levelManager.loadScene(currLvl + 1);
        }
        public void ResetButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void ChooseLevelButton()
        {
            SceneManager.LoadScene(1);
        }


        public void UpdateBestScore(int steps, float percentage)
        {
            string key = "level" + (SceneManager.GetActiveScene().buildIndex - 1);
            float bestPercentage = PlayerPrefs.GetFloat(key, -1);
            if (percentage > bestPercentage)
            {
                PlayerPrefs.SetFloat(key, percentage);
                PlayerPrefs.SetInt(key + "_steps", player.moveNums);
            }

        }
        void FindPlayer()
        {
            player = FindObjectOfType<Player>();
        }
    }
}