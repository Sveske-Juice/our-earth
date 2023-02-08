using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Category Data", menuName = "UpgradeSystem/Category")]
public class UpgradeCategoryData : ScriptableObject
{
    public string CategoryName;
    public Texture2D CategoryIcon;
}
