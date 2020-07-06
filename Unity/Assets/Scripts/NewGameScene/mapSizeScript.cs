using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mapSizeScript : MonoBehaviour
{
    public Text mapSizeText;
    public Scrollbar mapSizeScrollbar;
    private int mapSizeValue = 1000;
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewValue()
    {
        mapSizeValue = (int) (500 + (mapSizeScrollbar.value * 2000));
        UpdateText();
    }

    private void UpdateText()
    {
        mapSizeText.text = mapSizeValue.ToString();
    }
}
