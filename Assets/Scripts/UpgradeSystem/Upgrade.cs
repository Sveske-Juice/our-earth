public abstract class Upgrade : IBudgetInfluencer, IPollutionInfluencer
{
    protected int m_UpgradeLevel = 1;
    protected float m_UpgradeScaling = 1.25f;
    protected double m_CurrentUpgradeLevelPrice = 0d;
    protected abstract double m_StartPrice { get; }
    protected virtual double m_StartEmissionInfluence => 0d;
    protected virtual double m_StartBudgetInfluence => 0d;

    public abstract string UpgradeName { get; }
    public virtual double GetYearlyBudgetInfluence() { return 0d; }
    public virtual double GetEmissionInfluence() { return 0d; }

    /// <summary>
    /// Will calculate the price needed to get the upgrade from level 1 to m_UpgradeLevel.
    /// Will store how much that is in m_CurrentUpgradeLevelPrice. Useful for calculating price for next level.
    /// </summary>
    protected virtual double UpdateLevelPrice()
    {
        m_CurrentUpgradeLevelPrice = m_StartPrice;
        for (int i = 1; i < m_UpgradeLevel; i++)
        {
            m_CurrentUpgradeLevelPrice *= m_UpgradeScaling;
        }
        return m_CurrentUpgradeLevelPrice;
    }
    
    /// <summary>
    /// Calculates the price for the next upgrade.
    /// </summary>
    public double GetNextUpgradePrice()
    {
        return UpdateLevelPrice() * m_UpgradeScaling;
    }

    /// <summary>
    /// Calculates the next emission reduction the next upgrade will reduce emissions by.
    /// </summary>
    public double GetNextEmissionInfluence()
    {
        return m_StartEmissionInfluence * m_UpgradeLevel;
    }

    /// <summary>
    /// Calculates the next emission reduction the next upgrade will reduce emissions by.
    /// </summary>
    public double GetNextBudgetInfluence()
    {
        return m_StartBudgetInfluence * m_UpgradeLevel;
    }
}
