public class WalkableInfrastructure : Upgrade
{
    public override string UpgradeName => "Walkable Infrastructure";
    protected override double m_BasePrice => 15_000_000_000_000d; // 15T
    protected override double m_BaseEmissionInfluence => -50_000_000d; // -50M
}