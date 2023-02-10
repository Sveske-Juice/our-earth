using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private TimeManager s_Instance;
    private TimeData m_TimeData;

    [SerializeField, Tooltip("The year the game will start in")]
    private int m_StartYear = DateTime.Now.Year + 1; // The start year will be the next year from now
    private int m_Year;
    public static event Action<int> OnYearChange;

    private void Awake()
    {
        // Check if another instance already exists
        if (s_Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Else this is the first instance
        s_Instance = this;
    }

    private void OnEnable()
    {
        YearSkipButton.OnYearSkipClicked += AdvanceYear;
    }
    private void OnDisable()
    {
        YearSkipButton.OnYearSkipClicked -= AdvanceYear;
    }

    private void AdvanceYear()
    {
        OnYearChange?.Invoke(++m_Year);
    }

    private void Start()
    {
        LoadData();
        m_Year = m_StartYear;
        OnYearChange?.Invoke(m_Year); // Raise event so subscribers are notified about initial year
    }

    private void Update()
    {
        // Update seconds spent in-game and the year
        m_TimeData.secondsSpent += Time.deltaTime;
    }

    private void LoadData()
    {
        // TODO check for disk progress

        m_TimeData = new TimeData();
    }
}

/**
<summary>
Data container which stores data about time management of the game etc.
</summary>
<remarks>
The class is serializable which means that it can be
serialized (saved to disk), and unserialized (loaded from disk)
</remarks>
**/
[Serializable]
public class TimeData
{
    public double secondsSpent = 0;
}