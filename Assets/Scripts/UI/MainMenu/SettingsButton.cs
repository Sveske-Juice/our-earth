using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    [SerializeField]
    private GameObject m_SettingsMenu;

    public void OnSettingsButtonClick()
    {
        //Play Sound
        FindObjectOfType<AudioManager>().Play("Button");

        // Toggle menu
        m_SettingsMenu.SetActive(!m_SettingsMenu.activeSelf);
    }

}
