using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageGame : MonoBehaviour
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
    public Text button9;
    public Text button10;

    private void Awake()
    {
        ChangeText();
    }

    private void ChangeText()
    {
        Debug.Log("Langue choisi : " + textLanguage.text);
        if (language == "Français")     
        {
            button1.text = "Continuer";
            button2.text = "Options";
            button3.text = "Sauvegarder";
            button4.text = "Charger";
            button5.text = "Recommencer";
            button6.text = "Quitter";
            
            button7.text = "Plein écran";
            button8.text = "Retour";

            //button9.text = "Nombre de monstres ressucitables";
            button10.text = "Recussiter";
        }
        else if (language == "English")
        {
            button1.text = "Resume";
            button2.text = "Options";
            button3.text = "Save";
            button4.text = "Load";
            button5.text = "Retry";
            button6.text = "Quit";
            
            button7.text = "Full screen";
            button8.text = "Back";
            
            //button9.text = "Number of resuscitable monsters";
            button10.text = "Resurrect";
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