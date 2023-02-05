using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class CatastropheWariningDisplay : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Tooltip("Time the user has to react to catastrophe.")]
    private float m_UpgradeDowngradeDelay = 20f;

    [SerializeField, Tooltip("How long the warning will be shown in the middle of the screen.")]
    private float m_MiddleScreenTime = 2.5f;

    [SerializeField, Tooltip("How long the warning will be shown in the corner of the screen.")]
    private float m_CornerScreenTime = 7.5f;

    [SerializeField, Tooltip("Amount to be paid to avoid catastrophe")]
    private double m_AvoidCatastropheCost = NumberPrefixer.Parse("5T");

    [Header("References")]
    [SerializeField, Tooltip("Text component that will hold the type of catastrophe that will happen.")]
    private TextMeshProUGUI m_CatastropheText;

    [SerializeField, Tooltip("Button that will accept catastrophe payment")]
    private Button m_AcceptPaymentButton;

    [SerializeField, Tooltip("Explanation field, which will inform what upgrades will be downgraded etc.")]
    private TextMeshProUGUI m_CatastropheExplanation;

    [SerializeField, Tooltip("Text element where the payment amount should be displayed")]
    private TextMeshProUGUI m_AcceptPaymentButtonText;

    [SerializeField, TextArea, Tooltip("Text that will be displayed around the disaster that happens. ^ determines where the disaster will be placed")]
    private string m_CatastropheTextContext = "^ is approaching!";

    [SerializeField, Tooltip("Transform of where the text should be when in the middle of the screen")]
    private Transform m_MiddlePosition;

    [SerializeField, Tooltip("Transform of where the text should be when in the corner of the screen")]
    private Transform m_CornerPosition;

    private RectTransform m_RectTransform;
    private Vector3 originalPosition;

    [Header("Element Size Control")]
    [SerializeField, Tooltip("Scaling of element when being displayed on the middle of the screen.")]
    private Vector3 m_MiddleScreenSize = new Vector3(1, 1, 1);
    [SerializeField, Tooltip("Scaling of element when being displayed on the corner of the screen.")]
    private Vector3 m_CornerScreenSize = new Vector3(1, 1, 1);

    public static event Action OnCatastropheAvoided;
    public static event Action OnCatastropheIgnored;

    float time = 1;

    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }

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

        m_AcceptPaymentButtonText.text = $"Pay ${NumberPrefixer.Prefix(m_AvoidCatastropheCost)} to avoid";

        // Set middle screen position relative to height
        Vector3 calcedMiddlePos = m_MiddlePosition.position;
        calcedMiddlePos.y += m_RectTransform.rect.height / 2f; // When anchor is in the middle
        transform.position = calcedMiddlePos;

        // Scale in
        LeanTween.scale(gameObject, m_MiddleScreenSize, time).setOnComplete(() => {
            // Move to corner after delay
            Invoke("Move2Corner", m_MiddleScreenTime);
        });

        StartCoroutine(DowngradeUpgradeAfter(upgrade2Downgrade, m_UpgradeDowngradeDelay));
    }

    public void Reset()
    {
        // Scale down animation
        LeanTween.scale(gameObject, Vector2.zero, time * 2).setOnComplete(() => {
            // Move to original position
            transform.position = originalPosition;
        });
    }

    // Move to corner
    public void Move2Corner()
    {
        Vector3 calcedCornerPos = m_CornerPosition.position;
        calcedCornerPos.x -= m_RectTransform.rect.width / 2f;
        calcedCornerPos.y += m_RectTransform.rect.height;

        LeanTween.move(gameObject, calcedCornerPos, 1f);
        LeanTween.scale(gameObject, m_CornerScreenSize, 1f);

        // After moved to corner wait delay and remove text
        // Invoke("Reset", m_CornerScreenTime);
    }

    private IEnumerator DowngradeUpgradeAfter(Upgrade upgrade, float delay)
    {
        float timeSpent = 0f;
        UpgradeCategory upgradeCategory = upgrade.ParentCategory;
        ContinentUpgradeSystem upgradeSystem = upgrade.ParentCategory.ParentContinentUpgradeSystem;

        while (timeSpent < delay)
        {
            timeSpent += Time.deltaTime;
            m_CatastropheExplanation.text = $"{upgradeSystem.LinkedContinent} -> {upgradeCategory.CategoryName} -> {upgrade.UpgradeName} will be downgraded to level {upgrade.GetUpgradeLevel - 1} in {Mathf.Round(Mathf.Clamp(delay - timeSpent, 0f, Mathf.Infinity))} seconds!";
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