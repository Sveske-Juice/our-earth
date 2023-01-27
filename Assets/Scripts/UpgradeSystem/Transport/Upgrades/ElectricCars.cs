using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricCars : Upgrade
{
    public override double GetYearlyBudgetInfluence()
    {
        return 100d;
    }
}