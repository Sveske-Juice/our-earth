
public class SpecialUpgradeEffect
{
    private string m_UpgradeInfluenced;
    private UpgradeModifier m_SpecialEffect;

    public string UpgradeInfluenced => m_UpgradeInfluenced;
    public UpgradeModifier SpecialEffect => m_SpecialEffect;

    public SpecialUpgradeEffect(string upgradeInfluenced, UpgradeModifier specialEffect)
    {
        m_UpgradeInfluenced = upgradeInfluenced;
        m_SpecialEffect = specialEffect;
    }

    public override string ToString()
    {
        return m_SpecialEffect.ToString();
    }
}
