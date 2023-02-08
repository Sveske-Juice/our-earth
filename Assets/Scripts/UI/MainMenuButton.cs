using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public void Go2MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
