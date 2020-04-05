using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    
    [SerializeField] private GameObject UIWin;

    private float CurrentTime = 0f;
    private float StartTime = 10f;


    private void Start()
    {
        CurrentTime = StartTime;
    }


    // Update is called once per frame
    void Update()
    {
        if (bossScript.hitPoints == 0)
        {
            AfficherTheWin();
            
            CurrentTime -= 1 * Time.deltaTime;

            if (CurrentTime <= 0)
            {
                CurrentTime = 0;
                OffWin();
            }

        }
    }
    
    void AfficherTheWin()
    {
        UIWin.SetActive(true);
    }


    void OffWin()
    {
        UIWin.SetActive(false);
    }
}
