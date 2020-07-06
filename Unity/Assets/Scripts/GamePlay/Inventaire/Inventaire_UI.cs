﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventaire_UI : MonoBehaviour
{
    public Transform item_parent;
    Inventaire_Slot[] slots;
    Inventaire_Script inventaireScript;
    
    // Start is called before the first frame update
    void Start()
    {
        inventaireScript = Inventaire_Script.instance;
        inventaireScript.onItemChangedCallBack += Update_UI;
        slots = item_parent.GetComponentsInChildren<Inventaire_Slot>();
    }
    
    void Update_UI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventaireScript.items.Count)
            {
                slots[i].AddItem(inventaireScript.items[i]);
            }
            else
            {
                slots[i].clearSlot();
            }
        }
    }

    public int CountItemInInventory()
    {
        return inventaireScript.items.Count;
    }

    public void SetNbItemInventory(int nb)
    {
        for (int i = 0; i < inventaireScript.items.Count + 1; i++)
        {
            Debug.Log(inventaireScript.items.Count);
            inventaireScript.Remove(inventaireScript.items[i]);
        }
        for (int i = 0; i < nb; i++)
        {
            Debug.Log("test");
            inventaireScript.add(inventaireScript.items[i]);
        }
    }
}
