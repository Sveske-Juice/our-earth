using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PollutionManager : MonoBehaviour
{
    private static PollutionManager s_Instance;
    private PollutionData m_PollutionData;

    private static List<IPollutionInfluencer> m_PollutionInfluencers = new List<IPollutionInfluencer>();
    public static void RegisterPollutionInfluencer(IPollutionInfluencer influencer) { m_PollutionInfluencers.Add(influencer); UpdateEmission(); }
    public static void UnregisterPollutionInfluencer(IPollutionInfluencer influencer) { m_PollutionInfluencers.Remove(influencer); UpdateEmission(); }
    
    private static double m_EmissionPrYear;
    private static double m_BaseEmissionsPrYear = NumberPrefixer.Parse("3B"); // 3B
    public static PollutionManager Instance => s_Instance;

    /// <summary> Event that gets raised when the yearly emissions change. Fx when an upgrade is purchased that influence the emissions. </summary>
    public static event Action<double> OnYearlyEmissionChange;
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

    private void Start()
    {
        LoadData();

        // Update emissions pr year when initializing
        UpdateEmission();
    }

    private void OnEnable()
    {
        Upgrade.OnUpgradePerformed += OnUpgradePerformed;
    }

    private void OnDisable()
    {
        Upgrade.OnUpgradePerformed -= OnUpgradePerformed;
    }

    private void OnUpgradePerformed(Upgrade upgrade) => UpdateEmission();

    /// <summary>
    /// Will update the emission pr second value based on
    /// how much each continent is emitting.
    /// </summary>
    private static void UpdateEmission()
    {
        double oldEmissionPrSecond = m_EmissionPrYear;
        m_EmissionPrYear = m_BaseEmissionsPrYear;
        for(int i = 0; i < m_PollutionInfluencers.Count; i++)
        {
            m_EmissionPrYear += m_PollutionInfluencers[i].GetEmissionInfluence();
        }

        // Raise event if updated value is different to avoid raising event when there is no difference
        if (oldEmissionPrSecond != m_EmissionPrYear)
            OnYearlyEmissionChange?.Invoke(m_EmissionPrYear);
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