using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[RequireComponent(typeof(PopoutAnim))]
public class UpgradeMenu : MonoBehaviour
{
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

    private void Awake()
    {
        m_PopoutAnimator = GetComponent<PopoutAnim>();
    }

    private void OnEnable()
    {
        ContinentEventInitiator.OnContinentClick += TogglePopout;
    }

    private void OnDisable()
    {
        ContinentEventInitiator.OnContinentClick -= TogglePopout;
    }

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

        // Populate object with the upgrade data
        upgradeTitle.text = upgrade.UpgradeName;
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
        // Clear upgrades ui from last active category
        ClearUpgrades();
        
        CreateUpgradesBody(categoryClicked);
    }

}
