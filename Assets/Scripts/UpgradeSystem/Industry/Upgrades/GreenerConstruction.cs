using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenerConstruction : Upgrade
{
    public override string UpgradeName => "Greener Construction";
    protected override double m_BasePrice => 5_000_000_000_000d; // 5T
    protected override double m_BaseEmissionInfluence => -25_000_000d; // -25M
}
