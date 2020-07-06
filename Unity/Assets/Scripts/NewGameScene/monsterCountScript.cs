using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class monsterCountScript : MonoBehaviour
{
    public Text monsterCountText;
    public Scrollbar monsterCountScrollbar;
    private float monsterCountValue = 0.15f;
    
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
        monsterCountValue = 0.1f + (0.2f * monsterCountScrollbar.value);
        UpdateText();
    }

    private void UpdateText()
    {
        if (monsterCountValue <= 0.105f)
        {
            monsterCountText.text = "Facile";
        }
        else if (monsterCountValue <= 0.1505f)
        {
            monsterCountText.text = "Normal";
        }
        else if(monsterCountValue <= 0.205f)
        {
            monsterCountText.text = "Difficile";
        }
        else if (monsterCountValue <= 0.2505f)
        {
            monsterCountText.text = "Monstrueux";
        }
        else
        {
            monsterCountText.text = "Ariser";
        }
    }
}
