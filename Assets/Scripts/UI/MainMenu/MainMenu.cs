using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] RectTransform fader;

    public void Start()
    {
        fader.gameObject.SetActive(true);

        LeanTween.scale(fader, new Vector3(1, 1, 1), 1);
        LeanTween.scale(fader, Vector3.zero, 1).setOnComplete(() => {
            fader.gameObject.SetActive(false);
        });
    }
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }
    public void StartGame()
    {
        fader.gameObject.SetActive(true);

        LeanTween.scale(fader, Vector3.zero, 1);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 1).setOnComplete(() => {
            SceneManager.LoadScene("Game");
        });
    }
}
