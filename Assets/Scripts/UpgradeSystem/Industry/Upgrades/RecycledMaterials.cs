using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycledMaterials : Upgrade
{
    public override string UpgradeName => "Recycled Materials";
    protected override double m_StartPrice => 5000000000000d; // 5T
    protected override double m_StartEmissionInfluence => -25000000d; // -25M
}
