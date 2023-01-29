using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowergridCategory : UpgradeCategory
{
    public override string CategoryName => "Power Grid";
    public PowergridCategory()
    {
        GenerateUpgrades();
    }

    protected override void GenerateUpgrades()
    {
        m_Upgrades.Add(new RenewableEnergySources());         
        m_Upgrades.Add(new NuclearEnergySubsidies());         
        m_Upgrades.Add(new FusionEnergyResearch());         
    }
}
