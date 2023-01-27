using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        // TODO Update the balance UI Text element with the prefixed version of the balance

        string balanceTxt = balance.ToString();

        // Set the text on the balance element
        m_BalanceTextElement.text = balanceTxt;
    }
}
