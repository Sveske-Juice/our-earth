public class PowergridCategory : UpgradeCategory
{
    public override string CategoryName => "Power Grid";
    public PowergridCategory() : base()
    {}

    protected override void GenerateUpgrades()
    {
        m_Upgrades.Add(new RenewableEnergySources());         
        m_Upgrades.Add(new NuclearEnergySubsidies());         
        m_Upgrades.Add(new FusionEnergyResearch());         
    }
}
