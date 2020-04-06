using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    private void OnEnable() {
    
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
    }

    public void Retry()
    {
 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void TitleScreen() {
        SceneManager.LoadScene("MenuScene");
    }
}
