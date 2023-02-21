using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class CatastropheWariningDisplay : MonoBehaviour
{
    private Catastrophe m_Catastrophe;
    private Upgrade m_Upgrade2Downgrade;
    private Transform m_CornerPosition;

    public Catastrophe Catastrophe { get { return m_Catastrophe; } set { m_Catastrophe = value; }}
    public Upgrade Upgrade2Downgrade { get { return m_Upgrade2Downgrade; } set { m_Upgrade2Downgrade = value; }}
    public Transform CornerPosition { get { return m_CornerPosition; } set { m_CornerPosition = value; }}

    [Header("Settings")]
    [SerializeField, Tooltip("Time the user has to react to catastrophe.")]
    private float m_UpgradeDowngradeDelay = 20f;

    [SerializeField, Tooltip("How long the warning will be shown in the middle of the screen.")]
    private float m_MiddleScreenTime = 2.5f;

    [SerializeField, Tooltip("Amount to be paid to avoid catastrophe")]
    private double m_AvoidCatastropheCost = NumberPrefixer.Parse("5T");

    [SerializeField, TextArea, Tooltip("Text that will be displayed around the disaster that happens. ^ determines where the disaster will be placed")]
    private string m_CatastropheTextContext = "^ is approaching!";

    [SerializeField, Tooltip("How long it should take for the warning to appear in full size. (in seconds)")]
    private float m_AppearTime = 1f;

    [SerializeField, Tooltip("How long it should take for the warning to disappear to zero. (in seconds)")]
    private float m_DisappearTime = 1f;


    [Header("References")]
    [SerializeField, Tooltip("Text component that will hold the type of catastrophe that will happen.")]
    private TextMeshProUGUI m_CatastropheText;

    [SerializeField, Tooltip("Button that will accept catastrophe payment")]
    private Button m_AcceptPaymentButton;

    [SerializeField, Tooltip("Explanation field, which will inform what upgrades will be downgraded etc.")]
    private TextMeshProUGUI m_CatastropheExplanation;

    [SerializeField, Tooltip("Text element where the payment amount should be displayed")]
    private TextMeshProUGUI m_AcceptPaymentButtonText;


    [Header("Element Size Control")]
    [SerializeField, Tooltip("Scaling of element when being displayed on the middle of the screen.")]
    private Vector3 m_MiddleScreenSize = new Vector3(1, 1, 1);
    
    [SerializeField, Tooltip("Scaling of element when being displayed on the corner of the screen.")]
    private Vector3 m_CornerScreenSize = new Vector3(1, 1, 1);
    
    private RectTransform m_RectTransform;

    public static event Action OnCatastropheAvoided;
    public static event Action OnCatastropheIgnored;

    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        if (m_Catastrophe == null || m_Upgrade2Downgrade == null)
        {
            Debug.LogError("Catastrophe or upgrade2downgrade, was not set but are required! Will self-terminate!");
            Destroy(gameObject);
            return;
        }

        // Update values based on what was passed from spawner
        SetupWarningUI();

        // Start countdown of when the catastrophe will actually happen
        StartCoroutine(DowngradeUpgradeAfter(m_Upgrade2Downgrade, m_UpgradeDowngradeDelay));

        // Scale the warning up to its original size
        LeanTween.scale(gameObject, m_MiddleScreenSize, m_AppearTime).setOnComplete(() => {
            // Move to corner after delay once fully scaled
            StartCoroutine(Move2Corner(m_MiddleScreenTime));
        });
    }

    /// <summary>
    /// Will setup all the UI values based on the catastrophe happening and the upgrade to downgrade.
    /// </summary>
    private void SetupWarningUI()
    {
        // Set catastrophe text to reflect what the disaster is
        string displayTxt = m_CatastropheTextContext;
        int formatIdx = displayTxt.IndexOf('^');
        displayTxt = displayTxt.Remove(formatIdx, formatIdx + 1); // remove ^
        displayTxt = displayTxt.Insert(formatIdx, m_Catastrophe.CatastropheName); // Insert catastrophe name at ^
        m_CatastropheText.text = displayTxt;

        // Set accept payment button text
        m_AcceptPaymentButtonText.text = $"Pay ${NumberPrefixer.Prefix(m_AvoidCatastropheCost)} to avoid";

        // Setup payment callback
        m_AcceptPaymentButton.onClick.AddListener(() => AcceptPayment());
    }

    /// <summary> Will handle moving the warning to the corner of the screen after delay, and re-sizing to its corner scale. </summary>
    private IEnumerator Move2Corner(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        LeanTween.move(gameObject, m_CornerPosition.position, 1f);
        LeanTween.scale(gameObject, m_CornerScreenSize, 1f);
    }

    private IEnumerator DowngradeUpgradeAfter(Upgrade upgrade, float delay)
    {
        float timeSpent = 0f;
        UpgradeCategory upgradeCategory = upgrade.ParentCategory;
        ContinentUpgradeSystem upgradeSystem = upgrade.ParentCategory.ParentContinentUpgradeSystem;

        while (timeSpent < delay)
        {
            timeSpent += Time.deltaTime;
            m_CatastropheExplanation.text = $"A random upgrade will be downgraded in {Mathf.Round(Mathf.Clamp(delay - timeSpent, 0f, Mathf.Infinity))} seconds!";
            yield return new WaitForEndOfFrame();
        }
        upgrade.Downgrade();
        OnCatastropheIgnored?.Invoke();
        Reset();
    }

    private void AcceptPayment()
    {
        // Dont accept if not enough money
        if (EconomyManager.Instance.GetBalance < m_AvoidCatastropheCost)
            return;

        EconomyManager.Instance.RegisterPurchase(m_AvoidCatastropheCost);
        AudioManager.Instance.Play("Upgrade");

        // Stop downgrade from happening
        StopAllCoroutines();

        Reset();
        OnCatastropheAvoided?.Invoke();
    }

    /// <summary> Will scale the warning down to zero, and then self terminate. </summary>
    private void Reset()
    {
        // Scale warning down
        LeanTween.scale(gameObject, new Vector3(0f, 0f, 1f), m_DisappearTime).setOnComplete(() => {
            Destroy(gameObject);
        });
    }
}