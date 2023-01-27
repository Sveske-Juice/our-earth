using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class ContinentEventInitiator : MonoBehaviour
{
    private Camera m_MainCamera;
    private Vector2 m_LastClickPosition;

    /// <summary> Event that gets raised when a continent is clicked on. The gameobject of the continent is passed in. </summary>
    public static event Action<GameObject> OnContinentClick;

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
    private void OnDisable()
    {
        InputManager.PlayerControls.Navigation.ScreenClick.performed -= OnScreenClick;
        InputManager.PlayerControls.Navigation.OnMove.performed -= UpdateClickPosition;
        InputManager.PlayerControls.Navigation.Disable();
    }

    private void OnScreenClick(InputAction.CallbackContext ctx)
    {
        CheckForContinentClick(m_LastClickPosition);
    }

    private void CheckForContinentClick(Vector2 clickPosition)
    {
        // Check to see if a continent was clicked on
        Ray ray = m_MainCamera.ScreenPointToRay(clickPosition);
        
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.blue, 5f);

        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, Mathf.Infinity))
            return;
        

        if (hit.collider.tag != "Continent")
            return;
        
        OnContinentClick?.Invoke(hit.collider.gameObject);
    }

    private void UpdateClickPosition(InputAction.CallbackContext ctx)
    {
        // Update the last click position when mouse/touchscreen moves
        m_LastClickPosition = ctx.ReadValue<Vector2>();
    }
}
