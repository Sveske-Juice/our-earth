public class PublicTransportInfrastructure : Upgrade
{
    public override string UpgradeName => "Public Transport Infrastructure";

    protected override double m_BasePrice => NumberPrefixer.Parse("20T");
}