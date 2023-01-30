using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager s_Instance;
    private static Controls m_PlayerControls;

    public static Controls PlayerControls => m_PlayerControls;

    private void Awake()
    {
        if (s_Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        s_Instance = this;

        m_PlayerControls = new Controls();
    }
}
