public class IncreaseGoodsProduction : Upgrade
{
    public override string UpgradeName => "Increase Goods Production";
    public override string UpgradeExplanation => "fill me up";
    protected override double m_BasePrice => NumberPrefixer.Parse("5T");
    protected override double m_BaseEmissionInfluence => NumberPrefixer.Parse("-25M");
}
