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
    public GameObject monstreReanime;
    public GameObject player;
    private playerScript script1;
    private cameraController script2;
    private GameObject scripts;
    public GameObject nombreField;
    
    //private int count = 0;
    void Start()
    {
        nbMonstresReanimables = 10;
        menu.SetActive(false);
        UpdateText();
        script1 = player.GetComponent<playerScript>();
        script2 = player.GetComponentInChildren<cameraController>();
    }

    public void UpdateNbMonstre()
    {
        nbMonstresReanimables++;
        textNbMonstres.text = "Nombre de monstres ressucitables : " + nbMonstresReanimables;
    }

    public void UpdateText()
    {
        textNbMonstres.text = "Nombre de monstres ressucitables : " + nbMonstresReanimables;
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
                script1.enabled = false;
                script2.enabled = false;
                script1.stopMoving();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                script1.enabled = true;
                script2.enabled = true;
            }
        }

        
    }

    public void reanimation()
    {
        int nbLoups;
        int.TryParse(nombreField.GetComponent<Text>().text, out nbLoups);
        if (nbMonstresReanimables > 0 & nbLoups > 0)
        {
            script1.animationReanimation();
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
            script1.enabled = true;
            script2.enabled = true;
            menu.SetActive(false);
        }
    }
}
