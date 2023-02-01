public class GreenerConstruction : Upgrade
{
    public override string UpgradeName => "Greener Construction";
    protected override double m_BasePrice => NumberPrefixer.Parse("5T");
    protected override double m_BaseEmissionInfluence => NumberPrefixer.Parse("-25M");
}
