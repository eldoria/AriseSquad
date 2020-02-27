using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using Random = UnityEngine.Random;

public class generationMap : MonoBehaviour
{
    public GameObject [] treePrefab;
    public GameObject monsterPrefab;
    public GameObject bossPrefab;
    private float posX = -495;
    private float posZ = -495;
    private float randomX, randomZ;
    // the first two values are the boundary of the x zone, the nex two are the boudary of the y zone, then repeats for each area
    private readonly int [] arrayMonsters = {300, 400, 200, 300, 100, 200, 50, 150};
    private readonly int[] areaBoss = {100, 400, 150, 300};
    private bool arrayWithMonsters;
    private bool bossCreated = false;
    void Start()
    {
        for (int i = -490; i < 490; i += 40)
        {
            for (int j = -490; j < 490; j += 40)
            {
                if (createArrayWithMonsters(i, j) == false && createAreaWithBoss(i, j) == false)
                {
                    Vector3 position = new Vector3(x: i + Random.Range(0f, 8f), 0f, z: j + Random.Range(0f, 8f));
                    int treeChoose = Random.Range(0, 2);
                    if (treeChoose == 0) Instantiate(treePrefab[0], position + new Vector3(0, 9f, 0), treePrefab[0].transform.rotation);
                    else Instantiate(treePrefab[1], position + new Vector3(0, 35.5f, 0), treePrefab[1].transform.rotation);
                }
                
            }
        }
    }

    bool createArrayWithMonsters(int i, int j)
    {
        arrayWithMonsters = false;
        // check if the coordinates match with a zone monster
        for (int l = 0; l < arrayMonsters.Length && !arrayWithMonsters; l += 4)
        {
            arrayWithMonsters = (i > arrayMonsters[l] && i < arrayMonsters[l + 1]) && (j > arrayMonsters[l + 2] && j < arrayMonsters[l + 3]);
            if (arrayWithMonsters) Debug.Log("x : " + i + ", z : " + j);
        }
        // if the coordinates match with a zone monster, spawn monsters
        if (arrayWithMonsters)
        {
            Vector3 position = new Vector3(x: i + Random.Range(0f, 8f), -.1f, z: j + Random.Range(0f, 8f));
            Instantiate(monsterPrefab, position, monsterPrefab.transform.rotation);
        }
        return arrayWithMonsters;
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
            Instantiate(bossPrefab, position, bossPrefab.transform.rotation);
            bossCreated = true;
        }

        return areaWithBoss;
    }
}
