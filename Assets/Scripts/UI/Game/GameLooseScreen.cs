using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLooseScreen : MonoBehaviour
{
    [SerializeField] private GameObject m_GameLooseMenu;
    private void OnEnable()
    {
        GameOverManager.OnGameLoose += ShowLooseMenu;
    }
    private void OnDisable()
    {
        GameOverManager.OnGameLoose -= ShowLooseMenu;
    }

    private void ShowLooseMenu()
    {
        m_GameLooseMenu.SetActive(true);
    }
}
