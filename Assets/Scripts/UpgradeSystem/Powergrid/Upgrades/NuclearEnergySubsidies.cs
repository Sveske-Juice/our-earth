public class NuclearEnergySubsidies : Upgrade
{
    public override string UpgradeName => "Nuclear Energy Subsidies";
    protected override double m_BasePrice => 5_000_000_000_000d; // 5T
    protected override double m_BaseEmissionInfluence => -25_000_000d; // -25M
}
