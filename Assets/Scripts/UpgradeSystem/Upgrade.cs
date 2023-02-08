using UnityEngine;
using System.Collections.Generic;
using System;

public class Upgrade : IBudgetInfluencer, IPollutionInfluencer
{
    public Upgrade(UpgradeData upgradeData)
    {
        m_UpgradeData = upgradeData;
    }

    protected UpgradeData m_UpgradeData;
    protected UpgradeCategory m_ParentCategory;
    protected int m_UpgradeLevel = 0;
    protected double m_CurrentUpgradeLevelPrice = 0d;
    
    // Wrappers for public fields in upgrade data
    public string UpgradeName => m_UpgradeData.UpgradeName;
    public string UpgradeExplanation => m_UpgradeData.UpgradeExplanation;

    
    protected List<UpgradeModifier> m_UpgradeModifiers = new List<UpgradeModifier>();
    public void RegisterUpgradeModifier(UpgradeModifier modifier) => m_UpgradeModifiers.Add(modifier);
    public void RemoveUpgradeModifier(UpgradeModifier modifier) => m_UpgradeModifiers.Remove(modifier);
    public int GetUpgradeLevel => m_UpgradeLevel;
    public UpgradeCategory ParentCategory { set { m_ParentCategory = value; } get { return m_ParentCategory; }}
    public double BaseEmissionInfluence { get {
        double baseEmissionInfluence = m_UpgradeData.BaseEmissionInfluence;

        // Sum up all modifiers
        for (int i = 0; i < m_UpgradeModifiers.Count; i++)
        {
            baseEmissionInfluence += m_UpgradeModifiers[i].GetEmissionInfluence();
        }
        return baseEmissionInfluence;
    }}
    public double BaseBudgetInfluence { get {
        double baseBudgetInfluence = m_UpgradeData.BaseBudgetInfluence;

        // Sum up all modifiers
        for (int i = 0; i < m_UpgradeModifiers.Count; i++)
        {
            baseBudgetInfluence += m_UpgradeModifiers[i].GetYearlyBudgetInfluence();
        }
        return baseBudgetInfluence;
    }}

    /// <summary> Event raised when an upgrade was performed. Will pass the <seealso cref="Upgrade"/> instance. </summary>
    public static event Action<Upgrade> OnUpgradePerformed;

    /// <summary> Event raised when an upgrade was downgraded. Will pass the <seealso cref="Upgrade"/> instance. </summary>
    public static event Action<Upgrade> OnDowngradePerformed;

    /// <summary>
    /// Calculates the upgrade's yearly influence on the budget. 
    /// </summary>
    public virtual double GetYearlyBudgetInfluence()
    {
        return BaseBudgetInfluence * m_UpgradeLevel;
    }

    /// <summary>
    /// Calculates the upgrade's emission influence.
    /// </summary>
    public virtual double GetEmissionInfluence()
    {
        return BaseEmissionInfluence * m_UpgradeLevel;
    }

    /// <summary> Called just before the upgrade is performed. </summary>
    protected virtual void OnBeforeUpgrade()
    {
        if (!m_UpgradeData.SpecialEffectUpgrade)
            return;
        
        // Just before upgrade is performed and UI is updated, check if the 
        // special effect should be added or removed
        if (m_UpgradeLevel >= 1)
        {
            ApplySpecialEffects();
        }
        else
        {
            RemoveSpecialEffects();
        }
    }

    /// <summary>
    /// Will calculate the price needed to get the upgrade from level 1 to m_UpgradeLevel.
    /// Will store how much that is in m_CurrentUpgradeLevelPrice. Useful for calculating price for next level.
    /// </summary>
    protected virtual double UpdateLevelPrice()
    {
        m_CurrentUpgradeLevelPrice = m_UpgradeData.BasePrice;
        for (int i = 0; i < m_UpgradeLevel; i++)
        {
            m_CurrentUpgradeLevelPrice *= m_UpgradeData.UpgradeScaling;
        }
        return m_CurrentUpgradeLevelPrice;
    }

    /// <summary>
    /// Calculates the price for the next upgrade.
    /// </summary>
    public double GetNextUpgradePrice()
    {
        // If its the first upgrade then show base price
        if (m_UpgradeLevel == 0)
            return UpdateLevelPrice();
        
        return UpdateLevelPrice() * m_UpgradeData.UpgradeScaling;
    }

    /// <summary>
    /// Will determine if this upgrade is able to be upgraded.
    /// </summary>
    /// <returns>
    /// If its able to be upgraded it will return an empty string. 
    /// If not then a description describing why its not able will be returned.
    /// </returns>
    public virtual string IsUpgradable()
    {
        // Check that it wont exceed the max level
        if (m_UpgradeLevel >= m_UpgradeData.MaxUpgradeLevel)
            return "Max Level";

        // Check if unlock requirements are reached
        string requirementsNotMet = CheckForUnlockRequirements();
        
        if (requirementsNotMet != "")
            return requirementsNotMet;

        // Check if player can afford
        double balance = EconomyManager.Instance.GetBalance;
        double nextUpgradePrice = GetNextUpgradePrice();

        if (balance < nextUpgradePrice)
            return "Can not afford!";

        return "";
    }

    /// <summary>
    /// Will check if the unlock requirements are met.
    /// </summary>
    /// <returns>
    /// An empty string indicating all requirements are met.
    /// Or a string describing what requirements aren't met.
    /// </returns>
    private string CheckForUnlockRequirements()
    {
        string requirementsNotMet = "";

        // Traverse all unlock requirements
        for (int i = 0; i < (m_UpgradeData.RequiredUpgrades?.Length ?? 0); i++)
        {
            string upgradeName = m_UpgradeData.RequiredUpgrades[i].RequiredUpgradeName;
            int requiredLevel = m_UpgradeData.RequiredUpgrades[i].RequiredUpgradeLevel;

            // Try get the upgrade object from required upgrade name
            Upgrade upgrade = m_ParentCategory.GetUpgradeByName(upgradeName);
            if (upgrade == null)
            {
                Debug.LogWarning($"Upgrade {m_UpgradeData.UpgradeName} requires {upgradeName} but it doesn't exist!");
                return $"Unkown upgrade requirement: {upgradeName}";
            }

            // Make sure the level meets requirement
            if (upgrade.GetUpgradeLevel < requiredLevel)
                requirementsNotMet += $"{upgradeName} is required to be in level {requiredLevel}\n";
        }

        return requirementsNotMet;
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
            Debug.LogWarning($"Tried to upgrade ({m_UpgradeData.UpgradeName}) when the upgrade is not upgradable! Reason: {upgradeErr}");
            return;
        }

        EconomyManager.Instance.RegisterPurchase(GetNextUpgradePrice());
        m_UpgradeLevel++;

        OnBeforeUpgrade();

        OnUpgradePerformed?.Invoke(this);
    }

    public void Downgrade()
    {
        if (m_UpgradeLevel <= 0)
            return;
        
        m_UpgradeLevel--;

        OnBeforeUpgrade();

        OnDowngradePerformed?.Invoke(this);
    }
    
    protected void ApplySpecialEffects()
    {
        for (int i = 0; i < (m_UpgradeData.UpgradeSpecialEffects?.Length ?? 0); i++)
        {
            ParentCategory.RegisterSpecialEffect(m_UpgradeData.UpgradeSpecialEffects[i]);
        }
    }

    protected void RemoveSpecialEffects()
    {
        for (int i = 0; i < (m_UpgradeData.UpgradeSpecialEffects?.Length ?? 0); i++)
        {
            ParentCategory.UnregisterSpecialEffect(m_UpgradeData.UpgradeSpecialEffects[i]);
        }
    }
}
