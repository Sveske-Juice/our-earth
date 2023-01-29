using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ContinentHighlighter : MonoBehaviour
{
    private GameObject m_SelectedContinent;

    private void OnEnable()
    {
        ContinentEventInitiator.OnContinentClick += HiglightContinent;
    }

    private void OnDisable()
    {
        ContinentEventInitiator.OnContinentClick -= HiglightContinent;
    }

    private void HiglightContinent(GameObject continent)
    {
        // TODO implement this
        // Debug.Log($"Highlighting {continent.name}");
    }
}
