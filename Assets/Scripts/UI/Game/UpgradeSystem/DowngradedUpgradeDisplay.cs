using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DowngradedUpgradeDisplay : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Tooltip("How much time spent scaling the object to 100% when being displayed.")]
    private float m_ShowPopupScaleTime = 1.5f;

    [SerializeField, Tooltip("How much time spent scaling the object to 0% when being shrinked.")]
    private float m_ShowPopupShrinkTime = 1.5f;

    [SerializeField, Tooltip("How long the popup should be in 100% size shown before starting to shrink again.")]
    private float m_ShowPopupTime = 5f;

    
    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI m_UpgradedDowngradeText;

    [SerializeField]
    private Transform m_PopupMiddlePosition;

    

    private void OnEnable()
    {
        Upgrade.OnDowngradePerformed += OnDowngrade;
    }

    private void OnDisable()
    {
        Upgrade.OnDowngradePerformed -= OnDowngrade;
    }

    private void OnDowngrade(Upgrade upgrade)
    {
        m_UpgradedDowngradeText.text = $"{upgrade.UpgradeName} was downgraded to level {upgrade.GetUpgradeLevel}!";
        ShowDisplay();
    }

    private void ShowDisplay()
    {
        transform.position = m_PopupMiddlePosition.position;
        
        LeanTween.scale(gameObject, Vector3.one, m_ShowPopupScaleTime).setOnComplete( () =>
        {
            // When done scaling the popup up, then start shrinking after m_ShowPopupTime seconds
            Invoke("ShrinkDisplay", m_ShowPopupTime);
        });
    }

    private void ShrinkDisplay()
    {
        LeanTween.scale(gameObject, Vector3.zero, m_ShowPopupShrinkTime);
    }
}
