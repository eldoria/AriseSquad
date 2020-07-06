using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < leadboard.lenght; i++)
        {
            GetComponent<TextMesh>().text += leadboard.GetPosition(i) + "\n";
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("Fire1"))
        {
            Application.LoadLevel("game");

        }
    }
}
