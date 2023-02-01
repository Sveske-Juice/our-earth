public class FusionEnergyResearch : Upgrade
{
    public override string UpgradeName => "Fusion Energy Research";
    protected override double m_BasePrice => NumberPrefixer.Parse("5T");
    protected override double m_BaseEmissionInfluence => NumberPrefixer.Parse("-25M");
}
