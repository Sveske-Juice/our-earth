public class RenewableEnergySources : Upgrade
{
    public override string UpgradeName => "Renewable Energy Sources";
    protected override double m_StartPrice => 5000000000000d; // 5T
    protected override double m_StartEmissionInfluence => -25000000d; // -25M
}
