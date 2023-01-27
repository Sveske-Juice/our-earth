using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CatastropheManager : MonoBehaviour
{
    [Header("Catastrophe Settings")]
    [SerializeField, Tooltip("Minimum time before a catastrophe can happen")] private float m_MinTimeForCatastrophe = 30f;
    [SerializeField, Tooltip("Its guranteed that a catastrophe will happen before this time")] private float m_MaxTimeForCatastrophe = 60f;

    /// <summary> Event that gets raised when a catastrophe starts. </summary>
    public static event Action<string> OnCatastropheStart;

    private void Start()
    {
        ChooseNextCatastrophe();
    }

    private void ChooseNextCatastrophe()
    {
        float timeForCatastrophe = UnityEngine.Random.Range(m_MinTimeForCatastrophe, m_MaxTimeForCatastrophe);

    }

    private IEnumerator StartCountdownForCatastrophe(float delay, Catastrophe catastropheType)
    {
        yield return new WaitForSecondsRealtime(delay);
        PerformCatastrophe(catastropheType);
    }

    /// <summary>
    /// Gets called when the catastrophe should get performed and will handle performing it.
    /// </summary>
    private void PerformCatastrophe(Catastrophe catastropheType)
    {
        OnCatastropheStart?.Invoke(catastropheType.ToString());
    }
}
