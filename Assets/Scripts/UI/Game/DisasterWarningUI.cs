using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisasterWarningUI : MonoBehaviour
{

    Vector3 big = new Vector3(1, 1, 1);
    Vector3 small = new Vector3(0.4f, 0.4f, 1);
    Vector3 gone = new Vector3(0, 0, 1);
    Vector3 pos = new Vector3(340, -260, 0);

    float time = 1;


    void Awake()
    {
        //Scale in
        LeanTween.scale(gameObject, big, time).setOnComplete(() => {
            //Flyt ned i hjørnet efter 2,5 sekunder
            Invoke("MoveUI", 2.5f);
            
        }); ;
       
    }

    //Brug denne function når du skal fjerne teksten (først når disasteren er sket)
    public void ScaleOut()
    {
        //Scale ned
        LeanTween.scale(gameObject, gone, time * 2).setOnComplete(() => {
            Destroy(gameObject);
        });
    }

    //Flyt ned i hjørnet
    public void MoveUI()
    {
        LeanTween.moveLocal(gameObject, pos, 1f);
        LeanTween.scale(gameObject, small, 1f);
    }

}
