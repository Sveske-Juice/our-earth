using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class YearDisplay : MonoBehaviour
{
    [SerializeField, Tooltip("Text element that will be changed to show the current year")]
    private TextMeshProUGUI m_YearTextElement;

    private void OnEnable()
    {
        TimeManager.OnYearChange += UpdateYearText;
    }

    private void OnDisable()
    {
        TimeManager.OnYearChange -= UpdateYearText;
    }

    private void UpdateYearText(int year)
    {
        m_YearTextElement.text = $"Year: {year}";
    }
}
