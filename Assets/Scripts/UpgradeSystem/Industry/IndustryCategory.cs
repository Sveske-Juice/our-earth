using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndustryCategory : UpgradeCategory
{
    public override string CategoryName => "Industry";
    public IndustryCategory()
    {
        GenerateUpgrades();
    }

    protected override void GenerateUpgrades()
    {
    }
}

