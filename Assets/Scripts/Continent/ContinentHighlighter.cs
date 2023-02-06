using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ContinentHighlighter : MonoBehaviour
{
    private GameObject m_SelectedContinent;
    public Material green;
    public Material selected;

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
        m_SelectedContinent = continent;
        if (m_SelectedContinent != continent)
            m_SelectedContinent.GetComponent<Renderer>().material = green;


        ContinentUpgradeSystem system = continent.GetComponent<ContinentUpgradeSystem>();
        if (!system.highlighted)
        {
            system.highlighted = true;
            continent.GetComponent<Renderer>().material = selected;
            //Debug.Log(continent.GetComponent<Renderer>().material.ToString());
        }
        else
        {
            system.highlighted = false;
            print("green");
            continent.GetComponent<Renderer>().material = green;
        }

    }

}
