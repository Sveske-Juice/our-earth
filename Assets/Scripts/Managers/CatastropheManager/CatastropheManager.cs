using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CatastropheManager : MonoBehaviour
{
    [Header("Catastrophe Settings")]
    [SerializeField, Tooltip("Minimum time before a catastrophe can happen")]
    private float m_MinTimeForCatastrophe = 30f;

    [SerializeField, Tooltip("Its guranteed that a catastrophe will happen before this time")]
    private float m_MaxTimeForCatastrophe = 60f;

    [SerializeField, Tooltip("How much catastrophes are accelerated (multiplied) when pollution is at its maximum.")]
    private float m_TimeMultiplierWhenMaxPollution = 4f;

    private float m_TimeSinceCatestrophe = 0f;

    /// <summary> Event that gets raised when a catastrophe starts. </summary>
    public static event Action<Catastrophe, Upgrade> OnCatastropheStart;
    private static System.Random m_Random = new System.Random();
    private static List<Catastrophe> m_Catastrophes = new List<Catastrophe> { new Tsunami(), new Tornado(), new Earthquake(), new ForestFire() };

    private void OnEnable()
    {
        CatastropheWariningDisplay.OnCatastropheAvoided += TickCatastropheTimer;
        CatastropheWariningDisplay.OnCatastropheIgnored += TickCatastropheTimer;
    }

    private void OnDisable()
    {
        CatastropheWariningDisplay.OnCatastropheAvoided -= TickCatastropheTimer;
        CatastropheWariningDisplay.OnCatastropheIgnored -= TickCatastropheTimer;
    }

    private void Start()
    {
        TickCatastropheTimer();
    }

    private void TickCatastropheTimer() { StartCoroutine(IETickCatastropheTimer()); }

    private IEnumerator IETickCatastropheTimer()
    {
        do {
            // Calculate time multiplier based on emissions
            float pollution = Mathf.Clamp((float) PollutionManager.EmissionsPrYear, (float) PollutionManager.GoodPollutionThreshold, (float) PollutionManager.BadPollutionThreshold);
            float pollutionPercent = (pollution - (float) PollutionManager.GoodPollutionThreshold)/((float) (PollutionManager.BadPollutionThreshold - PollutionManager.GoodPollutionThreshold));
            float timeMultiplier = 1 + m_TimeMultiplierWhenMaxPollution * pollutionPercent;
            // Tick catastrophe timer
            m_TimeSinceCatestrophe += Time.deltaTime * timeMultiplier;
            // print($"cat time: {m_TimeSinceCatestrophe}, mult: {timeMultiplier}");
            yield return new WaitForEndOfFrame();
        } while (m_TimeSinceCatestrophe < m_MinTimeForCatastrophe);
        
        
        // Get random catastrophe that will happen
        int randomIdx = m_Random.Next(m_Catastrophes.Count);
        Catastrophe randomCatastrophe = m_Catastrophes[randomIdx];

        // Start countdown between min and max wait time
        StartCoroutine(StartCountdownForCatastrophe(UnityEngine.Random.Range(0f, m_MaxTimeForCatastrophe - m_TimeSinceCatestrophe), randomCatastrophe));
        m_TimeSinceCatestrophe = 0f;
    }

    private IEnumerator StartCountdownForCatastrophe(float delay, Catastrophe catastrophe)
    {
        // !FIXME should probably also be scaled by timeMultiplier
        yield return new WaitForSecondsRealtime(delay);
        PerformCatastrophe(catastrophe);
    }

    /// <summary>
    /// Gets called when the catastrophe should get performed and will handle performing it.
    /// </summary>
    private void PerformCatastrophe(Catastrophe catastrophe)
    {
        Upgrade upgrade = GetRandomUpgrade();
        
        // If no upgrades could be retrieved (maybe no upgrades are higher level than 0)
        // then just try again
        if (upgrade == null)
        {
            TickCatastropheTimer();
            return;
        }

        OnCatastropheStart?.Invoke(catastrophe, upgrade);
    }

    private Upgrade GetRandomUpgrade()
    {
        // Get all upgrades in scene
        ContinentUpgradeSystem[] continents = GameObject.FindObjectsOfType<ContinentUpgradeSystem>();
        List<Upgrade> upgrades = new List<Upgrade>();
        for (int i = 0; i < continents.Length; i++)
        {
            upgrades.AddRange(continents[i].GetUpgrades());
        }
        
        // Pick upgrade which is upgraded at least once
        List<Upgrade> leveledUpgrades = new List<Upgrade>();
        for (int i = 0; i < upgrades.Count; i++)
        {
            if (upgrades[i].GetUpgradeLevel >= 1)
                leveledUpgrades.Add(upgrades[i]);
        }

        if (leveledUpgrades.Count <= 0)
            return null;

        // Pick random upgrade from leveled upgrades
        int randomUpgradeIdx = m_Random.Next(leveledUpgrades.Count);
        if (randomUpgradeIdx < 0)
            return null;
        
        return leveledUpgrades[randomUpgradeIdx];
    }
}
