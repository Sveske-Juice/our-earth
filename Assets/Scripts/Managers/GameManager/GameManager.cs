using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
<summary>
Singleton Manager for managing the game and it's state.
</summary>
**/
public class GameManager : MonoBehaviour
{
    private static GameManager m_Instance;
    private GameState m_CurrentState;
    
    public static GameManager Instance => m_Instance;
    public GameState CurrentState => m_CurrentState;
    

    private void Awake()
    {
        // Check if another instance already exists
        if (m_Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Else this is the first instance
        m_Instance = this;
    }

    private void OnEnable()
    {
        // Subscribe to scene switch event
        SceneManager.sceneLoaded += OnSceneSwitched;
    }

    private void OnDisable()
    {
        // Unsubscribe to scene switch event
        SceneManager.sceneLoaded -= OnSceneSwitched;
    }

    private void OnSceneSwitched(Scene newScene, LoadSceneMode mode)
    {
        /* Find a new game state for the new scene. */
        GameStateType gameStateType;

        // If there's no game state type for the new scene name then just switch to no state; exits current state
        if (!Enum.TryParse(newScene.name, true, out gameStateType))
        {
            Debug.LogWarning($"Switching to scene with no corresponding game state type, for the scene name: {newScene.name}");
            SwitchState(null);
            return;
        }
        
        // Create the game state instance based on the game state type
        GameState newState;
        try
        {
            newState = GameState.Create(gameStateType);
        }
        catch (ArgumentException e)
        {
            Debug.LogError($"Could not create game state behaviour instance from enum type: {gameStateType}, error: {e.Message}");
            return;
        }

        // If it succesfully translated the scene name to it's corresponding game state, then switch to that state
        SwitchState(newState);
    }

    private void SwitchState(GameState newState)
    {
        m_CurrentState?.Exit();

        if (newState == null)
            return;
        
        m_CurrentState = newState;
        m_CurrentState.Enter();
    }
}
