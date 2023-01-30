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
    /// </summary>
    private void UpdateBalanceText(double balance)
    {
        // Update balance ui element with prefixed version of number
        m_BalanceTextElement.text = $"${NumberPrefixer.PrefixNumber(balance)}";
    }
}