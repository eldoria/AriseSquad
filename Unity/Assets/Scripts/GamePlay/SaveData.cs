using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveData : MonoBehaviour
{
    private Transform player;
    private HealthBar healthBar;

    private string saveSeparator = "%VALUE%";

    public GameObject wolfEnemy;
    public GameObject wolfBoss;

    private void Start()
    {
        player = GameObject.Find("Joueur").transform;
        healthBar = GameObject.Find("Canvas").GetComponentInChildren<HealthBar>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W)) Save();
        else if(Input.GetKeyDown(KeyCode.X)) Load();
    }

    void Save()
    {
        string[] dataPlayer = new string[]
        {
            player.position.x.ToString(),
            player.position.y.ToString(),
            player.position.z.ToString(),
            
            player.rotation.x.ToString(),
            player.rotation.y.ToString(),
            player.rotation.z.ToString(),
            // vie du perso
            GameObject.Find("Canvas").GetComponentInChildren<HealthBar>().slider.value.ToString()
        };
        string stringDataPlayer = string.Join(saveSeparator, dataPlayer);
        File.WriteAllText(Application.dataPath + "/dataPlayer.txt", stringDataPlayer);

        string[] dataEnemies = new string[GetComponent<monstersFight>().indE * 9 + 1];
        int cpt = 0;

        dataEnemies[cpt++] = GetComponent<monstersFight>().indE.ToString();

        for (int i = 0; i < GetComponent<monstersFight>().indE; i++)
        {
            if (GetComponent<monstersFight>().Enemies[i])
            {
                dataEnemies[cpt++] = GetComponent<monstersFight>().Enemies[i].tag;
                
                if (GetComponent<monstersFight>().Enemies[i].CompareTag("wolfEnemy"))
                {
                    dataEnemies[cpt++] = GetComponent<monstersFight>().Enemies[i].GetComponent<wolfScript>().num.ToString();
                    dataEnemies[cpt++] = GetComponent<monstersFight>().Enemies[i].GetComponent<wolfScript>().hitPoints.ToString();
                }
                else if (GetComponent<monstersFight>().Enemies[i].CompareTag("wolfBoss")){
                    dataEnemies[cpt++] = GetComponent<monstersFight>().Enemies[i].GetComponent<bossScript>().num.ToString();
                    dataEnemies[cpt++] = GetComponent<monstersFight>().Enemies[i].GetComponent<bossScript>().hitPoints.ToString();
                }

                dataEnemies[cpt++] = GetComponent<monstersFight>().Enemies[i].transform.position.x.ToString();
                dataEnemies[cpt++] = GetComponent<monstersFight>().Enemies[i].transform.position.y.ToString();
                dataEnemies[cpt++] = GetComponent<monstersFight>().Enemies[i].transform.position.z.ToString();

                dataEnemies[cpt++] = GetComponent<monstersFight>().Enemies[i].transform.rotation.x.ToString();
                dataEnemies[cpt++] = GetComponent<monstersFight>().Enemies[i].transform.rotation.y.ToString();
                dataEnemies[cpt++] = GetComponent<monstersFight>().Enemies[i].transform.rotation.z.ToString();
            }
        }

        string stringDataEnemies = string.Join(saveSeparator, dataEnemies);
        File.WriteAllText(Application.dataPath + "/dataEnemies.txt", stringDataEnemies);
        
        Debug.Log("Sauvegarde effectuée");
    }

    void Load()
    {
        string saveStringPlayer = File.ReadAllText(Application.dataPath + "/dataPlayer.txt");
        string[] dataPlayer = saveStringPlayer.Split(new[] {saveSeparator}, System.StringSplitOptions.None);

        player.position = new Vector3(float.Parse(dataPlayer[0]), float.Parse(dataPlayer[1]), float.Parse(dataPlayer[2]));
        player.rotation = Quaternion.Euler(float.Parse(dataPlayer[3]), float.Parse(dataPlayer[4]), float.Parse(dataPlayer[5]));
        int healthSave = int.Parse(dataPlayer[6]);
        healthBar.setHealth(healthSave);
        GameObject.Find("Joueur").GetComponent<PlayerHealth>().currentHealth = healthSave;

        string saveStringEnemies = File.ReadAllText(Application.dataPath + "/dataEnemies.txt");
        string[] dataEnemies = saveStringEnemies.Split(new[] {saveSeparator}, System.StringSplitOptions.None);
        
        for (int i = 0; i < float.Parse(dataEnemies[0]); i++)
        {
            Destroy(GetComponent<monstersFight>().Enemies[i]);
            GetComponent<monstersFight>().DeleteEnemy(i);
        }

        for (int i = 0; i < dataEnemies.Length - 1; i++)
        {
            if (dataEnemies[9 * i + 1] == "wolfEnemy")
            {
                Vector3 position = new Vector3(float.Parse(dataEnemies[9 * i + 4]), float.Parse(dataEnemies[9 * i + 5]), float.Parse(dataEnemies[9 * i + 6]));
                Vector3 rotation = new Vector3(float.Parse(dataEnemies[9 * i + 7]), float.Parse(dataEnemies[9 * i + 8]), float.Parse(dataEnemies[9 * i + 9]));

                GameObject obj = Instantiate(wolfEnemy, position, Quaternion.Euler(rotation));
                Debug.Log(dataEnemies[9 * i + 1]);

                int num = int.Parse(dataEnemies[9 * i + 2]);
                obj.GetComponent<wolfScript>().num = num;
                obj.GetComponent<wolfScript>().hitPoints = int.Parse(dataEnemies[9 * i + 3]);
                
                GetComponent<monstersFight>().AddEnemy(num, obj);
            }
            else if (dataEnemies[9 * i + 1] == "wolfBoss")
            {
                Vector3 position = new Vector3(float.Parse(dataEnemies[9 * i + 4]), float.Parse(dataEnemies[9 * i + 5]), float.Parse(dataEnemies[9 * i + 6]));
                Vector3 rotation = new Vector3(float.Parse(dataEnemies[9 * i + 7]), float.Parse(dataEnemies[9 * i + 8]), float.Parse(dataEnemies[9 * i + 9]));

                GameObject obj = Instantiate(wolfBoss, position, Quaternion.Euler(rotation));
                
                int num = int.Parse(dataEnemies[9 * i + 2]);
                obj.GetComponent<bossScript>().num = num;
                obj.GetComponent<bossScript>().hitPoints = int.Parse(dataEnemies[9 * i + 3]);
                
                GetComponent<monstersFight>().AddEnemy(num, obj);
            }
        }

        Debug.Log("Chargement effectué");
    }
}
