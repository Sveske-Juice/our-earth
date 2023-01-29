public class FinanceCategory : UpgradeCategory
{
    public override string CategoryName => "Finance";
    public FinanceCategory()
    {
        GenerateUpgrades();
    }

    protected override void GenerateUpgrades()
    {
        m_Upgrades.Add(new IncreaseGoodsProduction());
        m_Upgrades.Add(new IncreaseImportExport());
        m_Upgrades.Add(new IncreaseOutsourcing());
        m_Upgrades.Add(new DecreaseGreenSubsidies());
    }
}
