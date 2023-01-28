using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PopoutAnim : MonoBehaviour
{
    private bool m_IsOut = false;

    public bool IsOut => m_IsOut;

    [SerializeField]
    private Transform m_OutOfViewPosition;

    [SerializeField]
    private Transform m_InViewPosition;

    //Onto screen
    public void PopOut() {
        if (m_IsOut)
            return;

        LeanTween.moveX(this.gameObject, m_OutOfViewPosition.position.x, 0.2f);
        m_IsOut = true;
    }

    //Off screen
    public void PopBack()
    {
        if (!m_IsOut)
            return;
        
        LeanTween.moveX(this.gameObject, m_InViewPosition.position.x, 0.2f);
        m_IsOut = false;
    }
}
