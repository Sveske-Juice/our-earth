using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
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
        if (year == 2040 && PollutionManager.EmissionsPrYear == 0)
        {
            menu.SetActive(true);
        }
    }
}
