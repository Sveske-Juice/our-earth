using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseOutsourcing : Upgrade
{
    public override string UpgradeName => "Increase Outsourcing";
    protected override double m_BasePrice => 5_000_000_000_000d; // 5T
    protected override double m_BaseEmissionInfluence => -25_000_000d; // -25M
}
