using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class changeScene : MonoBehaviour
{
    public string sceneToLoad;

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
