using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenuUI;
    [SerializeField] private bool isPaused;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                ActivateMenuPaused();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                DeactivateMenuPause();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void ActivateMenuPaused()
    {
        Time.timeScale = 0;             //la vitesse du jeu est à la vitesse 1
        PauseMenuUI.SetActive(true);
    }

    public void DeactivateMenuPause()
    {
        Time.timeScale = 1;
        PauseMenuUI.SetActive(false);
        isPaused = false;
    }
}
