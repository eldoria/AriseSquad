using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    
    [SerializeField] private GameObject UIWin;

    private float CurrentTime;
    private float StartTime = 10f;

    public GameObject bossWolf;

    private void Start()
    {
        CurrentTime = StartTime;
    }


    // Update is called once per frame
    void Update()
    {
        if (!bossWolf)
        {
            AfficherTheWin();
            
            CurrentTime -= 1 * Time.deltaTime;

            if (CurrentTime <= 0)
            {
                CurrentTime = 0;
                OffWin();
                GetComponent<WinScreen>().enabled = false;
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
