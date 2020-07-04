using System;
using System.Collections;
using System.Collections.Generic;
using Unity.UNetWeaver;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Assertions.Must;


public class Inventaire_Slot : MonoBehaviour
{
    private PlayerHealth playerHealth;
    public Image icon;
    Item item;

    private void Start()
    {
        playerHealth = GameObject.Find("Joueur").GetComponent<PlayerHealth>();
    }
    
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

    public void UseItem()
    {
        if (item != null && playerHealth.currentHealth < playerHealth.maxHealth)
        {
            item.UsePotion();
            Inventaire_Script.instance.Remove(item);
            playerHealth.GetHeal(20);
        }
    }
}