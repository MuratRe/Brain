using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

namespace GameNameSpace
{
    public class EndLevelUI : MonoBehaviour
    {

        public GameObject endLevelPanel;  // Панель всплывающего окна
        public GameObject antiClicker;
        public TextMeshProUGUI stepsText;            // Текстовое поле для отображения количества шагов
        public TextMeshProUGUI percentageText;       // Текстовое поле для отображения процента успеха
        public Button continueButton;     // Кнопка для продолжения
        public Button menuButton;     // Кнопка для продолжения


        void Start()
        {
            antiClicker.SetActive(false);
            endLevelPanel.SetActive(false); // Скрыть панель при запуске
            continueButton.onClick.AddListener(OnContinueButtonClicked); // Добавить обработчик для кнопки
        }

        public void ShowEndLevelPanel(int steps, float percentage)
        {
            FindObjectOfType<Player>().DisableMove();
            stepsText.text = "Кол-во шагов: " + steps.ToString(); // Установить текст для количества шагов
            percentageText.text = "Процент от идеала: " + percentage.ToString("F0") + "%"; // Установить текст для процента успеха

            antiClicker.SetActive(true);
            endLevelPanel.SetActive(true);

        }

        private void OnContinueButtonClicked()
        {
            endLevelPanel.SetActive(false);
            antiClicker.SetActive(false);

        }
    }
}