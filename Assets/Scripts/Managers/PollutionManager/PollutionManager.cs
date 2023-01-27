using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PollutionManager : MonoBehaviour
{
    private PollutionManager m_Instance;
    private PollutionData m_PollutionData;

    private static List<IPollutionInfluencer> m_PollutionInfluencers = new List<IPollutionInfluencer>();
    public static void RegisterPollutionInfluencer(IPollutionInfluencer influencer) { m_PollutionInfluencers.Add(influencer); }
    public static void UnregisterPollutionInfluencer(IPollutionInfluencer influencer) { m_PollutionInfluencers.Remove(influencer); }
    
    private double m_EmissionPrSecond = 0d;

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
        LoadData();
    }

    /// <summary>
    /// Will update the emission pr second value based on
    /// how many constructions are emitting emissions.
    /// </summary>
    private void UpdateEmission()
    {
        m_EmissionPrSecond = 0;
        // TODO should loop through every pollution influencer and sum their influnce and store it in m_EmissionPrSecond
        for(int i = 0; i < m_PollutionInfluencers.Count; i++)
        {
            m_EmissionPrSecond += m_PollutionInfluencers[i].GetEmissionInfluence();
        }

    }

    private void LoadData()
    {
        // TODO Load progress data from disk

        // If there's no progress data then just load defaults
        m_PollutionData = new PollutionData();
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
public class PollutionData
{
    public double pollutionEmitted = 0d;
}