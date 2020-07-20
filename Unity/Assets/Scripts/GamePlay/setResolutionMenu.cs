using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class setResolutionMenu : MonoBehaviour
{
    
    [SerializeField] private Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    
    
    // Start is called before the first frame update
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
        string path = Directory.GetCurrentDirectory() + @"\Assets\dataResolution.txt";
        if (File.Exists(path))
            resolutionDropdown.value = int.Parse(File.ReadAllText(Application.dataPath + "/dataResolution.txt"));
        else resolutionDropdown.value = currentResolutionIndex;

        resolutionDropdown.RefreshShownValue();
    }

    public void SetFullScreen(bool isFullEcran)
    {
        Screen.fullScreen = isFullEcran;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
        File.WriteAllText(Application.dataPath + "/dataResolution.txt", resolutionIndex.ToString());
    }
}
