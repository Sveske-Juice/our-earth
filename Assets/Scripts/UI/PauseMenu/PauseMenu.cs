using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    [SerializeField] RectTransform fader;
    Scene scene;

    private void Awake()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void BackToMenu()
    {
        FindObjectOfType<AudioManager>().Play("Button");

        fader.gameObject.SetActive(true);
        Time.timeScale = 1;
        LeanTween.scale(fader, Vector3.zero, 1);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 1).setOnComplete(() => {
            SceneManager.LoadScene("Menu");
        });
        
        
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
        fader.gameObject.SetActive(true);
        Time.timeScale = 1;
        LeanTween.scale(fader, Vector3.zero, 1);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 1).setOnComplete(() => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
        
    }
}

