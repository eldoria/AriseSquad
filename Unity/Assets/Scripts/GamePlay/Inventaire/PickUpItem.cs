using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : Interactable
{
    public Item item;
    public override void Interact()
    {
        base.Interact();
        PickUp();
    }


    void PickUp()
    {
        //Debug.Log("vous avez ramassé un " + item.name);
        bool WasPickUp = Inventaire_Script.instance.add(item);

        if (WasPickUp)
        {
            Destroy(gameObject);
        }
        
    }
    
        
    
    
    
    
    
    
    
    
    
    
}
