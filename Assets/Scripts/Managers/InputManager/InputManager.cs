using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager m_Instance;
    private static Controls m_PlayerControls;

    public static Controls PlayerControls => m_PlayerControls;

    private void Awake()
    {
        if (m_Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        m_Instance = this;

        m_PlayerControls = new Controls();
    }
}
