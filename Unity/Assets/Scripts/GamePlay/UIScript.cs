using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Text Nom;
    public Text Score;


    public void AddScore()
    {
        leadboard.AddNewHighScores(Nom.GetComponent<Text>().text,int.Parse(Score.GetComponent<Text>().text));
    }

    public void LoadHighScore()
    {
        //Application.LoadLevel("Highscore");
        //Nous c'est sur le site
    }
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
