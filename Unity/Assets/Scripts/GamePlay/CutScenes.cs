using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScenes : MonoBehaviour
{

    [SerializeField] private Camera cameraForCutScene;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject Minimap;
    
    [SerializeField] private playerScript scriptPlayer;
    [SerializeField] private reanimationMonstre scriptReanimationMonstre;
    [SerializeField] private PauseGame scriptPauseGame;
    [SerializeField] private Inventaire_Script scriptInventaire;

    private void Start()
    {
        StartCoroutine(CutScene1());
    }

    IEnumerator CutScene1()
    {
        playerCamera.enabled = false;
        Minimap.SetActive(false);
        scriptPlayer.enabled = false;
        scriptReanimationMonstre.enabled = false;
        scriptPauseGame.enabled = false;
        scriptInventaire.enabled = false;
        
        yield return new WaitForSeconds(10);
        playerCamera.enabled = true;
        Minimap.SetActive(true);
        scriptPlayer.enabled = true;
        scriptReanimationMonstre.enabled = true;
        scriptPauseGame.enabled = true;
        scriptInventaire.enabled = true;
    }
}
