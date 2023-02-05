public class TransportCategory : UpgradeCategory
{
    public override string CategoryName => "Transport";
    public TransportCategory() : base()
    {}

    protected override void GenerateUpgrades()
    {
        m_Upgrades.Add(new ElectricCars());
        m_Upgrades.Add(new WalkableInfrastructure());
        m_Upgrades.Add(new PublicTransportInfrastructure());
        m_Upgrades.Add(new GreenPublicTransport());
    }
}
