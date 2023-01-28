using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PopoutAnim))]
public class UpgradeMenu : MonoBehaviour
{
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
        Debug.Log("Clearing Menu...");
    }

    private void CreateMenu(ContinentUpgradeSystem upgradeSystem)
    {
        Debug.Log("Creating menu...");
    }

}
