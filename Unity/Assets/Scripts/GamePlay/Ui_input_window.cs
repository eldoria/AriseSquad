using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_input_window : MonoBehaviour
{
   

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
