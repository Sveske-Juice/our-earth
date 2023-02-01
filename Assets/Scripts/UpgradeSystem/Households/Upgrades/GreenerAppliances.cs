public class GreenerAppliances : Upgrade
{
    public override string UpgradeName => "Greener Appliances";
    protected override double m_BasePrice => NumberPrefixer.Parse("5T");
    protected override double m_BaseEmissionInfluence => NumberPrefixer.Parse("-25M");
}
