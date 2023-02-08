using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Attached to every continent managing the upgrades for that continent.
/// </summary>
public class ContinentUpgradeSystem : MonoBehaviour, IBudgetInfluencer, IPollutionInfluencer
{
    [SerializeField, Tooltip("The Continent this upgrade system is attached to. Will default to name on object attached to")]
    private string m_LinkedContinent = "";
    private List<UpgradeCategory> m_UpgradeCategories = new List<UpgradeCategory>();
    public bool highlighted = false;

    public string LinkedContinent => m_LinkedContinent;
    public List<UpgradeCategory> UpgradeCategories => m_UpgradeCategories;

    private void OnEnable()
    {
        // Register this system to the different managers
        EconomyManager.RegisterBudgetInfluncer(this);
        PollutionManager.RegisterPollutionInfluencer(this);
    }

    private void OnDisable()
    {
        // Unregister this system from the different managers
        EconomyManager.UnregisterBudgetInfluncer(this);
        PollutionManager.UnregisterPollutionInfluencer(this);
    }

    private void Start()
    {
        if (m_LinkedContinent == "")
            m_LinkedContinent = gameObject.name; // Use default

        GenerateCategories();
        SetUpgradeSystemReference();
    }

    /// <summary>
    /// Generates an instance of all the upgrade categories each continent should have.
    /// </summary>
    private void GenerateCategories()
    {
        // Create category objects which have their respective category data
        UpgradeCategoryData[] categoryContainers = Resources.LoadAll<UpgradeCategoryData>("UpgradeSystem/Categories");

        for (int i = 0; i < categoryContainers.Length; i++)
        {
            m_UpgradeCategories.Add(new UpgradeCategory(categoryContainers[i]));
        }
    }

    private void SetUpgradeSystemReference()
    {
        for (int i = 0; i < m_UpgradeCategories.Count; i++)
        {
            m_UpgradeCategories[i].ParentContinentUpgradeSystem = this;
        }
    }

    public double GetEmissionInfluence()
    {
        double continentEmission = 0d;
        for (int i = 0; i < m_UpgradeCategories.Count; i++)
        {
            continentEmission += m_UpgradeCategories[i].GetEmissionInfluence();
        }
        return continentEmission;
    }

    public double GetYearlyBudgetInfluence()
    {
        double continentBudget = 0d;
        for (int i = 0; i < m_UpgradeCategories.Count; i++)
        {
            continentBudget += m_UpgradeCategories[i].GetYearlyBudgetInfluence();
        }
        return continentBudget;
    }

    public List<Upgrade> GetUpgrades()
    {
        List<Upgrade> upgrades = new List<Upgrade>();
        for (int i = 0; i < m_UpgradeCategories.Count; i++)
        {
            upgrades.AddRange(m_UpgradeCategories[i].Upgrades);
        }
        return upgrades;
    }
}
