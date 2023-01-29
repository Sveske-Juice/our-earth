using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinanceCategory : UpgradeCategory
{
    public override string CategoryName => "Finance";
    public FinanceCategory()
    {
        GenerateUpgrades();
    }

    protected override void GenerateUpgrades()
    {
    }
}
