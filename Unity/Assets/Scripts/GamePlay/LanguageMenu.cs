using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageMenu : MonoBehaviour
{
    private string language = "français";

    public Text button1;
    public Text button2;
    public Text button3;
    public Text button4;
    public Text button5;
    public Text button6;

    private void Awake()
    {
        ChangeText();
    }

    void ChangeText()
    {
        if (language == "français")
        {
            button1.text = "Jouer";
            button2.text = "Options";
            button3.text = "Quitter";
            button4.text = "Charger";
            button5.text = "Les touches";
            button6.text = "Classement";
        }
        else if (language == "english")
        {
            button1.text = "Play";
            button2.text = "Options";
            button3.text = "Quit";
            button4.text = "Load";
            button5.text = "The keys";
            button6.text = "Leaderboard";
        }
    }

    public void ChooseEnglish()
    {
        if (language != "english")
        {
            language = "english";
            ChangeText();
        }
    }

    public void ChooseFrench()
    {
        if (language != "français")
        {
            language = "français";
            ChangeText();
        }
    }
}