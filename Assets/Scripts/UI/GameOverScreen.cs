using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOverScreen : MonoBehaviour
{
    public GameObject menu;
    private void OnEnable()
    {
        TimeManager.OnYearChange += CheckForLoose;
    }
    private void OnDisable()
    {
        TimeManager.OnYearChange -= CheckForLoose;
    }

    private void CheckForLoose(int year)
    {
        if (year == 2030)
        {
            menu.SetActive(true);
        }
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("Game");
    }

}
