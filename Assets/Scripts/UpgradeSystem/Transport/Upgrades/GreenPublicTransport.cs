using System.Collections.Generic;

public class GreenPublicTransport : Upgrade
{
    public override string UpgradeName => "Green Public Transport";
    public override string UpgradeExplanation => "fill me up";
    protected override double m_BasePrice => NumberPrefixer.Parse("150T");
    protected override int m_MaxUpgradeLevel => 1;
    protected override bool m_SpecialEffectUpgrade => true;
    private List<SpecialUpgradeEffect> upgradeSpecialEffects = new List<SpecialUpgradeEffect> { new SpecialUpgradeEffect("Public Transport Infrastructure", new UpgradeModifier(NumberPrefixer.Parse("-30M"), 0d)) };
    protected override List<SpecialUpgradeEffect> m_UpgradeSpecialEffects => upgradeSpecialEffects;
    
}
