using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class hPDisplayScript : MonoBehaviour
{
    public Text hitPointsText;
    
    public void ChangeHitPoints(int hp)
    {
        hitPointsText.text = "PV : " + hp;
    }
}
