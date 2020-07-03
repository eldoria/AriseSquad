using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : Interactable
{
    public override void Interact()
    {
        base.Interact();
        PickUp();
    }


    void PickUp()
    {
        Debug.Log("vous avez ramassé un potion HP");
        Destroy(gameObject);
    }
    
        
    
    
    
    
    
    
    
    
    
    
}
