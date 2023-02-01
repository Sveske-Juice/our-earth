public class IncreaseGoodsProduction : Upgrade
{
    public override string UpgradeName => "Increase Goods Production";
    protected override double m_BasePrice => NumberPrefixer.Parse("5T");
    protected override double m_BaseEmissionInfluence => NumberPrefixer.Parse("-25M");
}
