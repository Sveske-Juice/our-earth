using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private TimeManager m_Instance;
    private TimeData m_TimeData;

    [SerializeField, Tooltip("How many seconds that should pass before a new year starts")]
    private double m_SecondsInYear;

    [SerializeField, Tooltip("The year the game will start in")]
    private int m_StartYear = DateTime.Now.Year + 1; // The start year will be the next year from now
    private int m_Year;
    public static event Action<int> OnYearChange;

    private void Awake()
    {
        // Check if another instance already exists
        if (m_Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Else this is the first instance
        m_Instance = this;
    }

    private void Start()
    {
        // TODO Load progress data from disk

        // If there's no progress data then just load defaults
        m_TimeData = new TimeData();

        // Calculate year of the first frame
        m_Year = (int) (m_TimeData.secondsSpent / m_SecondsInYear) + m_StartYear;
    }

    private void Update()
    {
        // Update seconds spent in-game and the year
        m_TimeData.secondsSpent += Time.deltaTime;
        int newYear = (int) (m_TimeData.secondsSpent / m_SecondsInYear) + m_StartYear;

        // If the year changed then raise year change event
        if (m_Year != newYear)
        {
            OnYearChange?.Invoke(newYear);
        }
        m_Year = newYear;

        // Debug.Log($"Seconds spent: {m_TimeData.secondsSpent}");
        // Debug.Log($"Year: {m_Year}");
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