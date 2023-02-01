public class HouseholdsCategory : UpgradeCategory
{
    public override string CategoryName => "Households";
    public HouseholdsCategory() : base()
    {}

    protected override void GenerateUpgrades()
    {
        m_Upgrades.Add(new WFHSubsidies());
        m_Upgrades.Add(new GreenerAppliances());
    }
}
