using UnityEngine;

/// <summary>
/// Modifies an upgrade's base emission and budget influence.
/// Its here so that every upgrade can have a number of modifiers which 
/// contribute to the total emission and budget influence.
/// So each upgrade can have a "special effect" and influence other upgrades.
/// </summary>
[System.Serializable]
public class UpgradeModifier : IBudgetInfluencer, IPollutionInfluencer
{
    private static int s_IdIncrementor;
    private int m_Id = s_IdIncrementor++;
    [SerializeField] private double m_EmissionInfluence;
    [SerializeField] private double m_BudgetInfluence;

    public double GetYearlyBudgetInfluence() => m_BudgetInfluence;
    public double GetEmissionInfluence() => m_EmissionInfluence;

    public override string ToString()
    {
        return $"Special effect ({GetEmissionInfluence()} emission influence, {GetYearlyBudgetInfluence()} budget influence) Id: {m_Id}";
    }

    public override int GetHashCode()
    {
        return m_Id;
    }

    /// <summary>
    /// Use id to compare objects. Used for when to remove a modifier again.
    /// </summary>
    public override bool Equals(object obj)
    {
        if (obj == null || ! (obj is UpgradeModifier))
            return false;

        return GetHashCode() == ((UpgradeModifier) obj).GetHashCode();
    }
}
