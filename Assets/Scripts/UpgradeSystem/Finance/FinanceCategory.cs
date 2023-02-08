using UnityEngine;

public class FinanceCategory : UpgradeCategory
{
    private UpgradeCategoryData m_ConcreteCategoryData;
    protected override UpgradeCategoryData m_CategoryData => m_ConcreteCategoryData;
    public FinanceCategory() : base()
    {
        m_ConcreteCategoryData = LoadCategoryData("UpgradeSystem/Categories/Finance/FinanceCategory");
    }

    protected override void GenerateUpgrades()
    {
        m_Upgrades.Add(new IncreaseGoodsProduction());
        m_Upgrades.Add(new IncreaseImportExport());
        m_Upgrades.Add(new IncreaseOutsourcing());
        m_Upgrades.Add(new DecreaseGreenSubsidies());
    }
}
