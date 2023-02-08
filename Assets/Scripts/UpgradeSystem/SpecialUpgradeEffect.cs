using UnityEngine;

[System.Serializable]
public class SpecialUpgradeEffect
{
    [SerializeField] private string m_UpgradeInfluenced;
    [SerializeField] private UpgradeModifier m_SpecialEffect;

    public string UpgradeInfluenced => m_UpgradeInfluenced;
    public UpgradeModifier SpecialEffect => m_SpecialEffect;

    public override string ToString()
    {
        return m_SpecialEffect.ToString();
    }
}
