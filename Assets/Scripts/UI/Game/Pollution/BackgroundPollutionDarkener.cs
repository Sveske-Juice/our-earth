using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPollutionDarkener : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Tooltip("The pollution level that corresponds to maximum. The color will be the darkest if the pollution level reaches this value.")]
    private float m_MaxPollutionThreshold = (float) NumberPrefixer.Parse("30B");

    [SerializeField, Tooltip("The pollution level that corresponds to minimum. The color will be the lightest if the pollution level reaches this value.")]
    private float m_MinPollutionThreshold = (float) NumberPrefixer.Parse("5B");

    [SerializeField, Tooltip("The Value (from HSV) used when minimum pollution is reached.")]
    private float m_MaxHSVValue = 1f;

    [SerializeField, Tooltip("The Value (from HSV) used when maximum pollution is reached.")]
    private float m_MinHSVValue = 0.1f;

    private Camera m_Camera;

    private void OnEnable()
    {
        PollutionManager.OnYearlyEmissionChange += UpdateBackgroundColor;
    }

    private void OnDisable()
    {
        PollutionManager.OnYearlyEmissionChange -= UpdateBackgroundColor;
    }

    private void Awake()
    {
        m_Camera = Camera.main;
    }

    /// <summary>
    /// Will update the games background color based on the yearly emissions.
    /// More pollution will darken the background more.
    /// </summary>
    private void UpdateBackgroundColor(double emissions)
    {
        float pollution = Mathf.Clamp((float) emissions, 0f, (float) m_MaxPollutionThreshold);

        // Percentage of how much the dark color should be viewed. If pollution is on max the percentage is 100%
        float pollutionPercentage = pollution / m_MaxPollutionThreshold;
        print($"Pollution Percentage: {pollutionPercentage}");
        // Find HSB brightness for pollution percentage
        float brightness = 1 - (m_MaxHSVValue - m_MinHSVValue) * pollutionPercentage;
        brightness = Mathf.Clamp(brightness, m_MinHSVValue, m_MaxHSVValue);

        // Set value
        float h;
        float s;
        float v;
        Color.RGBToHSV(m_Camera.backgroundColor, out h, out s, out v);

        v = brightness;
        
        m_Camera.backgroundColor = Color.HSVToRGB(h, s, v);
    }
}
