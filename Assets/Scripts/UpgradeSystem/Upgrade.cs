public abstract class Upgrade : IBudgetInfluencer, IPollutionInfluencer
{
    public abstract string UpgradeName { get; }
    public virtual double GetYearlyBudgetInfluence() { return 0d; }
    public virtual double GetEmissionInfluence() { return 0d; }
}
