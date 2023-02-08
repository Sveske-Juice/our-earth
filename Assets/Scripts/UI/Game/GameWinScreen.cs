using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWinScreen : MonoBehaviour
{
    [SerializeField] private GameObject m_GameWinMenu;

    private void OnEnable()
    {
        GameOverManager.OnGameWin += ShowWinMenu;
    }
    private void OnDisable()
    {
        GameOverManager.OnGameWin -= ShowWinMenu;
    }

    private void ShowWinMenu()
    {
        m_GameWinMenu.SetActive(true);
    }
}
