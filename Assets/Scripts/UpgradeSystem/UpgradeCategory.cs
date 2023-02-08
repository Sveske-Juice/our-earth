using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCategory : IBudgetInfluencer, IPollutionInfluencer
{
    protected UpgradeCategoryData m_CategoryData;
    protected ContinentUpgradeSystem m_ParentContinentUpgradeSystem;
    protected List<Upgrade> m_Upgrades = new List<Upgrade>();

    public string CategoryName => m_CategoryData.CategoryName;
    public Texture2D CategoryIcon => m_CategoryData.CategoryIcon;
    public ContinentUpgradeSystem ParentContinentUpgradeSystem { set { m_ParentContinentUpgradeSystem = value; } get { return m_ParentContinentUpgradeSystem; }}
    public List<Upgrade> Upgrades => m_Upgrades;


    public UpgradeCategory(UpgradeCategoryData categoryData)
    {
        m_CategoryData = categoryData;

        GenerateUpgrades();
        SetCategoryReference();
    }

    /// <summary>
    /// Generates all the upgrades linked to this upgrade category.
    /// </summary>
    /// <remarks>
    /// For example if the category is "Transport". Electric cars etc. upgrades will be generated.
    /// </remarks>
    protected void GenerateUpgrades()
    {
        // Dynamically load all upgrade containers linked to this category
        UpgradeData[] upgradeContainers = Resources.LoadAll<UpgradeData>($"UpgradeSystem/Upgrades/{m_CategoryData.CategoryName}");

        // Generate upgrade objects from containers
        for (int i = 0; i < upgradeContainers.Length; i++)
        {
            m_Upgrades.Add(new Upgrade(upgradeContainers[i]));
        }
    }

    private void SetCategoryReference()
    {
        for (int i = 0; i < m_Upgrades.Count; i++)
        {
            m_Upgrades[i].ParentCategory = this;
        }
    }

    public Upgrade GetUpgradeByName(string upgradeName)
    {
        for (int i = 0; i < m_Upgrades.Count; i++)
        {
            if (m_Upgrades[i].UpgradeName == upgradeName)
                return m_Upgrades[i];
        }

        return null;
    }

    public double GetYearlyBudgetInfluence()
    {
        double categoryBudget = 0d;
        for (int i = 0; i < m_Upgrades.Count; i++)
        {
            categoryBudget += m_Upgrades[i].GetYearlyBudgetInfluence();
        }
        return categoryBudget;
    }
    public double GetEmissionInfluence()
    {
        double categoryEmission = 0d;
        for (int i = 0; i < m_Upgrades.Count; i++)
        {
            categoryEmission += m_Upgrades[i].GetEmissionInfluence();
        }
        return categoryEmission;
    }

    public void RegisterSpecialEffect(SpecialUpgradeEffect specialUpgradeEffect)
    {
        // Find upgrade influenced by special effect
        for (int i = 0; i < m_Upgrades.Count; i++)
        {
            Upgrade upgrade = m_Upgrades[i];
            if (upgrade.UpgradeName == specialUpgradeEffect.UpgradeInfluenced)
            {
                Debug.Log($"Applying special effect: {specialUpgradeEffect.ToString()} on upgrade: {upgrade.UpgradeName} in category {upgrade.ParentCategory} in continent {upgrade.ParentCategory.ParentContinentUpgradeSystem.LinkedContinent}");
                // Apply special effect on upgrade
                upgrade.RegisterUpgradeModifier(specialUpgradeEffect.SpecialEffect);
            }
        }
    }

    public void UnregisterSpecialEffect(SpecialUpgradeEffect specialUpgradeEffect)
    {
        // Find upgrade influenced by special effect
        for (int i = 0; i < m_Upgrades.Count; i++)
        {
            Upgrade upgrade = m_Upgrades[i];
            if (upgrade.UpgradeName == specialUpgradeEffect.UpgradeInfluenced)
            {
                Debug.Log($"Removing special effect: {specialUpgradeEffect.ToString()} on upgrade: {upgrade.UpgradeName}");
                // Remove special effect on upgrade
                upgrade.RemoveUpgradeModifier(specialUpgradeEffect.SpecialEffect);
            }
        }
    }

    protected UpgradeCategoryData LoadCategoryData(string path)
    {
        UpgradeCategoryData categoryData = Resources.Load<UpgradeCategoryData>(path);
        if (categoryData == null)
            Debug.LogError($"Could not load category data on {this}, path: {path}");
        return categoryData;
    }
}
