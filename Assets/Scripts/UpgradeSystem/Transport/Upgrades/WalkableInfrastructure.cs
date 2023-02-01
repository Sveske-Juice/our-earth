public class WalkableInfrastructure : Upgrade
{
    public override string UpgradeName => "Walkable Infrastructure";
    protected override double m_BasePrice => NumberPrefixer.Parse("15T"); // 15T
    protected override double m_BaseEmissionInfluence => NumberPrefixer.Parse("-50M"); // -50M
}