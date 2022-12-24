using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState
{
    /// <summary>
    /// Factory method for creating a new GameState instance based on a game state type.
    /// </summary>
    /// <param name="gameStateType">The Game State Type the new instance will be based on. </param>
    public static GameState Create(GameStateType gameStateType)
    {
        switch (gameStateType)
        {
            case GameStateType.Menu:
                return new MenuState();
            
            case GameStateType.Game:
                return new InGameState();

            default:
                throw new ArgumentException($"Invalid Game State Type passed to Game State Factory Method: {gameStateType.ToString()}");
        }
    }

    /// <summary>
    /// Gets called when the state this method
    /// belongs to gets activated.
    /// </summary>
    public virtual void Enter()
    {
        Debug.Log($"Entered new state: {GameManager.Instance.CurrentState}");
    }

    /// <summary>
    /// Gets called every frame while the state this 
    /// method belongs to is active.
    /// </summary>
    public virtual void Update()
    {

    }

    /// <summary>
    /// Gets called when the state this method
    /// belongs to gets deactivated.
    /// </summary>
    public virtual void Exit()
    {
        Debug.Log($"Exited state: {GameManager.Instance.CurrentState}");
    }
}
