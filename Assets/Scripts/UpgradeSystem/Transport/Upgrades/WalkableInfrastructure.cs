public class WalkableInfrastructure : Upgrade
{
    public override string UpgradeName => "Walkable Infrastructure";
    protected override double m_StartPrice => 15000000000000d; // 15T
    protected override double m_StartEmissionInfluence => -50000000d; // -50M
}