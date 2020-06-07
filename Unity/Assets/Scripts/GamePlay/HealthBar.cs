using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Text healthPoint;

    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        healthPoint.text = health.ToString();
    }

    public void setHealth(int health)
    {
        slider.value = health;
        healthPoint.text = health.ToString();
    }
    
}
