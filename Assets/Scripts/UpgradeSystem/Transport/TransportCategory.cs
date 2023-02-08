using UnityEngine;

public class TransportCategory : UpgradeCategory
{
    private UpgradeCategoryData m_ConcreteCategoryData;
    protected override UpgradeCategoryData m_CategoryData => m_ConcreteCategoryData;
    public TransportCategory() : base()
    {
        m_ConcreteCategoryData = LoadCategoryData("UpgradeSystem/Categories/Transport/TransportCategory");
    }

    protected override void GenerateUpgrades()
    {
        m_Upgrades.Add(new ElectricCars());
        m_Upgrades.Add(new WalkableInfrastructure());
        m_Upgrades.Add(new PublicTransportInfrastructure());
        m_Upgrades.Add(new GreenPublicTransport());
    }
}
