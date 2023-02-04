using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CatastropheManager : MonoBehaviour
{
    [Header("Catastrophe Settings")]
    [SerializeField, Tooltip("Minimum time before a catastrophe can happen")] private float m_MinTimeForCatastrophe = 30f;
    [SerializeField, Tooltip("Its guranteed that a catastrophe will happen before this time")] private float m_MaxTimeForCatastrophe = 60f;

    /// <summary> Event that gets raised when a catastrophe starts. </summary>
    public static event Action<Catastrophe, Upgrade> OnCatastropheStart;
    private static System.Random m_Random = new System.Random();
    private static List<Catastrophe> m_Catastrophes = new List<Catastrophe> { new Tsunami(), new Tornado(), new Earthquake(), new ForestFire() };

    private void OnEnable()
    {
        DisasterWarningUI.OnCatastropheAvoided += ChooseNextCatastrophe;
        DisasterWarningUI.OnCatastropheIgnored += ChooseNextCatastrophe;
    }

    private void OnDisable()
    {
        DisasterWarningUI.OnCatastropheAvoided -= ChooseNextCatastrophe;
        DisasterWarningUI.OnCatastropheIgnored -= ChooseNextCatastrophe;
    }

    private void Start()
    {
        ChooseNextCatastrophe();
    }

    private void ChooseNextCatastrophe()
    {
        // Get random time for the catastrophe to happen
        float timeForCatastrophe = UnityEngine.Random.Range(m_MinTimeForCatastrophe, m_MaxTimeForCatastrophe);

        // Get random catastrophe that will happen
        int randomIdx = m_Random.Next(m_Catastrophes.Count);
        Catastrophe randomCatastrophe = m_Catastrophes[randomIdx];

        // Start countdown of the catastrophe
        StartCoroutine(StartCountdownForCatastrophe(timeForCatastrophe, randomCatastrophe));
    }

    private IEnumerator StartCountdownForCatastrophe(float delay, Catastrophe catastrophe)
    {
        yield return new WaitForSecondsRealtime(delay);
        PerformCatastrophe(catastrophe);
    }

    /// <summary>
    /// Gets called when the catastrophe should get performed and will handle performing it.
    /// </summary>
    private void PerformCatastrophe(Catastrophe catastrophe)
    {
        Upgrade upgrade = GetRandomUpgrade();
        if (upgrade == null)
        {
            ChooseNextCatastrophe();
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
        Debug.Log(randomUpgradeIdx);
        if (randomUpgradeIdx < 0)
            return null;
        
        return leveledUpgrades[randomUpgradeIdx];
    }
}
