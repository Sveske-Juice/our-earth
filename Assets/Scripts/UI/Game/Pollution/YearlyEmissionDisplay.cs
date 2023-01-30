using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class YearlyEmissionDisplay : MonoBehaviour
{
    [SerializeField, Tooltip("Text element that will be changed to show the yearly emissions")]
    private TextMeshProUGUI m_YearlyEmissionsTextElement;

    private void OnEnable()
    {
        PollutionManager.OnYearlyEmissionChange += UpdateYearlyEmissionsText;
    }

    private void OnDisable()
    {
        PollutionManager.OnYearlyEmissionChange -= UpdateYearlyEmissionsText;
    }

    private void UpdateYearlyEmissionsText(double yearlyEmissions)
    {
        m_YearlyEmissionsTextElement.text = $"{NumberPrefixer.PrefixNumber(yearlyEmissions)} tons";
    }
}
