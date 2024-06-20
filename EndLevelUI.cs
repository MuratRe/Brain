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

        public GameObject endLevelPanel;  // ������ ������������ ����
        public GameObject antiClicker;
        public TextMeshProUGUI stepsText;            // ��������� ���� ��� ����������� ���������� �����
        public TextMeshProUGUI percentageText;       // ��������� ���� ��� ����������� �������� ������
        public Button continueButton;     // ������ ��� �����������
        public Button menuButton;     // ������ ��� �����������


        void Start()
        {
            antiClicker.SetActive(false);
            endLevelPanel.SetActive(false); // ������ ������ ��� �������
            continueButton.onClick.AddListener(OnContinueButtonClicked); // �������� ���������� ��� ������
        }

        public void ShowEndLevelPanel(int steps, float percentage)
        {
            FindObjectOfType<Player>().DisableMove();
            stepsText.text = "���-�� �����: " + steps.ToString(); // ���������� ����� ��� ���������� �����
            percentageText.text = "������� �� ������: " + percentage.ToString("F0") + "%"; // ���������� ����� ��� �������� ������

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