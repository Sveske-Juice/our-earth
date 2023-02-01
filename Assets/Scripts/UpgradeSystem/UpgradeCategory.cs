using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradeCategory : IBudgetInfluencer, IPollutionInfluencer
{
    public abstract string CategoryName { get; }
    protected List<Upgrade> m_Upgrades = new List<Upgrade>();
    public List<Upgrade> Upgrades => m_Upgrades;

    public UpgradeCategory()
    {
        GenerateUpgrades();
        SetCategoryReference();
    }

    /// <summary>
    /// Generates all the upgrades linked to this upgrade category.
    /// </summary>
    /// <remarks>
    /// For example if the category is "Transport". Electric cars etc. upgrades will be generated.
    /// </remarks>
    protected abstract void GenerateUpgrades();

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
}
