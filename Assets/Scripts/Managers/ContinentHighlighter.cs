using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ContinentHighlighter : MonoBehaviour
{
    private Controls m_Controls;
    private Camera m_MainCamera;

    private Vector2 m_LastClickPosition;

    private void Awake()
    {
        m_Controls = new Controls();
        m_MainCamera = Camera.main;
    }

    private void OnEnable()
    {
        m_Controls.Navigation.Enable();
        m_Controls.Navigation.ScreenClick.performed += OnScreenClick;
        m_Controls.Navigation.OnMove.performed += UpdateClickPosition;
    }

    private void OnScreenClick(InputAction.CallbackContext ctx)
    {
        print("Screen clicked");
        HighlightContinent(m_LastClickPosition);
    }

    private void UpdateClickPosition(InputAction.CallbackContext ctx)
    {
        // Update the last click position when mouse/touchscreen moves
        m_LastClickPosition = ctx.ReadValue<Vector2>();
    }

    private void HighlightContinent(Vector2 clickPosition)
    {
        // Check to see if a continent was clicked on
        Ray ray = m_MainCamera.ScreenPointToRay(clickPosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, Mathf.Infinity))
            return;
        
        
    }
}
