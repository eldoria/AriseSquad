using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class monstersFight : MonoBehaviour
{
    public int moveSpeed;
    public int detectionDistance;
    [SerializeField]private GameObject[] wolfEnemies;
    private int indE = 0;
    [SerializeField]private GameObject[] wolfAllies;
    private int indA = 0;
    private float timeDelay = 0;

    private void Awake()
    {
        wolfEnemies = new GameObject[100];
        wolfAllies = new GameObject[100];
    }

    public void AddWolfEnemy(GameObject wolfEnemy)
    {
        wolfEnemies[indE++] = wolfEnemy;
    }

    public void DeleteWolfEnemy(int count)
    {
        wolfEnemies[count] = null;
    }
    
    public void AddWolfAlly(GameObject wolfAlly)
    {
        wolfAllies[indA++] = wolfAlly;
    }
    
    public void DeleteWolfAlly(int count)
    {
        wolfAllies[count] = null;
    }

    public void checkForFight()
    {
        for (int i = 0; i < indA; i++)
        {
            for (int j = 0; j < indE; j++)
            {
                //if only the 2 monsters exist and both are not already attacking
                if (wolfAllies[i] && wolfEnemies[j])
                {
                    if(wolfAllies[i].GetComponent<wolfReanimated>().isAttacking == false || wolfEnemies[j].GetComponent<wolfScript>().isAttacking == false)
                    {
                        // if the distance is inferior to the detectionDistance
                        if(Vector3.Distance(wolfAllies[i].transform.position, wolfEnemies[j].transform.position) <= 120)
                        {
                            //3 cases possibles
                            
                            //first case : both of the wolfs are not attacking
                            if (wolfAllies[i].GetComponent<wolfReanimated>().isAttacking == false &&
                                wolfEnemies[j].GetComponent<wolfScript>().isAttacking == false)
                            {
                                wolfAllies[i].GetComponent<wolfReanimated>().isAttacking = true;
                                wolfEnemies[j].GetComponent<wolfScript>().isAttacking = true;
                                MakeFigthsBtwnMonsters (i, j, 0);
                            }
                            // the ennemy wolf is not attacking but our wolf is currently attacking
                            else if(wolfAllies[i].GetComponent<wolfReanimated>().isAttacking &&
                                    wolfEnemies[j].GetComponent<wolfScript>().isAttacking == false)
                            {
                                wolfEnemies[j].GetComponent<wolfScript>().isAttacking = true;
                                MakeFigthsBtwnMonsters (i, j, 1);
                            }
                            // our wolf is not attcking and the ennemey wolf is currently attacking
                            else if(wolfAllies[i].GetComponent<wolfReanimated>().isAttacking == false &&
                                    wolfEnemies[j].GetComponent<wolfScript>().isAttacking)
                            {
                                wolfAllies[i].GetComponent<wolfReanimated>().isAttacking = true;
                                MakeFigthsBtwnMonsters (i,j,2);
                            }
                        }
                    }
                }
            }
        }
    }

    public void MakeFigthsBtwnMonsters(int numWolfAlly, int numWolfEnemy, int caseSelected)
    {
        if (wolfAllies[numWolfAlly] && wolfEnemies[numWolfEnemy])
        {
            if (caseSelected == 0)
            {
                wolfAllies[numWolfAlly].transform.LookAt(wolfEnemies[numWolfEnemy].transform);
                wolfEnemies[numWolfEnemy].transform.LookAt(wolfAllies[numWolfAlly].transform);
                if (Vector3.Distance(wolfAllies[numWolfAlly].transform.position,
                        wolfEnemies[numWolfEnemy].transform.position) > 25)
                {
                    wolfAllies[numWolfAlly].transform.position += wolfAllies[numWolfAlly].transform.rotation * Vector3.forward * moveSpeed * Time.deltaTime;
                    wolfEnemies[numWolfEnemy].transform.position += wolfEnemies[numWolfEnemy].transform.rotation * Vector3.forward * moveSpeed * Time.deltaTime;
                    
                    wolfAllies[numWolfAlly].GetComponent<wolfReanimated>().AnimationMove();
                    wolfEnemies[numWolfEnemy].GetComponent<wolfScript>().AnimationMove();
                }
                else
                {
                    wolfAllies[numWolfAlly].GetComponent<wolfReanimated>().AnimationAttack();
                    wolfEnemies[numWolfEnemy].GetComponent<wolfScript>().AnimationAttack();
                }
            }
            else if (caseSelected == 1)
            {
                wolfEnemies[numWolfEnemy].transform.LookAt(wolfAllies[numWolfAlly].transform);
                if (Vector3.Distance(wolfAllies[numWolfAlly].transform.position,
                        wolfEnemies[numWolfEnemy].transform.position) > 25)
                {
                    wolfEnemies[numWolfEnemy].transform.position += wolfEnemies[numWolfEnemy].transform.rotation * Vector3.forward * moveSpeed * Time.deltaTime;
                    
                    wolfEnemies[numWolfEnemy].GetComponent<wolfScript>().AnimationMove();
                }
                else
                {
                    wolfEnemies[numWolfEnemy].GetComponent<wolfScript>().AnimationAttack();
                }
                
            }
            else
            {
                wolfAllies[numWolfAlly].transform.LookAt(wolfEnemies[numWolfEnemy].transform);
                if (Vector3.Distance(wolfAllies[numWolfAlly].transform.position,
                        wolfEnemies[numWolfEnemy].transform.position) > 25)
                {
                    wolfAllies[numWolfAlly].transform.position += wolfAllies[numWolfAlly].transform.rotation * Vector3.forward * moveSpeed * Time.deltaTime;
                    
                    wolfAllies[numWolfAlly].GetComponent<wolfReanimated>().AnimationMove();
                }
                else
                {
                    wolfAllies[numWolfAlly].GetComponent<wolfReanimated>().AnimationAttack();
                }
            }
            StartCoroutine(Wait(numWolfAlly, numWolfEnemy, caseSelected));
        }
        else
        {
            if (wolfAllies[numWolfAlly]) wolfAllies[numWolfAlly].GetComponent<wolfReanimated>().isAttacking = false;
            else if (wolfEnemies[numWolfEnemy]) wolfEnemies[numWolfEnemy].GetComponent<wolfScript>().isAttacking = false;
        }
    }

    IEnumerator Wait(int numWolfAlly, int numWolfEnemy, int caseSelected)
    {
        yield return new WaitForSeconds(Time.deltaTime);
        MakeFigthsBtwnMonsters(numWolfAlly, numWolfEnemy, caseSelected);
    }

    private void Update()
    {
        timeDelay += Time.deltaTime;
        if (timeDelay > 2)
        {
            timeDelay = 0f;
            checkForFight();
        }
    }
}
