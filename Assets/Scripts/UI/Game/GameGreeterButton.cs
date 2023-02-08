using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGreeterButton : MonoBehaviour
{
    [SerializeField] private GameObject m_GreeterMenu;

    public void CloseGreeterMenu()
    {
        m_GreeterMenu.SetActive(false);
    }
}
