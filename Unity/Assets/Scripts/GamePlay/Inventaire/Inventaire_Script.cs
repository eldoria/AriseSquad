using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventaire_Script : MonoBehaviour
{
    
    [SerializeField] private GameObject inventaire_UI;
    [SerializeField] private bool isOpen;
    
    
    // Start is called before the first frame update
    void Start()
    {
      
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isOpen = !isOpen;

            if (isOpen)
            {
                Inventaire_Open();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                
            }
            else
            {
                Inventaire_Close();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }


    void Inventaire_Open()
    {
        inventaire_UI.SetActive(true);
    }


    void Inventaire_Close()
    {
        inventaire_UI.SetActive(false);
        isOpen = false;
    }
    
}
