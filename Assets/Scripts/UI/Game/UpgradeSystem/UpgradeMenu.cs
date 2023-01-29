using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        // Remove all category ui buttons
        for (int i = 0; i < m_CategoryContent.childCount; i++)
        {
            Destroy(m_CategoryContent.GetChild(i));
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
            CreateCategoryTab(category);
        }
    }

    /// <summary>
    /// Creates the upgrades ui elements which includes all the upgrades attached
    /// to a specific <seealso cref="UpgradeCategory"/>.
    /// </summary>
    private void CreateUpgradesBody(UpgradeCategory upgradeCategory)
    {
        
    }

    private void CreateCategoryTab(UpgradeCategory category)
    {
        // Create category object
        GameObject categoryTab = Instantiate(m_CategoryPrefab, m_CategoryContent);
        Button categoryBtn = categoryTab.GetComponent<Button>();
        TextMeshProUGUI categoryText = categoryTab.GetComponentInChildren<TextMeshProUGUI>();

        // Populate object with the upgrade category data
        categoryBtn.onClick.AddListener(() => OnCategoryButtonClick(category)); // Set callback so we know when and what category was clicked
        categoryText.text = category.CategoryName; // Set category name
        // TODO set icon or something
    }

    private void OnCategoryButtonClick(UpgradeCategory categoryClicked)
    {
        Debug.Log($"Clicked category: {categoryClicked.CategoryName}");
        CreateUpgradesBody(categoryClicked);
    }

}
