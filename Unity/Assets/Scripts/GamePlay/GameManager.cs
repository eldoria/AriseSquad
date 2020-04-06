using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static bool gameOver;
    public GameObject gameOverUI;
    public GameObject player;


    private void Start()
    {
        gameOver = false;
    }

    public void EndGame()
    {
        Destroy(player);
        gameOver = true;
        gameOverUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
