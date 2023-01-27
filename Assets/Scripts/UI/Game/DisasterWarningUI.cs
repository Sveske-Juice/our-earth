using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DisasterWarningUI : MonoBehaviour
{
    [SerializeField, Tooltip("Text component that will hold the text display")] private TextMeshProUGUI m_CatastropheText;
    [SerializeField, TextArea, Tooltip("Text that will be displayed around the disaster that happens. ^ determines where the disaster will be placed")]
    private string m_CatastropheTextContext = "^ is approaching!";

    [SerializeField, Tooltip("How long the warning will be shown in the middle of the screen")]
    private float m_MiddleScreenTime = 2.5f;

    [SerializeField, Tooltip("How long the warning will be shown in the corner of the screen")]
    private float m_CornerScreenTime = 7.5f;
    private Vector3 big = new Vector3(1, 1, 1);
    private Vector3 small = new Vector3(0.4f, 0.4f, 1);
    private Vector3 gone = new Vector3(0, 0, 1);
    private Vector3 pos = new Vector3(340, -260, 0);
    private Vector3 originalPosition;

    float time = 1;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void OnEnable()
    {
        CatastropheManager.OnCatastropheStart += ShowCatastropheWarning;
    }

    private void OnDisable()
    {
        CatastropheManager.OnCatastropheStart -= ShowCatastropheWarning;
    }

    private void ShowCatastropheWarning(string catastrophe)
    {
        if (m_CatastropheText == null)
            return;

        // Set catastrophe text to reflect what the disaster is
        string display = m_CatastropheTextContext;
        int formatIdx = display.IndexOf('^');
        display = display.Remove(formatIdx, formatIdx + 1); // remove ^
        display = display.Insert(formatIdx, catastrophe); // Insert catastrophe name at ^

        m_CatastropheText.text = display;

        //Scale in
        LeanTween.scale(gameObject, big, time).setOnComplete(() => {
            // Move to corner after delay
            Invoke("MoveUI", m_MiddleScreenTime);
        });
    }

    //Brug denne function n�r du skal fjerne teksten (f�rst n�r disasteren er sket)
    public void Reset()
    {
        // Scale down animation
        LeanTween.scale(gameObject, gone, time * 2).setOnComplete(() => {
            // Move to original position
            transform.position = originalPosition;
        });
    }

    // Move to corner
    public void MoveUI()
    {
        LeanTween.moveLocal(gameObject, pos, 1f);
        LeanTween.scale(gameObject, small, 1f);

        // After moved to corner wait delay and remove text
        Invoke("Reset", m_CornerScreenTime);
    }

}
