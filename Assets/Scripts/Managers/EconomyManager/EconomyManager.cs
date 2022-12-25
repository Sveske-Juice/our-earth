using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    private EconomyManager m_Instance;
    private EconomyData m_EconomyData;

    public static event Action<double> OnBalanceChange;

    private void Awake()
    {
        // Check if another instance already exists
        if (m_Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Else this is the first instance
        m_Instance = this;
    }

    private void OnEnable()
    {
        TimeManager.OnYearChange += OnNewYear;
    }

    private void OnDisable()
    {
        TimeManager.OnYearChange -= OnNewYear;
    }

    private void Start()
    {
        // TODO Load progress data from disk

        // If there's no progress data then just load defaults
        m_EconomyData = new EconomyData();
    }

    private void OnNewYear(int year)
    {
        // Add the yearly income to the player's balance
        m_EconomyData.balance += m_EconomyData.yearlyIncome;

        // Raise balance change event with the new balance
        OnBalanceChange?.Invoke(m_EconomyData.balance);

        // Debug.Log($"Balance: {m_EconomyData.balance}");
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
    public double balance = 0;
    public double yearlyIncome = 1500;
}