using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricCars : Upgrade
{
    public override string UpgradeName => "Electric Cars";
    public override double GetYearlyBudgetInfluence()
    {
        return 100d;
    }
}