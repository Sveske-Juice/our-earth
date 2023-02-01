public class DecreaseGreenSubsidies : Upgrade
{
    public override string UpgradeName => "Decrease Green Subsidies";
    protected override double m_BasePrice => NumberPrefixer.Parse("5T");
    protected override double m_BaseEmissionInfluence => NumberPrefixer.Parse("-25M");
}
