using UnityEngine;

public class HouseholdsCategory : UpgradeCategory
{
    private UpgradeCategoryData m_ConcreteCategoryData;
    protected override UpgradeCategoryData m_CategoryData => m_ConcreteCategoryData;
    public HouseholdsCategory() : base()
    {
        m_ConcreteCategoryData = LoadCategoryData("UpgradeSystem/Categories/Households/HouseholdsCategory");
    }

    protected override void GenerateUpgrades()
    {
        m_Upgrades.Add(new WFHSubsidies());
        m_Upgrades.Add(new GreenerAppliances());
    }
}
