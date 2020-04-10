using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reanimationMonstre : MonoBehaviour
{

    public GameObject menu;
    public Text textNbMonstres;
    private static int nbMonstresReanimables;
    public GameObject monstreReanime;
    public GameObject player;
    private playerScript script1;
    private cameraController script2;
    private GameObject scripts;

    private int count = 0;
    void Start()
    {
        nbMonstresReanimables = 10;
        menu.SetActive(false);
        updateText();
        script1 = player.GetComponent<playerScript>();
        script2 = player.GetComponentInChildren<cameraController>();
    }

    public static void UpdateNbMonstre()
    {
        nbMonstresReanimables++;
    }

    private void updateText()
    {
        textNbMonstres.text = "nombre de monstres réanimables : " + nbMonstresReanimables;
    }
    void Update()
    {
        if(Input.GetKeyDown("tab")){
            menu.SetActive(!menu.activeSelf);
            if (menu.activeSelf is true)
            {
                updateText();
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
        if (nbMonstresReanimables > 0)
        {
            Vector3 position = new Vector3(x: player.transform.position.x + Random.Range(-20,20), 0, z: player.transform.position.z + Random.Range(-20,-35));
            GameObject wolfReanim = Instantiate(monstreReanime, position, monstreReanime.transform.rotation);
            wolfReanim.GetComponent<wolfReanimated>().num = count++;
            GetComponent<monstersFight>().AddWolfAlly(wolfReanim);
            nbMonstresReanimables--;
        }
        
    }
}
