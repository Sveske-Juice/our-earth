using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    private static EconomyManager s_Instance;
    private EconomyData m_EconomyData;

    [SerializeField, Tooltip("Amount of yearly income the game starts at")]
    private double m_BaseYearlyIncome = 100_000_000_000_000d; // 100 Trillion
    private double m_YearlyIncome;

    private static List<IBudgetInfluencer> m_BudgetInfluencers = new List<IBudgetInfluencer>();
    public static void RegisterBudgetInfluncer(IBudgetInfluencer influencer) { m_BudgetInfluencers.Add(influencer); }
    public static void UnregisterBudgetInfluncer(IBudgetInfluencer influencer) { m_BudgetInfluencers.Remove(influencer); }
    public static EconomyManager Instance => s_Instance;
    public double GetBalance => m_EconomyData.balance;

    public static event Action<double> OnBalanceChange;
    public static event Action<double> OnYearlyIncomeChange;

    private void Awake()
    {
        // Check if another instance already exists
        if (s_Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Else this is the first instance
        s_Instance = this;
    }

    private void OnEnable()
    {
        TimeManager.OnYearChange += OnNewYear;
        Upgrade.OnUpgradePerformed += OnUpgradePerformed; // Update total yearly budget when a new upgrade is performed
        Upgrade.OnDowngradePerformed += OnUpgradePerformed; // Update total yearly budget when a new upgrade is performed
    }

    private void OnDisable()
    {
        TimeManager.OnYearChange -= OnNewYear;
        Upgrade.OnUpgradePerformed -= OnUpgradePerformed;
        Upgrade.OnDowngradePerformed -= OnUpgradePerformed; // Update total yearly budget when a new upgrade is performed
    }

    private void OnUpgradePerformed(Upgrade upgrade) => UpdateTotalYearlyIncome();

    /// <summary>
    /// Updates the yearly income based on the income influencers.
    /// </summary>
    private void UpdateTotalYearlyIncome()
    {
        double oldYearlyIncome = m_YearlyIncome;
        m_YearlyIncome = m_BaseYearlyIncome;

        for (int i = 0; i < m_BudgetInfluencers.Count; i++)
        {
            m_YearlyIncome += m_BudgetInfluencers[i].GetYearlyBudgetInfluence();
        }

        // If yearly income have changed then notify subscribers
        if (oldYearlyIncome != m_YearlyIncome)
            OnYearlyIncomeChange?.Invoke(m_YearlyIncome);

        // Debug.Log($"Updated total yearly income. Budget is now {m_YearlyIncome}");
    }

    private void Start()
    {
        LoadData();

        // Update the yearly income when initializing
        UpdateTotalYearlyIncome();
    }

    private void OnNewYear(int year)
    {
        // Add the yearly income to the player's balance
        m_EconomyData.balance += m_YearlyIncome;

        // Raise on balance change event with the new balance
        OnBalanceChange?.Invoke(m_EconomyData.balance);
    }

    private void LoadData()
    {
        // TODO Load progress data from disk

        // If there's no progress data then just load defaults
        m_EconomyData = new EconomyData();
    }

    public void RegisterPurchase(double amount)
    {
        m_EconomyData.balance -= amount;
        OnBalanceChange?.Invoke(m_EconomyData.balance);
    }
}

/**
<summary>
Data container which stores data about the player's economy.
</summary>
<remarks>
The class is serializable which means that it can be
serialized (saved to disk), and unserialized (loaded from disk)
</remarks>
**/
[Serializable]
public class EconomyData
{
    public double balance = 6969d;
}