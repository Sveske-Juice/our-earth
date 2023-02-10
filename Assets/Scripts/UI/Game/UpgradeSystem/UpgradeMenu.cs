using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[RequireComponent(typeof(PopoutAnim))]
public class UpgradeMenu : MonoBehaviour
{
    [SerializeField, Tooltip("Text element of where the continent name should be placed.")]
    private TextMeshProUGUI m_ContinentNameElement;

    [Header("Category Display Body")]
    [SerializeField]
    private Transform m_CategoryContent;

    [SerializeField]
    private GameObject m_CategoryPrefab;

    [Header("Upgrade Display Body")]
    [SerializeField]
    private Transform m_UpgradeContent;
    
    [SerializeField]
    private GameObject m_UpgradePrefab;

    private PopoutAnim m_PopoutAnimator;
    private ContinentUpgradeSystem m_LastClickedContinentSystem;
    private UpgradeCategory m_LastClickedCategory;


    private void Awake()
    {
        m_PopoutAnimator = GetComponent<PopoutAnim>();
    }

    private void OnEnable()
    {
        ContinentEventInitiator.OnContinentSelect += ShowMenu;
        ContinentEventInitiator.OnContinentDeselect += CloseMenu;
        EconomyManager.OnBalanceChange += OnBalanceChange;
        Upgrade.OnUpgradePerformed += OnUpgradePerformed;
        Upgrade.OnDowngradePerformed += OnUpgradePerformed;
    }

    private void OnDisable()
    {
        ContinentEventInitiator.OnContinentSelect -= ShowMenu;
        ContinentEventInitiator.OnContinentDeselect -= CloseMenu;
        EconomyManager.OnBalanceChange -= OnBalanceChange;
        Upgrade.OnUpgradePerformed -= OnUpgradePerformed;
        Upgrade.OnDowngradePerformed -= OnUpgradePerformed;
    }

    private void OnBalanceChange(double balance) => UpdateUpgrades();
    private void OnUpgradePerformed(Upgrade upgrade) => UpdateUpgrades();

    /// <summary>
    /// Will toggle the popout menu. Will also handle 
    /// calling nescesary functions to show correct info.
    /// </summary>
    private void ShowMenu(GameObject continent)
    {
        // Get the upgrade system on the continent clicked
        ContinentUpgradeSystem clickedContinentSystem = continent.GetComponent<ContinentUpgradeSystem>();

        if (clickedContinentSystem == null)
        {
            Debug.LogError($"Continent ({continent.name}) does not have a upgrade system attached which is required.");
            return;
        }

        // Clear the menu from old data
        ClearMenu();

        // A new continent was clicked - show new data relevant to clicked continent
        CreateMenu(clickedContinentSystem);
        m_PopoutAnimator.PopOut();
        m_LastClickedContinentSystem = clickedContinentSystem;
    }

    private void CloseMenu(GameObject continent)
    {
        m_PopoutAnimator.PopBack();
    }

    private void ClearMenu()
    {
        ClearCategories();
        ClearUpgrades();
    }

    private void ClearCategories()
    {
        // Remove all category ui
        for (int i = 0; i < m_CategoryContent.childCount; i++)
        {
            Destroy(m_CategoryContent.GetChild(i).gameObject);
        }
    }

    private void ClearUpgrades()
    {
        // Remove all upgrade ui
        for (int i = 0; i < m_UpgradeContent.childCount; i++)
        {
            Destroy(m_UpgradeContent.GetChild(i).gameObject);
        }
    }

    private void CreateMenu(ContinentUpgradeSystem upgradeSystem)
    {
        // Set continent title
        m_ContinentNameElement.text = upgradeSystem.LinkedContinent;

        // Create upgrade categories assigned to this upgrade system.
        CreateCategoriesBody(upgradeSystem);
    }

    /// <summary>
    /// Creates the category ui elements which includes all the categories attached
    /// to a specific <seealso cref="ContinentUpgradeSystem"/>.
    /// </summary>
    private void CreateCategoriesBody(ContinentUpgradeSystem upgradeSystem)
    {
        List<UpgradeCategory> categories = upgradeSystem.UpgradeCategories;

        for (int i = 0; i < categories.Count; i++)
        {
            UpgradeCategory category = categories[i];
            CreateCategoryContainer(category);
        }
    }

    /// <summary>
    /// Creates the upgrades ui elements which includes all the upgrades attached
    /// to a specific <seealso cref="UpgradeCategory"/>.
    /// </summary>
    private void CreateUpgradesBody(UpgradeCategory upgradeCategory)
    {
        if (upgradeCategory == null)
            return;
        
        List<Upgrade> upgrades = upgradeCategory.Upgrades;

        for (int i = 0; i < upgrades.Count; i++)
        {
            Upgrade upgrade = upgrades[i];
            CreateUpgradeContainer(upgrade);
        }
    }

    /// <summary> Will handle creating one container with data about an upgrade in the upgrades scroll view. </summary>
    private void CreateUpgradeContainer(Upgrade upgrade)
    {
        // TODO read objects from script attached to prefab and serialized from inspector instead og hardcode lol

        // Create upgrade object
        GameObject upgradeObj = Instantiate(m_UpgradePrefab, m_UpgradeContent);
        GameObject upgradeBoxObj = upgradeObj.transform.GetChild(5).GetChild(0).gameObject;

        TextMeshProUGUI upgradeTitle = upgradeObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI explanationText = upgradeObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI levelText = upgradeObj.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI emissionImpact = upgradeObj.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI budgetImpact = upgradeObj.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI upgradePrice = upgradeBoxObj.GetComponentInChildren<TextMeshProUGUI>();

        upgradeBoxObj.GetComponent<Button>().onClick.AddListener(() => OnUpgradeButtonClick(upgrade));

        // Populate object with the upgrade data
        upgradeTitle.text = upgrade.UpgradeName; // Upgrade name
        explanationText.text = upgrade.UpgradeExplanation; // Upgrade explanation/info about upgrade
        upgradePrice.text = $"${NumberPrefixer.Prefix(upgrade.GetNextUpgradePrice())}"; // Price for next upgrade
        emissionImpact.text = $"Emission impact: {NumberPrefixer.Prefix(upgrade.BaseEmissionInfluence)}"; // Emission influence for next upgrade
        budgetImpact.text = $"Budget impact: {NumberPrefixer.Prefix(upgrade.BaseBudgetInfluence)}"; // Budget influence for next upgrade
        levelText.text = $"Level: {upgrade.GetUpgradeLevel}"; // Set upgrade level

        // If the upgrade is not upgradable show lock ui and reason
        string upgradeErr = upgrade.IsUpgradable();
        if (upgradeErr == "")
            return;

        GameObject upgradeLockObj = upgradeObj.transform.GetChild(5).GetChild(1).gameObject;
        GameObject upgradeLockedBackground = upgradeObj.transform.GetChild(6).gameObject;
        upgradeLockObj.SetActive(true);
        upgradeLockedBackground.SetActive(true);
        upgradeLockObj.GetComponentInChildren<TextMeshProUGUI>().text = upgradeErr; // Set reason to why upgrade is not possible
    }

    /// <summary> Will handle creating one container with data about a category in the categories tab scroll view. </summary>
    private void CreateCategoryContainer(UpgradeCategory category)
    {
        // Create category object
        GameObject categoryObj = Instantiate(m_CategoryPrefab, m_CategoryContent);
        Button categoryBtn = categoryObj.GetComponent<Button>();
        TextMeshProUGUI categoryText = categoryObj.GetComponentInChildren<TextMeshProUGUI>();

        // Populate object with the upgrade category data
        categoryBtn.onClick.AddListener(() => OnCategoryButtonClick(category)); // Set callback so we know when and what category was clicked
        categoryText.text = category.CategoryName; // Set category name
        categoryObj.GetComponentInChildren<RawImage>().texture = category.CategoryIcon;
    }

    private void OnCategoryButtonClick(UpgradeCategory categoryClicked)
    {
        m_LastClickedCategory = categoryClicked;

        //Play Sound
        FindObjectOfType<AudioManager>().Play("Button");

        UpdateUpgrades();
    }

    private void OnUpgradeButtonClick(Upgrade upgrade)
    {
        // Actually upgrade to next level
        upgrade.Upgrade2NextLevel();

        //Play Sound
        FindObjectOfType<AudioManager>().Play("Upgrade");

        // Update UI
        UpdateUpgrades();
    }

    /// <summary>
    /// Will re-generate the upgrades body with new data.
    /// </summary>
    private void UpdateUpgrades()
    {
        ClearUpgrades();
        CreateUpgradesBody(m_LastClickedCategory);
    }
}
