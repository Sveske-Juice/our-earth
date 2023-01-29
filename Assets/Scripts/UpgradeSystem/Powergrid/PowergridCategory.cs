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
    }
}
