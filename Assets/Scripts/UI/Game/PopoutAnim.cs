using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PopoutAnim : MonoBehaviour
{
    public GameObject op, bp;
    public bool isOut = false;
    float outPos = -300;
    float backPos = -560;

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
        LeanTween.moveX(this.gameObject, op.transform.position.x, 0.3f);
        isOut = true;
    }

    //Off screen
    public void PopBack()
    {
        LeanTween.moveX(this.gameObject, bp.transform.position.x, 0.3f);
        isOut = false;
    }
}
