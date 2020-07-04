using System.Collections;
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
}
