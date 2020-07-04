using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nouveau Item", menuName = "Inventaire/Items")]



public class Item : ScriptableObject
{
    
    new public string name = "Nouveau Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;


    public virtual void UsePotion()
    {
        
        Debug.Log("hp + 20");
        
    }




}
