using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class reanimationMonstre : MonoBehaviour
{

    public GameObject menu;
    public Text textNbMonstres;
    public int nbMonstresReanimables;
    [SerializeField] private GameObject monstreReanime;
    [SerializeField] private GameObject player;
    
    [SerializeField] private playerScript scriptPlayer;
    [SerializeField] private cameraController scriptCamera;
    [SerializeField] private Inventaire_Script scriptInventaire;
    [SerializeField] private PauseGame scriptPause;
    
    [SerializeField] private GameObject nombreField;

    

    public Text language;
    
    //private int count = 0;
    void Start()
    {
        nbMonstresReanimables = 30;
        menu.SetActive(false);
        UpdateText();
    }

    public void UpdateNbMonstre()
    {
        nbMonstresReanimables++;
        UpdateText();
    }

    public void UpdateText()
    {
        if(language.text == "Français") textNbMonstres.text = "Nombre de monstres ressucitables : " + nbMonstresReanimables;
        else if (language.text == "English") textNbMonstres.text = "Number of resuscitable monsters : " + nbMonstresReanimables;
    }
    void Update()
    {

        if(Input.GetKeyDown("tab")){
            menu.SetActive(!menu.activeSelf);
            
            if (menu.activeSelf is true)
            {
                UpdateText();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                scriptCamera.enabled = false;
                scriptInventaire.enabled = false;
                scriptPause.enabled = false;
                scriptPlayer.enabled = false;
                scriptPlayer.stopMoving();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                scriptCamera.enabled = true;
                scriptInventaire.enabled = true;
                scriptPause.enabled = true;
                scriptPlayer.enabled = true;
            }
        }

        
    }

    public void reanimation()
    {
        int nbLoups;
        int.TryParse(nombreField.GetComponent<Text>().text, out nbLoups);
        if (nbMonstresReanimables > 0 & nbLoups > 0)
        {
            scriptPlayer.animationReanimation();
            if (nbLoups > nbMonstresReanimables) nbLoups = nbMonstresReanimables;
            for (int i = 0; i < nbLoups; i++)
            {
                Vector3 position = new Vector3(x: player.transform.position.x + Random.Range(-20,20), 0, z: player.transform.position.z + Random.Range(-20,-35));
                GameObject wolfReanim = Instantiate(monstreReanime, position, monstreReanime.transform.rotation);
                wolfReanim.GetComponent<wolfReanimated>().num = GetComponent<monstersFight>().GetCountAlly();
                GetComponent<monstersFight>().AddAlly(wolfReanim);
                nbMonstresReanimables--;
            }
            UpdateText();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            scriptCamera.enabled = true;
            scriptInventaire.enabled = true;
            scriptPause.enabled = true;
            scriptPlayer.enabled = true;
            menu.SetActive(false);
        }
    }
}
