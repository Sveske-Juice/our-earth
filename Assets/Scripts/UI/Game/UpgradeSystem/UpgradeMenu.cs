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
        ContinentEventInitiator.OnContinentClick += TogglePopout;
        EconomyManager.OnBalanceChange += OnBalanceChange;
        Upgrade.OnUpgradePerformed += OnUpgradePerformed;
    }

    private void OnDisable()
    {
        ContinentEventInitiator.OnContinentClick -= TogglePopout;
        EconomyManager.OnBalanceChange -= OnBalanceChange;
        Upgrade.OnUpgradePerformed -= OnUpgradePerformed;
    }

    private void OnBalanceChange(double balance) => UpdateUpgrades();
    private void OnUpgradePerformed(Upgrade upgrade) => UpdateUpgrades();

    /// <summary>
    /// Will toggle the popout menu. Will also handle 
    /// calling nescesary functions to show correct info.
    /// </summary>
    private void TogglePopout(GameObject continent)
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

        // Close the menu if the clicked continent is the same as the last clicked continent and the menu is already opened
        if (m_PopoutAnimator.IsOut && m_LastClickedContinentSystem?.LinkedContinent == clickedContinentSystem.LinkedContinent)
        {
            m_PopoutAnimator.PopBack();
            return;
        }

        // A new continent was clicked - show new data relevant to clicked continent
        CreateMenu(clickedContinentSystem);
        m_PopoutAnimator.PopOut();
        m_LastClickedContinentSystem = clickedContinentSystem;
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
        // Create upgrade object
        GameObject upgradeObj = Instantiate(m_UpgradePrefab, m_UpgradeContent);
        GameObject upgradeBoxObj = upgradeObj.transform.GetChild(1).gameObject;

        TextMeshProUGUI upgradeTitle = upgradeObj.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI upgradePrice = upgradeBoxObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI emissionImpact = upgradeBoxObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI budgetImpact = upgradeBoxObj.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI level = upgradeBoxObj.transform.GetChild(3).GetComponent<TextMeshProUGUI>();

        upgradeBoxObj.GetComponent<Button>().onClick.AddListener(() => OnUpgradeButtonClick(upgrade));

        // Populate object with the upgrade data
        upgradeTitle.text = upgrade.UpgradeName; // Upgrade name
        upgradePrice.text = $"${NumberPrefixer.Prefix(upgrade.GetNextUpgradePrice())}"; // Price for next upgrade
        emissionImpact.text = $"Emission impact: {NumberPrefixer.Prefix(upgrade.BaseEmissionInfluence)}"; // Emission influence for next upgrade
        budgetImpact.text = $"Budget impact: {NumberPrefixer.Prefix(upgrade.BaseBudgetInfluence)}"; // Budget influence for next upgrade
        level.text = $"Level: {upgrade.GetUpgradeLevel}"; // Set upgrade level

        // If the upgrade is not upgradable show lock ui and reason
        string upgradeErr = upgrade.IsUpgradable();
        if (upgradeErr == "")
            return;

        GameObject upgradeLockObj = upgradeObj.transform.GetChild(2).gameObject;
        upgradeLockObj.SetActive(true);
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
        // TODO set icon or something
    }

    private void OnCategoryButtonClick(UpgradeCategory categoryClicked)
    {
        m_LastClickedCategory = categoryClicked;
        
        UpdateUpgrades();
    }

    private void OnUpgradeButtonClick(Upgrade upgrade)
    {
        // Actually upgrade to next level
        upgrade.Upgrade2NextLevel();

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
