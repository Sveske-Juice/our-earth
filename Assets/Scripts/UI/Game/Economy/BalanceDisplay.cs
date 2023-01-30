using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BalanceDisplay : MonoBehaviour
{
    [SerializeField, Tooltip("Text element that will be changed to show the balance")]
    private TextMeshProUGUI m_BalanceTextElement;

    private void OnEnable()
    {
        EconomyManager.OnBalanceChange += UpdateBalanceText;
    }

    private void OnDisable()
    {
        EconomyManager.OnBalanceChange -= UpdateBalanceText;
    }

    /// <summary>
    /// Updates the balance ui text element to show the player's current balance.
    /// Will also show it in prefix form, like $300M for $300 million etc.
    /// </summary>
    private void UpdateBalanceText(double balance)
    {
        string prefix = "";
        double newBalance = 0d;

        if (balance < 100000) // 100K
        {
            newBalance = balance / 1000;
            prefix = "K";
        }
        else if (balance < 1000000000) // 100M
        {
            newBalance = balance / 1000000;
            prefix = "M";
        }
        else if (balance < 1000000000000) // 100B
        {
            newBalance = balance / 1000000000;
            prefix = "B";
        }
        else if (balance < 1000000000000000) // 100T
        {
            newBalance = balance / 1000000000000;
            prefix = "T";
        }

        // Set the text on the balance element
        newBalance = Math.Round(newBalance, 2);
        m_BalanceTextElement.text = $"${newBalance}{prefix}";
    }
}