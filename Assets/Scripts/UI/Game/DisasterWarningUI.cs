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

    [SerializeField, Tooltip("Transform of where the text should be when in the middle of the screen")]
    private Transform m_MiddlePosition;

    [SerializeField, Tooltip("Transform of where the text should be when in the corner of the screen")]
    private Transform m_CornerPosition;
    private Vector3 originalPosition;


    private Vector3 big = new Vector3(1, 1, 1);
    private Vector3 small = new Vector3(0.7f, 0.7f, 1);
    private Vector3 gone = new Vector3(0, 0, 1);

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

        // Scale in
        transform.position = m_MiddlePosition.position;
        LeanTween.scale(gameObject, big, time).setOnComplete(() => {
            // Move to corner after delay
            Invoke("MoveUI", m_MiddleScreenTime);
        });
    }

    // Resets the element
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
        LeanTween.move(gameObject, m_CornerPosition.position, 1f);
        LeanTween.scale(gameObject, small, 1f);

        // After moved to corner wait delay and remove text
        Invoke("Reset", m_CornerScreenTime);
    }

}
