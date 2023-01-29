using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenerConstruction : Upgrade
{
    public override string UpgradeName => "Greener Construction";
    protected override double m_StartPrice => 5000000000000d; // 5T
    protected override double m_StartEmissionInfluence => -25000000d; // -25M
}
