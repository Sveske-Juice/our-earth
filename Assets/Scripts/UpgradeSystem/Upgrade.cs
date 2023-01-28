public abstract class Upgrade : IBudgetInfluencer, IPollutionInfluencer
{
    public virtual double GetYearlyBudgetInfluence() { return 0d; }
    public virtual double GetEmissionInfluence() { return 0d; }
}
