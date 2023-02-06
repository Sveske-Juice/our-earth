using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ContinentHighlighter : MonoBehaviour
{
    private Material[] m_SelectedOriginalMaterials;
    private ContinentUpgradeSystem m_SelectedContinent;
    public Material selected;

    private void OnEnable()
    {
        ContinentEventInitiator.OnContinentSelect += HiglightContinent;
        ContinentEventInitiator.OnContinentDeselect += UnhiglightContinent;
    }

    private void OnDisable()
    {
        ContinentEventInitiator.OnContinentSelect -= HiglightContinent;
        ContinentEventInitiator.OnContinentDeselect -= UnhiglightContinent;
    }

    private void HiglightContinent(GameObject continent)
    {
        Debug.Log($"Selecting {continent.name}");
        ContinentUpgradeSystem system = continent.GetComponent<ContinentUpgradeSystem>();

        m_SelectedContinent = system;
        Renderer renderer = continent.GetComponent<Renderer>();
        m_SelectedOriginalMaterials = renderer.materials;

        // Replace all materials with the selected variant
        Material[] selectedMats = new Material[renderer.materials.Length];
        for (int i = 0; i < selectedMats.Length; i++)
        {
            selectedMats[i] = selected;
        }

        renderer.materials = selectedMats;
    }

    private void UnhiglightContinent(GameObject continent)
    {
        Debug.Log($"UN!-selecting {continent.name}");
        m_SelectedContinent.highlighted = false;

        // Replace all the selected material back to the original material
        Renderer renderer = m_SelectedContinent.GetComponent<Renderer>();
        Material[] originalMats = new Material[renderer.materials.Length];
        for (int i = 0; i < originalMats.Length; i++)
        {
            originalMats[i] = m_SelectedOriginalMaterials[i];
        }

        renderer.materials = originalMats;

        m_SelectedContinent = null;
        m_SelectedOriginalMaterials = null;
    }
}
