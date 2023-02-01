public class NuclearEnergySubsidies : Upgrade
{
    public override string UpgradeName => "Nuclear Energy Subsidies";
    protected override double m_BasePrice => NumberPrefixer.Parse("5T");
    protected override double m_BaseEmissionInfluence => NumberPrefixer.Parse("-25M");
}
