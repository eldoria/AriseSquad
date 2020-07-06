using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vegetationScript : MonoBehaviour
{
    public Text vegetationText;
    public Scrollbar vegetationScrollbar;
    private float vegetationValue = 0.15f;
    
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
        vegetationValue = 0.1f + (0.2f * vegetationScrollbar.value);
        UpdateText();
    }

    private void UpdateText()
    {
        if (vegetationValue <= 0.105f)
        {
            vegetationText.text = "Faible";
        }
        else if (vegetationValue <= 0.1505f)
        {
            vegetationText.text = "Normale";
        }
        else if(vegetationValue <= 0.205f)
        {
            vegetationText.text = "Forte";
        }
        else if (vegetationValue <= 0.2505f)
        {
            vegetationText.text = "Dense";
        }
        else
        {
            vegetationText.text = "Ariser";
        }
    }

}
