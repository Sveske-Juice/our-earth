using System;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField, Tooltip("The year where the game is over. If the user managed to get emissions down to 0 they will win, otherwise they'll lose")]
    private int m_GameOverYear = DateTime.Now.Year + 1 + 100;

    public static event Action OnGameWin;
    public static event Action OnGameLoose;

    private void OnEnable()
    {
        TimeManager.OnYearChange += CheckForWinLoose;
    }
    private void OnDisable()
    {
        TimeManager.OnYearChange -= CheckForWinLoose;
    }

    private void CheckForWinLoose(int year)
    {
        if (year != m_GameOverYear)
            return;

        double emissions = PollutionManager.EmissionsPrYear;
        if (emissions <= 0d)
        {
            OnGameWin?.Invoke();
            return;
        }

        OnGameLoose?.Invoke();
    }
}
