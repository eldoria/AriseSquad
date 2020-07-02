using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveData : MonoBehaviour
{

    private Transform player;
    private healthPLayer
    
    private string saveSeparator = "%VALUE%";

    private void Start()
    {
        player = GameObject.Find("Joueur").transform;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W)) Save();
        else if(Input.GetKeyDown(KeyCode.X)) Load();
    }

    void Save()
    {
        string[] content = new string[]
        {
            player.position.x.ToString(),
            player.position.y.ToString(),
            player.position.z.ToString(),
            player.rotation.x.ToString(),
            player.rotation.y.ToString(),
            player.rotation.z.ToString()
        };
        
        string saveString = string.Join(saveSeparator, content);
        File.WriteAllText(Application.dataPath + "/data.txt", saveString);
        Debug.Log("Sauvegarde effectuée");
    }

    void Load()
    {
        string saveString = File.ReadAllText(Application.dataPath + "/data.txt");

        string[] content = saveString.Split(new[] {saveSeparator}, System.StringSplitOptions.None);

        player.position = new Vector3(float.Parse(content[0]), float.Parse(content[1]), float.Parse(content[2]));
        player.rotation = Quaternion.Euler(float.Parse(content[3]), float.Parse(content[4]), float.Parse(content[5]));
        
        Debug.Log("Chargement effectué");
    }
}
