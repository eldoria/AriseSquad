using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class generationMap : MonoBehaviour
{
    [SerializeField] private int tailleMap = 1000;
    public GameObject [] treePrefab;
    public GameObject monsterPrefab;
    public GameObject bossPrefab;
    private float randomX, randomZ;
    // the first two values are the boundary of the x zone, the nex two are the boudary of the z zone, then repeats for each area
    private int monsterAreas = 0;
    private List<int> areaMonsters = new List<int>();
/*
    private readonly int [] areaMonsters = {200, 400, 200, 400, 100, 300, 50, 250, -120, 120, 770, 1000};
*/
    private readonly int[] areaBoss = {100, 400, 150, 300};
    private bool areaWithMonsters;
    private bool bossCreated = false;
    void Start()
    {
        monsterAreas = Random.Range(3, 8);
        for (int i = 0; i < monsterAreas; i++)
        {
            int val = Random.Range(-tailleMap, tailleMap - 200);
            areaMonsters.Add(val);
            areaMonsters.Add(val + 200);
            val = Random.Range(-tailleMap, tailleMap - 200);
            areaMonsters.Add(val);
            areaMonsters.Add(val + 200);
        }
        for (int i = -tailleMap; i < tailleMap; i += 50)
        {
            for (int j = -tailleMap; j < tailleMap; j += 50)
            {
                if (createAreaWithMonsters(i, j) == false && createAreaWithBoss(i, j) == false)
                {
                    Vector3 position = new Vector3(x: i + Random.Range(0f, 8f), 0f, z: j + Random.Range(0f, 8f));
                    int treeChoose = Random.Range(0, 2);
                    if (treeChoose == 0) Instantiate(treePrefab[0], position + new Vector3(0, 9f, 0), treePrefab[0].transform.rotation);
                    else Instantiate(treePrefab[1], position + new Vector3(0, 35.5f, 0), treePrefab[1].transform.rotation);
                }
            }
        }
    }

    bool createAreaWithMonsters(int i, int j)
    {
        areaWithMonsters = false;
        // check if the coordinates match with a zone monster
        for (int l = 0; l < monsterAreas * 4 /*areaMonsters.Length*/ && !areaWithMonsters; l += 4)
        {
            areaWithMonsters = (i > areaMonsters[l] && i < areaMonsters[l + 1]) && (j > areaMonsters[l + 2] && j < areaMonsters[l + 3]);
        }
        // if the coordinates match with a zone monster, spawn monsters
        if (areaWithMonsters & j < 800)
        {
            Vector3 position = new Vector3(x: i + Random.Range(0f, 8f), -.1f, z: j + Random.Range(0f, 8f));
            GameObject monster = Instantiate(monsterPrefab, position, monsterPrefab.transform.rotation);
            monster.GetComponent<wolfScript>().num = GetComponent<monstersFight>().GetCountEnemy();
            GetComponent<monstersFight>().AddEnemy(monster);
        }
        return areaWithMonsters;
    }

    bool createAreaWithBoss(int i, int j)
    {
        bool areaWithBoss = false;
        for (int l = 0; l < areaBoss.Length && !areaWithBoss; l += 4)
        {
            areaWithBoss = (i > areaBoss[l] && i < areaBoss[l + 1]) && (j > areaBoss[l + 2] && j < areaBoss[l + 3]);
        }

        if (areaWithBoss && !bossCreated)
        {
            Vector3 position = new Vector3(i + 8f, 0f, j + 8f);
            GameObject boss = Instantiate(bossPrefab, position, bossPrefab.transform.rotation);
            bossCreated = true;
            boss.GetComponent<bossScript>().num = GetComponent<monstersFight>().GetCountEnemy();
            GameObject.Find("Joueur").GetComponentInChildren<WinScreen>().bossWolf = boss;
            GetComponent<monstersFight>().AddEnemy(boss);
        }

        return areaWithBoss;
    }
}
