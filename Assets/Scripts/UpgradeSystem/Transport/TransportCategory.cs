public class TransportCategory : UpgradeCategory
{
    public override string CategoryName => "Transport";
    public TransportCategory()
    {
        GenerateUpgrades();
    }

    protected override void GenerateUpgrades()
    {
        m_Upgrades.Add(new ElectricCars());
        m_Upgrades.Add(new WalkableInfrastructure());
        m_Upgrades.Add(new PublicTransportInfrastructure());
    }

}
