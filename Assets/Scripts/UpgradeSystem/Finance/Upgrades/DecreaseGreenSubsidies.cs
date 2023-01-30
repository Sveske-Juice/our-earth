using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseGreenSubsidies : Upgrade
{
    public override string UpgradeName => "Decrease Green Subsidies";
    protected override double m_BasePrice => 5_000_000_000_000d; // 5T
    protected override double m_BaseEmissionInfluence => -25_000_000d; // -25M
}
