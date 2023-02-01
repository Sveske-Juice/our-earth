public class IncreaseOutsourcing : Upgrade
{
    public override string UpgradeName => "Increase Outsourcing";
    protected override double m_BasePrice => NumberPrefixer.Parse("5T");
    protected override double m_BaseEmissionInfluence => NumberPrefixer.Parse("-25M");
}
