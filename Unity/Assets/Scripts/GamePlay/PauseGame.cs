using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;


public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenuUI;
    [SerializeField] private GameObject OptionMenuUI;
    [SerializeField] private bool isPaused;
    private bool isOption;
    public Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    public reanimationMonstre script;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        
        int currentResolutionIndex = 0;
        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].width == Screen.currentResolution.width)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenuUI.activeSelf is true || OptionMenuUI.activeSelf is true)
        {
            script.enabled = false;
        }
        else
        {
            script.enabled = true;
        }
        if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) {
            isPaused = !isPaused;
            {
                if (isPaused)
                {
                    ActivateMenuPaused();
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    DeactivateMenuPause();
                    if(OptionMenuUI.activeSelf is true) DeactivateMenuOption();
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
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

    void ActivateMenuOption()
    {
        Time.timeScale = 0;
        OptionMenuUI.SetActive(true);
        isOption = true;
    }
    
    public void DeactivateMenuOption()
    {
        Time.timeScale = 1;
        OptionMenuUI.SetActive(false);
        isOption = false;
    }


    public void SetFullScreen(bool isFullEcran)
    {
        Screen.fullScreen = isFullEcran;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
    }
    
    
    
    
    
}
