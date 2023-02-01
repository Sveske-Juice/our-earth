public class IndustryCategory : UpgradeCategory
{
    public override string CategoryName => "Industry";
    public IndustryCategory() : base()
    {}

    protected override void GenerateUpgrades()
    {
        m_Upgrades.Add(new GreenerConstruction());
        m_Upgrades.Add(new RecycledMaterials());
    }
}

