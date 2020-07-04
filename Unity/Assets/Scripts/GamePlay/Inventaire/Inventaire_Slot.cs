using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class Inventaire_Slot : MonoBehaviour
{
    public Image icon;
    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void clearSlot()
    {
        item = null;
        icon.sprite = null;


        icon.enabled = false;
    }
    
    
}
