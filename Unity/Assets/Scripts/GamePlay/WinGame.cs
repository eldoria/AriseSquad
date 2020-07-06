using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinGame : MonoBehaviour
{
    public GameObject endGamePanel;
    public Text text;

    private void OnEnable() {
    
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Retry()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TitleScreen() {
        Time.timeScale = 1; 
        SceneManager.LoadScene("MenuScene");
    }

    public void endGame()
    {
        endGamePanel.SetActive(true);
        string str = GameObject.Find("Scripts_Map_boss").GetComponent<timer>().getTimeSinceStart().ToString();
        text.text = string.Format("{0} {1}", str, " secondes");
            
        GameObject.Find("Joueur").GetComponent<playerScript>().enabled = false;
        GameObject.Find("Joueur").GetComponentInChildren<cameraController>().enabled = false;
        GameObject.Find("Scripts_Map_boss").GetComponent<reanimationMonstre>().enabled = false;
    }
}