using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade Container", menuName = "UpgradeSystem/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public string UpgradeName;
    public double BasePrice;
    public float UpgradeScaling = 1.25f;
    public double BaseEmissionInfluence = 0d;
    public double BaseBudgetInfluence = 0d;
    public int MaxUpgradeLevel = 10;
    public bool SpecialEffectUpgrade = false;
    public RequiredUpgradeData[] RequiredUpgrades;

    [TextArea()]
    public string UpgradeExplanation;
}
