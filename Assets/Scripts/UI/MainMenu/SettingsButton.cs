using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    [SerializeField]
    private GameObject m_SettingsMenu;

    public void OnSettingsButtonClick()
    {
        // Toggle menu
        m_SettingsMenu.SetActive(!m_SettingsMenu.activeSelf);
    }

}
