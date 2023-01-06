using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PopoutAnim : MonoBehaviour
{
    public bool isOut = false;
    float outPos = -70;
    float backPos = -144;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isOut)
                PopOut(); 
            else
                PopBack();
        }
    }

    //Onto screen
    public void PopOut() {
        LeanTween.moveX(this.gameObject, outPos, 0.3f);
        isOut = true;
    }

    //Off screen
    public void PopBack()
    {
        LeanTween.moveX(this.gameObject, backPos, 0.3f);
        isOut = false;
    }
}
