using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageMenu : MonoBehaviour
{
    private string language = "Français";

    public Text textLanguage;

    public Text button1;
    public Text button2;
    public Text button3;
    public Text button4;
    public Text button5;
    public Text button6;
    public Text button7;
    public Text button8;

    private void Awake()
    {
        ChangeText();
    }

    private void ChangeText()
    {
        Debug.Log("Langue choisi : " + textLanguage.text);
        if (language == "Français")     
        {
            button1.text = "Jouer";
            button2.text = "Options";
            button3.text = "Quitter";
            button4.text = "Charger";
            button5.text = "Les touches";
            button6.text = "Classement";
            button7.text = "Plein écran";
            button8.text = "Retour";
        }
        else if (language == "English")
        {
            button1.text = "Play";
            button2.text = "Options";
            button3.text = "Quit";
            button4.text = "Load";
            button5.text = "The keys";
            button6.text = "Leaderboard";
            button7.text = "Full screen";
            button8.text = "Back";
        }
    }

    private void ChooseEnglish()
    {
        if (language != "English")
        {
            language = "English";
            ChangeText();
        }
    }

    private void ChooseFrench()
    {
        if (language != "Français")
        {
            language = "Français";
            ChangeText();
        }
    }

    public void ChangeLanguage()
    {
        if (textLanguage.text == "Français") ChooseFrench();
        else if (textLanguage.text == "English") ChooseEnglish();
    }
}