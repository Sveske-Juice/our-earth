using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    [SerializeField, Tooltip("The name of scene that will be loaded on click")] private string m_GameScene = "Game";

    public void Play()
    {
        SceneManager.LoadScene(m_GameScene);
    }
}
