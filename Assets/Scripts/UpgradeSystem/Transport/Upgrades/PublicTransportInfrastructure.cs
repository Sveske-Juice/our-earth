using System.Collections.Generic;

public class PublicTransportInfrastructure : Upgrade
{
    public override string UpgradeName => "Public Transport Infrastructure";
    protected override double m_BasePrice => NumberPrefixer.Parse("20T");
    protected override float m_UpgradeScaling => 1.3f;
    protected override double m_BaseEmissionInfluence => NumberPrefixer.Parse("-120M");
    protected override double m_BaseBudgetInfluence => NumberPrefixer.Parse("-8T");
    protected override List<(string, int)> m_RequiredUpgrades => new List<(string, int)> { ("Walkable Infrastructure", 5), ("Electric Cars", 1) };
}
