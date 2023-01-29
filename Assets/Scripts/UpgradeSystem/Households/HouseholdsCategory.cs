using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseholdsCategory : UpgradeCategory
{
    public override string CategoryName => "Households";
    public HouseholdsCategory()
    {
        GenerateUpgrades();
    }

    protected override void GenerateUpgrades()
    {
        m_Upgrades.Add(new WFHSubsidies());
        m_Upgrades.Add(new GreenerAppliances());
    }
}
