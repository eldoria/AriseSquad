using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventaire_Script : MonoBehaviour
{
    PlayerHealth PlayerHP;
    [SerializeField] private GameObject inventaire_UI;
    [SerializeField] private bool isOpen;
    public List<Item> items = new List<Item>();
    public int space = 9;
    private cameraController scriptCamera;

    private void Start()
    {
        scriptCamera = GameObject.Find("Joueur").GetComponentInChildren<cameraController>();
    }

    #region Singleton
    
    public static Inventaire_Script instance;
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("more than one instance of inventory found");
            return;
        }
        instance = this;
    }
    
    #endregion

    public delegate void OnItemChanged();

    public OnItemChanged onItemChangedCallBack;
    
    public bool add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log("pas assez d'espace");
                return false;
            }
            items.Add(item);
            if (onItemChangedCallBack != null)
            {
                onItemChangedCallBack.Invoke();
            }
            
        }

        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
        
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
        scriptCamera.enabled = false;
    }


    public void Inventaire_Close()
    {
        inventaire_UI.SetActive(false);
        isOpen = false;
        scriptCamera.enabled = true;
    }
    
    
    
}
