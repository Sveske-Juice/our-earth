using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatastropheWarningSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField, Tooltip("Transform of where the warning should appear.")]
    private Transform m_StartPosition;

    [SerializeField, Tooltip("Transform of where the text should be when in the corner of the screen")]
    private Transform m_CornerPosition;

    [SerializeField]
    private GameObject m_WarningDisplayPrefab;

    [Header("Optional References")]
    [SerializeField, Tooltip("The transform catastrophe warnings will be spawned under (their parent). Will default to transform this component is attached to.")]
    private Transform m_WarningParent;

    private void Awake()
    {
        if (m_WarningParent == null)
            m_WarningParent = transform; // Set default
    }

    private void OnEnable()
    {
        CatastropheManager.OnCatastropheStart += SpawnWarning;
    }

    private void OnDisable()
    {
        CatastropheManager.OnCatastropheStart -= SpawnWarning;
    }

    private void SpawnWarning(Catastrophe catastrophe, Upgrade upgrade2Downgrade)
    {
        GameObject catastropheWarningObj = Instantiate(m_WarningDisplayPrefab, m_StartPosition.position, Quaternion.identity, m_WarningParent);
        catastropheWarningObj.transform.localScale = Vector3.zero; // The warning should scale it up itself

        // Get the prefab behaviour class and pass the catastrophe and upgrade to be downgraded
        CatastropheWariningDisplay catastropheWariningDisplay = catastropheWarningObj.GetComponent<CatastropheWariningDisplay>();
        catastropheWariningDisplay.Catastrophe = catastrophe;
        catastropheWariningDisplay.Upgrade2Downgrade = upgrade2Downgrade;
        catastropheWariningDisplay.CornerPosition = m_CornerPosition;
    }
}
