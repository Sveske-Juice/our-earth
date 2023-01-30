public class FusionEnergyResearch : Upgrade
{
    public override string UpgradeName => "Fusion Energy Research";
    protected override double m_BasePrice => 5_000_000_000_000d; // 5T
    protected override double m_BaseEmissionInfluence => -25_000_000d; // -25M
}
