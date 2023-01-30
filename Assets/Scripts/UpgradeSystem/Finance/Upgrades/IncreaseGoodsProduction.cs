public class IncreaseGoodsProduction : Upgrade
{
    public override string UpgradeName => "Increase Goods Production";
    protected override double m_BasePrice => 5000000000000d; // 5T
    protected override double m_BaseEmissionInfluence => -25000000d; // -25M
}
