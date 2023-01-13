using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ContinentHighlighter : MonoBehaviour
{
    private Camera m_MainCamera;
    private GameObject m_SelectedContinent;
    private Vector2 m_LastClickPosition;

    private void Awake()
    {
        m_MainCamera = Camera.main;
    }

    private void OnEnable()
    {
        InputManager.PlayerControls.Navigation.Enable();
        InputManager.PlayerControls.Navigation.ScreenClick.performed += OnScreenClick;
        InputManager.PlayerControls.Navigation.OnMove.performed += UpdateClickPosition;
    }

    private void OnScreenClick(InputAction.CallbackContext ctx)
    {
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
        
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.blue, 5f);

        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, Mathf.Infinity))
            return;
        
        if (hit.transform.tag != "Continent")
            return;

        // Highlight the clicked continent
        m_SelectedContinent = hit.transform.gameObject;

        

        Debug.Log($"hit {hit.transform.name}");
    }
}
