public class WFHSubsidies : Upgrade
{
    public override string UpgradeName => "Work From Home Subsidies";
    protected override double m_BasePrice => NumberPrefixer.Parse("5T");
    protected override double m_BaseEmissionInfluence => NumberPrefixer.Parse("-25M");
}
