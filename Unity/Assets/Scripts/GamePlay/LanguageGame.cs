using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LanguageGame : MonoBehaviour
{
    public string language = "Français";

    [SerializeField] private Text textLanguage;

    [SerializeField] private Text button1;
    [SerializeField] private Text button2;
    [SerializeField] private Text button3;
    [SerializeField] private Text button4;
    [SerializeField] private Text button5;
    [SerializeField] private Text button6;
    [SerializeField] private Text button7;
    [SerializeField] private Text button8;
    [SerializeField] private Text button9;
    [SerializeField] private Text button10;
    
    [SerializeField] private Dropdown DrLanguage;

    private void Awake()
    {
        language = File.ReadAllText(Application.dataPath + "/dataLanguage.txt");
        ChangeText();
        if (language == "Français") DrLanguage.value = 0;
        else if (language == "English") DrLanguage.value = 1;
        //GetComponent<Dropdown>().value = language;
    }

    private void ChangeText()
    {
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
            
            button9.text = "Recussiter";

            button10.text = "Inventaire";
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

            button9.text = "Resurrect";

            button10.text = "Inventory";
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
        File.WriteAllText(Application.dataPath + "/dataLanguage.txt", textLanguage.text);
        if (textLanguage.text == "Français") ChooseFrench();
        else if (textLanguage.text == "English") ChooseEnglish();
    }
}