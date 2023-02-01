public class ElectricCars : Upgrade
{
    public override string UpgradeName => "Electric Cars";
    protected override double m_BasePrice => NumberPrefixer.Parse("5T");
    protected override double m_BaseEmissionInfluence => NumberPrefixer.Parse("-25M");
}