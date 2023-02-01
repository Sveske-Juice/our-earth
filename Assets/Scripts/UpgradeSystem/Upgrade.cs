using UnityEngine;
using System;

public abstract class Upgrade : IBudgetInfluencer, IPollutionInfluencer
{
    protected int m_UpgradeLevel = 1;
    protected double m_CurrentUpgradeLevelPrice = 0d;
    protected abstract double m_BasePrice { get; }
    protected virtual double m_BaseEmissionInfluence => 0d;
    protected virtual double m_BaseBudgetInfluence => 0d;
    protected virtual float m_UpgradeScaling => 1.25f;

    public abstract string UpgradeName { get; }
    public double BaseEmissionInfluence => m_BaseEmissionInfluence;
    public double BaseBudgetInfluence => m_BaseBudgetInfluence;

    /// <summary> Event raised when an upgrade was performed. Will pass the <seealso cref="Upgrade"/> instance. </summary>
    public static event Action<Upgrade> OnUpgradePerformed;

    /// <summary>
    /// Calculates the upgrade's yearly influence on the budget. 
    /// </summary>
    public virtual double GetYearlyBudgetInfluence()
    {
        return m_BaseBudgetInfluence * m_UpgradeLevel;
    }

    /// <summary>
    /// Calculates the upgrade's emission influence.
    /// </summary>
    public virtual double GetEmissionInfluence()
    {
        return m_BaseEmissionInfluence * m_UpgradeLevel;
    }

    /// <summary>
    /// Will calculate the price needed to get the upgrade from level 1 to m_UpgradeLevel.
    /// Will store how much that is in m_CurrentUpgradeLevelPrice. Useful for calculating price for next level.
    /// </summary>
    protected virtual double UpdateLevelPrice()
    {
        m_CurrentUpgradeLevelPrice = m_BasePrice;
        for (int i = 1; i < m_UpgradeLevel; i++)
        {
            m_CurrentUpgradeLevelPrice *= m_UpgradeScaling;
        }
        return m_CurrentUpgradeLevelPrice;
    }

    /// <summary>
    /// Calculates the price for the next upgrade.
    /// </summary>
    public double GetNextUpgradePrice()
    {
        // If its the first upgrade then show base price
        if (m_UpgradeLevel == 1)
            return UpdateLevelPrice();
        
        return UpdateLevelPrice() * m_UpgradeScaling;
    }

    /// <summary>
    /// Will determine if this upgrade is able to be upgraded.
    /// </summary>
    /// <returns>
    /// If its able to be upgraded it will return an empty string. 
    /// If not then a description describing why its not able will be returned.
    /// </returns>
    public string IsUpgradable()
    {
        // Check if player can afford
        double balance = EconomyManager.Instance.GetBalance;
        double nextUpgradePrice = GetNextUpgradePrice();

        if (balance < nextUpgradePrice)
            return "Can not afford!";

        return "";
    }

    /// <summary>
    /// Will upgrade this upgrade to the next level if possible.
    /// </summary>
    public void Upgrade2NextLevel()
    {
        // Make sure its possible to upgrade
        string upgradeErr = IsUpgradable();
        if (upgradeErr != "")
        {
            Debug.LogWarning($"Tried to upgrade ({UpgradeName}) when the upgrade is not upgradable! Reason: {upgradeErr}");
            return;
        }

        EconomyManager.Instance.RegisterPurchase(GetNextUpgradePrice());
        m_UpgradeLevel++;
        
        OnUpgradePerformed?.Invoke(this);
    }
}
