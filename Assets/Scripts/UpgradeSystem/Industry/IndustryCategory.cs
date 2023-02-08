using UnityEngine;

public class IndustryCategory : UpgradeCategory
{
    private UpgradeCategoryData m_ConcreteCategoryData;
    protected override UpgradeCategoryData m_CategoryData => m_ConcreteCategoryData;
    public IndustryCategory() : base()
    {
        m_ConcreteCategoryData = LoadCategoryData("UpgradeSystem/Categories/Industry/IndustryCategory");
    }

    protected override void GenerateUpgrades()
    {
        m_Upgrades.Add(new GreenerConstruction());
        m_Upgrades.Add(new RecycledMaterials());
    }
}

