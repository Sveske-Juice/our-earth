using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    Scene scene;

    private void Awake()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void BackToMenu()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void ResetLevel()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

