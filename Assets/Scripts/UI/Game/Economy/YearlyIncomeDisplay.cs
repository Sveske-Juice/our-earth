using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class YearlyIncomeDisplay : MonoBehaviour
{
    [SerializeField, Tooltip("Text element that will be changed to show the yearly income")]
    private TextMeshProUGUI m_YearlyIncomeTextElement;

    private void OnEnable()
    {
        EconomyManager.OnYearlyIncomeChange += OnYearlyIncomeChange;
    }

    private void OnDisable()
    {
        EconomyManager.OnYearlyIncomeChange -= OnYearlyIncomeChange;
    }

    private void OnYearlyIncomeChange(double yearlyIncome)
    {
        m_YearlyIncomeTextElement.text = $"${NumberPrefixer.Prefix(yearlyIncome)}";
    }
}
