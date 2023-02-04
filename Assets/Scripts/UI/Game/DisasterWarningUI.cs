using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class DisasterWarningUI : MonoBehaviour
{
    [SerializeField, Tooltip("Text component that will hold the text display")]
    private TextMeshProUGUI m_CatastropheText;

    [SerializeField, Tooltip("Button that will accept catastrophe payment")]
    private Button m_AcceptPaymentButton;

    [SerializeField, Tooltip("Amount to be paid to avoid catastrophe")]
    private double m_AvoidCatastropheCost = NumberPrefixer.Parse("5T");

    [SerializeField, Tooltip("Text element where the time left for upgrade destruction will be set.")]
    private TextMeshProUGUI m_TimeForUpgradeDowngrade;

    [SerializeField, Tooltip("Explanation field")]
    private TextMeshProUGUI m_CatastropheExplanation;

    [SerializeField, Tooltip("Text element where the payment amount should be displayed")]
    private TextMeshProUGUI m_AcceptPaymentButtonText;

    [SerializeField, Tooltip("Time the user has to react to catastrophe")]
    private float m_UpgradeDowngradeDelay = 20f;

    [SerializeField, TextArea, Tooltip("Text that will be displayed around the disaster that happens. ^ determines where the disaster will be placed")]
    private string m_CatastropheTextContext = "^ is approaching!";

    [SerializeField, Tooltip("How long the warning will be shown in the middle of the screen")]
    private float m_MiddleScreenTime = 2.5f;

    [SerializeField, Tooltip("How long the warning will be shown in the corner of the screen")]
    private float m_CornerScreenTime = 7.5f;

    [SerializeField, Tooltip("Transform of where the text should be when in the middle of the screen")]
    private Transform m_MiddlePosition;

    [SerializeField, Tooltip("Transform of where the text should be when in the corner of the screen")]
    private Transform m_CornerPosition;
    private Vector3 originalPosition;

    private Vector3 big = new Vector3(1, 1, 1);
    private Vector3 small = new Vector3(0.7f, 0.7f, 1);
    private Vector3 gone = new Vector3(0, 0, 1);

    public static event Action OnCatastropheAvoided;
    public static event Action OnCatastropheIgnored;

    float time = 1;

    private void Start()
    {
        originalPosition = transform.position;
        m_AcceptPaymentButton.onClick.AddListener(() => AcceptPayment()); // Setup callback so payment is accepted on btn click
    }

    private void OnEnable()
    {
        CatastropheManager.OnCatastropheStart += ShowCatastropheWarning;
    }

    private void OnDisable()
    {
        CatastropheManager.OnCatastropheStart -= ShowCatastropheWarning;
    }

    private void ShowCatastropheWarning(Catastrophe catastrophe , Upgrade upgrade2Downgrade)
    {
        if (m_CatastropheText == null)
            return;

        // Set catastrophe text to reflect what the disaster is
        string display = m_CatastropheTextContext;
        int formatIdx = display.IndexOf('^');
        display = display.Remove(formatIdx, formatIdx + 1); // remove ^
        display = display.Insert(formatIdx, catastrophe.CatastropheName); // Insert catastrophe name at ^

        m_CatastropheText.text = display;

        m_CatastropheExplanation.text = $"{upgrade2Downgrade.UpgradeName} will be downgraded!";
        m_AcceptPaymentButtonText.text = $"Pay ${NumberPrefixer.Prefix(m_AvoidCatastropheCost)} to avoid";

        // Scale in
        transform.position = m_MiddlePosition.position;
        LeanTween.scale(gameObject, big, time).setOnComplete(() => {
            // Move to corner after delay
            Invoke("Move2Corner", m_MiddleScreenTime);
        });

        StartCoroutine(DowngradeUpgradeAfter(upgrade2Downgrade, m_UpgradeDowngradeDelay));
    }

    public void Reset()
    {
        // Scale down animation
        LeanTween.scale(gameObject, gone, time * 2).setOnComplete(() => {
            // Move to original position
            transform.position = originalPosition;
        });
    }

    // Move to corner
    public void Move2Corner()
    {
        LeanTween.move(gameObject, m_CornerPosition.position, 1f);
        LeanTween.scale(gameObject, small, 1f);

        // After moved to corner wait delay and remove text
        // Invoke("Reset", m_CornerScreenTime);
    }

    private IEnumerator DowngradeUpgradeAfter(Upgrade upgrade, float delay)
    {
        float timeSpent = 0f;
        while (timeSpent < delay)
        {
            timeSpent += Time.deltaTime;
            m_TimeForUpgradeDowngrade.text = $"Time left: {Mathf.Clamp(delay - timeSpent, 0f, Mathf.Infinity)}";
            yield return new WaitForEndOfFrame();
        }
        upgrade.Downgrade();

        Reset();
    }

    private void AcceptPayment()
    {
        // Dont accept if not enough money
        if (EconomyManager.Instance.GetBalance < m_AvoidCatastropheCost)
            return;

        EconomyManager.Instance.RegisterPurchase(m_AvoidCatastropheCost);

        // Stop downgrade from happening
        StopAllCoroutines();

        Reset();
    }
}