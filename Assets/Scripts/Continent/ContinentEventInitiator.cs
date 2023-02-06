using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class ContinentEventInitiator : MonoBehaviour
{
    private Camera m_MainCamera;
    private Vector2 m_LastClickPosition;
    private GameObject m_SelectedContinent;

    /// <summary> Event that gets raised when a continent is clicked on. The gameobject of the continent is passed in. </summary>
    public static event Action<GameObject> OnContinentSelect;

    /// <summary> Event that gets raised when a continent is de-selected on. The gameobject of the continent is passed in. </summary>
    public static event Action<GameObject> OnContinentDeselect;

    private void Awake()
    {
        m_MainCamera = Camera.main;
    }

    private void OnEnable()
    {
        InputManager.PlayerControls.Navigation.Enable();
        InputManager.PlayerControls.Navigation.ScreenClick.canceled += OnScreenClick;
        InputManager.PlayerControls.Navigation.OnMove.performed += UpdateClickPosition;
    }
    private void OnDisable()
    {
        InputManager.PlayerControls.Navigation.ScreenClick.canceled -= OnScreenClick;
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
        
        GameObject clickedContinent = hit.collider.gameObject;
        
        // Scenario where a the continent clicked is the same as the one already selected.
        // Here it should unselect the current selected continent
        if (m_SelectedContinent == clickedContinent)
        {
            OnContinentDeselect?.Invoke(m_SelectedContinent);
            m_SelectedContinent = null;
            return;
        }

        // Scenario where a new continent is clicked - here the current selected continent
        // should be deselected and the new clicked continent should be selected.
        if (m_SelectedContinent != null)
            OnContinentDeselect?.Invoke(m_SelectedContinent);

        m_SelectedContinent = clickedContinent;
        OnContinentSelect?.Invoke(clickedContinent);
    }

    private void UpdateClickPosition(InputAction.CallbackContext ctx)
    {
        // Update the last click position when mouse/touchscreen moves
        m_LastClickPosition = ctx.ReadValue<Vector2>();
    }
}
