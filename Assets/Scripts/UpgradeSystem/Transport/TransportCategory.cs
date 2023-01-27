using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportCategory : UpgradeCategory
{
    public override string CategoryName => "Transport";
    public TransportCategory()
    {
        GenerateUpgrades();
    }

    protected override void GenerateUpgrades()
    {
        Debug.Log("Generating transport category");
        
        m_Upgrades.Add(new ElectricCars());
    }

}
