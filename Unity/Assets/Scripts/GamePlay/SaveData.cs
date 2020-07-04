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
    public GameObject wolfAlly;
    public GameObject wolfBoss;
    public GameObject potion;

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

    public void Save()
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

        string[] dataEnemies = new string[GetComponent<monstersFight>().GetCountEnemy() * 9];
        int cpt = 0;

        for (int i = 0; i < GetComponent<monstersFight>().GetCountEnemy(); i++)
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
        
        
        string[] dataAllies = new string[(GetComponent<monstersFight>().GetCountAlly() - 1) * 9 + 1];
        cpt = 0;

        for (int i = 0; i < GetComponent<monstersFight>().GetCountAlly(); i++)
        {
            if (GetComponent<monstersFight>().Allies[i] && GetComponent<monstersFight>().Allies[i].CompareTag("wolfAlly"))
            {
                dataAllies[cpt++] = GetComponent<monstersFight>().Allies[i].tag;
                dataAllies[cpt++] = GetComponent<monstersFight>().Allies[i].GetComponent<wolfReanimated>().num.ToString();
                dataAllies[cpt++] = GetComponent<monstersFight>().Allies[i].GetComponent<wolfReanimated>().hitPoints.ToString();

                dataAllies[cpt++] = GetComponent<monstersFight>().Allies[i].transform.position.x.ToString();
                dataAllies[cpt++] = GetComponent<monstersFight>().Allies[i].transform.position.y.ToString();
                dataAllies[cpt++] = GetComponent<monstersFight>().Allies[i].transform.position.z.ToString();

                dataAllies[cpt++] = GetComponent<monstersFight>().Allies[i].transform.rotation.x.ToString();
                dataAllies[cpt++] = GetComponent<monstersFight>().Allies[i].transform.rotation.y.ToString();
                dataAllies[cpt++] = GetComponent<monstersFight>().Allies[i].transform.rotation.z.ToString();
            }
        }

        dataAllies[cpt++] = GetComponent<reanimationMonstre>().nbMonstresReanimables.ToString();

        string stringDataAllies = string.Join(saveSeparator, dataAllies);
        File.WriteAllText(Application.dataPath + "/dataAllies.txt", stringDataAllies);

        GameObject potion = GameObject.Find("Potions");

        string[] dataPotions = new string[potion.transform.childCount * 3];// +1 for the number of potions in the inventory
        cpt = 0;

        foreach (Transform child in potion.transform)
        {
            dataPotions[cpt++] = child.transform.position.x.ToString();
            dataPotions[cpt++] = child.transform.position.y.ToString();
            dataPotions[cpt++] = child.transform.position.z.ToString();
        }

        string stringDataPotions = string.Join(saveSeparator, dataPotions);
        File.WriteAllText(Application.dataPath + "/dataPotions.txt", stringDataPotions);
        
        Debug.Log("Sauvegarde effectuée");
    }

    public void Load()
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
        
        for (int i = 0; i < dataEnemies.Length/9; i++)
        {
            Destroy(GetComponent<monstersFight>().Enemies[i]);
            GetComponent<monstersFight>().DeleteEnemy(i);
        }

        
        
        for (int i = 0; i < dataEnemies.Length/9; i++)
        {
            if (dataEnemies[9 * i] == "wolfEnemy")
            {
                Vector3 position = new Vector3(float.Parse(dataEnemies[9 * i + 3]), float.Parse(dataEnemies[9 * i + 4]), float.Parse(dataEnemies[9 * i + 5]));
                Vector3 rotation = new Vector3(float.Parse(dataEnemies[9 * i + 6]), float.Parse(dataEnemies[9 * i + 7]), float.Parse(dataEnemies[9 * i + 8]));

                GameObject obj = Instantiate(wolfEnemy, position, Quaternion.Euler(rotation));

                int num = int.Parse(dataEnemies[9 * i + 1]);
                obj.GetComponent<wolfScript>().num = num;
                obj.GetComponent<wolfScript>().hitPoints = int.Parse(dataEnemies[9 * i + 2]);

                GetComponent<monstersFight>().AddEnemy(num, obj);
            }
            else if (dataEnemies[9 * i] == "wolfBoss")
            {
                Vector3 position = new Vector3(float.Parse(dataEnemies[9 * i + 3]), float.Parse(dataEnemies[9 * i + 4]), float.Parse(dataEnemies[9 * i + 5]));
                Vector3 rotation = new Vector3(float.Parse(dataEnemies[9 * i + 6]), float.Parse(dataEnemies[9 * i + 7]), float.Parse(dataEnemies[9 * i + 8]));

                GameObject obj = Instantiate(wolfBoss, position, Quaternion.Euler(rotation));
                
                int num = int.Parse(dataEnemies[9 * i + 1]);
                obj.GetComponent<bossScript>().num = num;
                obj.GetComponent<bossScript>().hitPoints = int.Parse(dataEnemies[9 * i + 2]);

                GetComponent<monstersFight>().AddEnemy(num, obj);
                GameObject.Find("Joueur").GetComponentInChildren<WinScreen>().bossWolf = obj;
            }
        }

    
        string saveStringAllies = File.ReadAllText(Application.dataPath + "/dataAllies.txt");
        string[] dataAllies = saveStringAllies.Split(new[] {saveSeparator}, System.StringSplitOptions.None);

        int cpt = 0;
        
        for (int i = 0; i < GetComponent<monstersFight>().GetCountAlly(); i++)
        {
            if (GetComponent<monstersFight>().Allies[i] && GetComponent<monstersFight>().Allies[i].CompareTag("wolfAlly"))
            {
                Destroy(GetComponent<monstersFight>().Allies[i]);
                GetComponent<monstersFight>().DeleteAlly(i);
                cpt++;
            }
        }

        // si supérieur à 0 il fut augmenter l'indice sinon le baisser
        int diffLoups = dataAllies.Length / 9 - cpt;

        GetComponent<monstersFight>().ChangeNumberOfAllies(diffLoups);
        
        
        for (int i = 0; i < (dataAllies.Length - 1)/9; i++)
        {
            if (dataAllies[9 * i] == "wolfAlly")
            {
                Vector3 position = new Vector3(float.Parse(dataAllies[9 * i + 3]), float.Parse(dataAllies[9 * i + 4]), float.Parse(dataAllies[9 * i + 5]));
                Vector3 rotation = new Vector3(float.Parse(dataAllies[9 * i + 6]), float.Parse(dataAllies[9 * i + 7]), float.Parse(dataAllies[9 * i + 8]));

                GameObject obj = Instantiate(wolfAlly, position, Quaternion.Euler(rotation));
                
                int num = int.Parse(dataAllies[9 * i + 1]);
                obj.GetComponent<wolfReanimated>().num = num;
                obj.GetComponent<wolfReanimated>().hitPoints = int.Parse(dataAllies[9 * i + 2]);
                
                GetComponent<monstersFight>().AddAlly(num, obj);
            }
        }
        GetComponent<reanimationMonstre>().nbMonstresReanimables = int.Parse(dataAllies[dataAllies.Length - 1]);
        
        GameObject potion = GameObject.Find("Potions");

        string saveStringPotions = File.ReadAllText(Application.dataPath + "/dataPotions.txt");
        string[] dataPotions = saveStringPotions.Split(new[] {saveSeparator}, System.StringSplitOptions.None);
        
        foreach (Transform child in potion.transform)
        {
            Destroy(child);
        }
        
        for (int i = 0; i < dataPotions.Length/3; i++)
        {
            Vector3 pos = new Vector3(float.Parse(dataPotions[i]), float.Parse(dataPotions[i + 1]), float.Parse(dataPotions[i + 2]));
            Instantiate(potion, pos, Quaternion.identity);
        }

        Debug.Log("Chargement effectué");
    }
}
