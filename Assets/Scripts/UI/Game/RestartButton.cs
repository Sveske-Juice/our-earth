using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    [SerializeField] RectTransform fader;
    public void RestartGame()
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 1);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 1).setOnComplete(() => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }
}
