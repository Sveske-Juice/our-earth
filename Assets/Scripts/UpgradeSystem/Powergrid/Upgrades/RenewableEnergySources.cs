public class RenewableEnergySources : Upgrade
{
    public override string UpgradeName => "Renewable Energy Sources";
    protected override double m_BasePrice => NumberPrefixer.Parse("5T");
    protected override double m_BaseEmissionInfluence => NumberPrefixer.Parse("-25M");
}
