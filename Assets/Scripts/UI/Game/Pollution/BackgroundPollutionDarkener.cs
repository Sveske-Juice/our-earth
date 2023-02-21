using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPollutionDarkener : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Tooltip("The brightness used when emissions is at a good level.")]
    private float m_Goodbrightness = 0.95f;

    [SerializeField, Tooltip("The brightness used when emissions is at a very bad level.")]
    private float m_BadBrightness = 0.1f;

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
    /// More pollution will darken the background more. (Linearly interpolating between bad and good levels)
    /// </summary>
    private void UpdateBackgroundColor(double emissions)
    {
        // Clamp pollution between good and bad levels
        float pollution = Mathf.Clamp((float) emissions, (float) PollutionManager.GoodPollutionThreshold, (float) PollutionManager.BadPollutionThreshold);

        // Get percentage of pollution in the range [PollutionManager.GoodPollutionThreshold;PollutionManager.BadPollutionThreshold]
        // see: https://math.stackexchange.com/questions/51509/how-to-calculate-percentage-of-value-inside-arbitrary-range
        float pollutionPercent = (pollution - (float) PollutionManager.GoodPollutionThreshold)/((float) (PollutionManager.BadPollutionThreshold - PollutionManager.GoodPollutionThreshold));

        // Calculate brightness value based on percentage
        float brightness = m_Goodbrightness + (m_BadBrightness - m_Goodbrightness) * pollutionPercent;

        // Set value
        float h;
        float s;
        float v;
        Color.RGBToHSV(m_Camera.backgroundColor, out h, out s, out v);

        v = brightness;
        
        m_Camera.backgroundColor = Color.HSVToRGB(h, s, v);
    }
}
