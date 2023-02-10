using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class YearSkipButton : MonoBehaviour
{
    public static event Action OnYearSkipClicked;

    public void OnSkipButtonClick()
    {
        OnYearSkipClicked?.Invoke();
    }
}
