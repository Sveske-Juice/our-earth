using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PopoutAnim : MonoBehaviour
{
    private bool m_IsOut = false;

    public bool IsOut => m_IsOut;
    private float m_Width;
    private RectTransform m_RectTransform;

    private void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_Width = m_RectTransform.rect.width*2f;
    }

    //Onto screen
    public void PopOut() {
        if (m_IsOut)
            return;

        LeanTween.moveX(this.gameObject, 0f, 0.2f);
        m_IsOut = true;
    }

    //Off screen
    public void PopBack()
    {
        if (!m_IsOut)
            return;
        
        LeanTween.moveX(this.gameObject, -m_Width, 0.2f);
        m_IsOut = false;
    }
}
