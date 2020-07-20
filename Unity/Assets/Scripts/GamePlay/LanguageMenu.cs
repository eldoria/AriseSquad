using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LanguageMenu : MonoBehaviour
{
    private string language = "Français";

    public Text textLanguage;

    [SerializeField] private Text button1;
    [SerializeField] private Text button2;
    [SerializeField] private Text button3;
    [SerializeField] private Text button4;
    [SerializeField] private Text button5;
    [SerializeField] private Text button6;
    [SerializeField] private Text button7;
    [SerializeField] private Text button8;

    [SerializeField] private Dropdown DrLanguage;
    

    private void Awake()
    {
        language = File.ReadAllText(Application.dataPath + "/dataLanguage.txt");
        ChangeText();
        if (language == "Français") DrLanguage.value = 0;
        else if (language == "English") DrLanguage.value = 1;
    }

    private void ChangeText()
    {
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
        language = "English";
        ChangeText();
    }

    private void ChooseFrench()
    {
        language = "Français";
        ChangeText();
    }

    public void ChangeLanguage()
    {
        File.WriteAllText(Application.dataPath + "/dataLanguage.txt", textLanguage.text);
        if (textLanguage.text == "Français") ChooseFrench();
        else if (textLanguage.text == "English") ChooseEnglish();
    }
}