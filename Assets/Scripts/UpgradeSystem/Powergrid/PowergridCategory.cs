using UnityEngine;

public class PowergridCategory : UpgradeCategory
{
    private UpgradeCategoryData m_ConcreteCategoryData;
    protected override UpgradeCategoryData m_CategoryData => m_ConcreteCategoryData;
    public PowergridCategory() : base()
    {
        m_ConcreteCategoryData = LoadCategoryData("UpgradeSystem/Categories/Powergrid/PowergridCategory");
    }

    protected override void GenerateUpgrades()
    {
        m_Upgrades.Add(new RenewableEnergySources());         
        m_Upgrades.Add(new NuclearEnergySubsidies());         
        m_Upgrades.Add(new FusionEnergyResearch());         
    }
}
