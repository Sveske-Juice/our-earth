public class WalkableInfrastructure : Upgrade
{
    public override string UpgradeName => "Walkable Infrastructure";
    public override string UpgradeExplanation => "fill me up";
    protected override double m_BasePrice => NumberPrefixer.Parse("15T"); // 15T
    protected override double m_BaseEmissionInfluence => NumberPrefixer.Parse("-50M"); // -50M
}