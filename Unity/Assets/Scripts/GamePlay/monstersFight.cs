using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class monstersFight : MonoBehaviour
{
    public int moveSpeed;
    public int detectionDistance;
    public GameObject[] Enemies;
    private int indE = 0;
    public GameObject[] Allies;
    private int indA = 0;
    private float timeDelay = 0;
    private int nbEnemyDie;

    [SerializeField] private WinGame scriptWinGame;

    private void Awake()
    {
        Enemies = new GameObject[100];
        Allies = new GameObject[100];
    }

    public int GetCountAlly()
    {
        return indA;
    }

    public int GetCountEnemy()
    {
        return indE;
    }

    public void AddEnemy(GameObject Enemy)
    {
        Enemies[indE++] = Enemy;
    }

    public void AddEnemy(int cpt, GameObject Enemy)
    {
        Enemies[cpt] = Enemy;
    }

    public void DeleteEnemy(int count)
    {
        if (Enemies[count] != null)
        {
            Enemies[count] = null;
            GetNbEnemyDie();
        }
    }
    
    public void AddAlly(GameObject Ally)
    {
        Allies[indA++] = Ally;
    }
    
    public void AddAlly(int cpt, GameObject Ally)
    {
        Allies[cpt] = Ally;
    }
    
    
    public void DeleteAlly(int count)
    {
        Allies[count] = null;
    }

    public void ChangeNumberOfAllies(int val)
    {
        indA += val;
    }

    public void GetNbEnemyDie()
    {
        if (GameObject.Find("Scripts_Map_boss").GetComponent<SaveData>().isLoading == false)
        {
            nbEnemyDie++;
            //Debug.Log("nbEnemyDie : " + nbEnemyDie + " et indE : " + indE);
            if (nbEnemyDie == indE)
            {
                scriptWinGame.endGame();
            }
        }
    }

    private void CalculateDistance()
    {
        // Assigne les combats pour les unités
        //Debug.Log(indA + " " + indE);
        for (int i = 0; i < indA; i++)
        {
            for (int j = 0; j < indE; j++)
            {
                if (Allies[i] && Enemies[j])
                {
                    AssignUnits(Allies[i], Enemies[j]);
                }
            }
        }
    }

    private void AssignUnits(GameObject Ally, GameObject Enemy)
    {
        float distBtwUnits = Vector3.Distance(Ally.transform.position, Enemy.transform.position);
        if (!Ally.CompareTag("Player"))
        {
            if (Ally.GetComponent<moveToTarget>().target)
            {
                float distBtwTargetAlly = Vector3.Distance(Ally.transform.position,
                    Ally.GetComponent<moveToTarget>().target.transform.position);
                if (distBtwUnits < distBtwTargetAlly) Ally.GetComponent<moveToTarget>().target = Enemy.transform;   
            }
            else if(!Ally.CompareTag("Player"))
            {
                Ally.GetComponent<moveToTarget>().target = Enemy.transform;  
            }
        }

        if (Enemy.GetComponent<moveToTarget>().target)
        {
            float distBtwTargetEnemy = Vector3.Distance(Enemy.transform.position,
                Enemy.GetComponent<moveToTarget>().target.transform.position);
            if (distBtwUnits < distBtwTargetEnemy)
            {
                Enemy.GetComponent<moveToTarget>().target = Ally.transform;
            }
        }
        else
        {
            Enemy.GetComponent<moveToTarget>().target = Ally.transform;
        }
    }

    /*
    private void AssignTargetForEnemy(int numAlly, int numEnemy)
    {
        Enemies[numEnemy].GetComponent<moveToTarget>().target = Allies[numAlly].transform;
    }
    private void AssignTargetsForAlly(int numAlly, int numEnemy)
    {
        Allies[numAlly].GetComponent<moveToTarget>().target = Enemies[numEnemy].transform;
    float distance =  Vector3.Distance(Ally.transform.position, Enemy.transform.position);
        if (Ally.GetComponent<entityType>().GetType() != "player")
        {
            // Si les deux unités ont déjà des cibles assignés
            if (Ally.GetComponent<moveToTarget>().target && Enemy.GetComponent<moveToTarget>().target)
            {
                // Si l'unité ennemie est plus proche que son ancienne cible, change de cible
                if(distance < Vector3.Distance(Ally.transform.position, Ally.GetComponent<moveToTarget>().target.position)) Ally.GetComponent<moveToTarget>().target = Enemy.transform;
                // Si l'unité allié est plus proche que son ancienne cible, change de cible
                if(distance < Vector3.Distance(Enemy.transform.position, Enemy.GetComponent<moveToTarget>().target.position)) Enemy.GetComponent<moveToTarget>().target = Ally.transform;
            }
            else if(Ally.GetComponent<moveToTarget>().target)
            {
                // Si l'unité ennemie est plus proche que son ancienne cible, change de cible
                if(distance < Vector3.Distance(Ally.transform.position, Ally.GetComponent<moveToTarget>().target.position)) Ally.GetComponent<moveToTarget>().target = Enemy.transform;
                Enemy.GetComponent<moveToTarget>().target = Ally.transform;
            }
            else if (Enemy.GetComponent<moveToTarget>().target)
            {
                Ally.GetComponent<moveToTarget>().target = Enemy.transform;
                // Si l'unité allié est plus proche que son ancienne cible, change de cible
                if(distance < Vector3.Distance(Enemy.transform.position, Enemy.GetComponent<moveToTarget>().target.position)) Enemy.GetComponent<moveToTarget>().target = Ally.transform;
            }
            else
            {
                Ally.GetComponent<moveToTarget>().target = Enemy.transform;
                Enemy.GetComponent<moveToTarget>().target = Ally.transform;
            }
        }
        else if (Ally.GetComponent<entityType>().GetType() == "player")
        {
            if (Enemy.GetComponent<moveToTarget>().target)
            {
                // Si l'unité allié est plus proche que son ancienne cible, change de cible
                if(distance < Vector3.Distance(Enemy.transform.position, Enemy.GetComponent<moveToTarget>().target.position)) Enemy.GetComponent<moveToTarget>().target = Ally.transform;
            }
            else
            {
                Enemy.GetComponent<moveToTarget>().target = Ally.transform;
            }
        }

        if (Ally.GetComponent<entityType>().GetType() == "wolfAllie")
        {
            if (Ally.GetComponent<wolfReanimated>().cible)
            {
                float dist = Vector3.Distance(Allies[i].transform.position,
                    Allies[i].GetComponent<wolfReanimated>().cible.position);
                if(valMin < dist) Allies[i].GetComponent<wolfReanimated>().cible = Enemies[j].transform;
            }
            else
            {
                Allies[i].GetComponent<wolfReanimated>().cible = Enemies[j].transform;
            }
        }
        if (Enemies[j].GetComponent<wolfScript>().cible)
        {
            float dist = Vector3.Distance(Enemies[j].transform.position,
                Enemies[j].GetComponent<wolfScript>().cible.position);
            if (valMin < dist) Enemies[j].GetComponent<wolfScript>().cible = Allies[i].transform;
        }
        else
        {
            Enemies[j].GetComponent<wolfScript>().cible = Allies[i].transform;
        }
}

    
    public void MakeFigthsBtwnMonsters(int numWolfAlly, int numWolfEnemy, int caseSelected)
    {
        if (Allies[numWolfAlly] && Enemies[numWolfEnemy])
        {
            if (caseSelected == 0)
            {
                Allies[numWolfAlly].transform.LookAt(Enemies[numWolfEnemy].transform);
                Enemies[numWolfEnemy].transform.LookAt(Allies[numWolfAlly].transform);
                if (Vector3.Distance(Allies[numWolfAlly].transform.position,
                        Enemies[numWolfEnemy].transform.position) > 25)
                {
                    Allies[numWolfAlly].transform.position += Allies[numWolfAlly].transform.rotation * Vector3.forward * moveSpeed * Time.deltaTime;
                    Enemies[numWolfEnemy].transform.position += Enemies[numWolfEnemy].transform.rotation * Vector3.forward * moveSpeed * Time.deltaTime;
                    
                    Allies[numWolfAlly].GetComponent<wolfReanimated>().AnimationMove();
                    Enemies[numWolfEnemy].GetComponent<wolfScript>().AnimationMove();
                }
                else
                {
                    Allies[numWolfAlly].GetComponent<wolfReanimated>().AnimationAttack();
                    Enemies[numWolfEnemy].GetComponent<wolfScript>().AnimationAttack();
                }
            }
            else if (caseSelected == 1)
            {
                Enemies[numWolfEnemy].transform.LookAt(Allies[numWolfAlly].transform);
                if (Vector3.Distance(Allies[numWolfAlly].transform.position,
                        Enemies[numWolfEnemy].transform.position) > 25)
                {
                    Enemies[numWolfEnemy].transform.position += Enemies[numWolfEnemy].transform.rotation * Vector3.forward * moveSpeed * Time.deltaTime;
                    
                    Enemies[numWolfEnemy].GetComponent<wolfScript>().AnimationMove();
                }
                else
                {
                    Enemies[numWolfEnemy].GetComponent<wolfScript>().AnimationAttack();
                }
                
            }
            else
            {
                Allies[numWolfAlly].transform.LookAt(Enemies[numWolfEnemy].transform);
                if (Vector3.Distance(Allies[numWolfAlly].transform.position,
                        Enemies[numWolfEnemy].transform.position) > 25)
                {
                    Allies[numWolfAlly].transform.position += Allies[numWolfAlly].transform.rotation * Vector3.forward * moveSpeed * Time.deltaTime;
                    
                    Allies[numWolfAlly].GetComponent<wolfReanimated>().AnimationMove();
                }
                else
                {
                    Allies[numWolfAlly].GetComponent<wolfReanimated>().AnimationAttack();
                }
            }
            StartCoroutine(Wait(numWolfAlly, numWolfEnemy, caseSelected));
        }
        else
        {
            if (Allies[numWolfAlly]) Allies[numWolfAlly].GetComponent<wolfReanimated>().isAttacking = false;
            else if (Enemies[numWolfEnemy]) Enemies[numWolfEnemy].GetComponent<wolfScript>().isAttacking = false;
        }
    }

    IEnumerator Wait(int numWolfAlly, int numWolfEnemy, int caseSelected)
    {
        yield return new WaitForSeconds(Time.deltaTime);
        MakeFigthsBtwnMonsters(numWolfAlly, numWolfEnemy, caseSelected);
    }*/

    private void Update()
    {
        timeDelay += Time.deltaTime;
        if (timeDelay > .1f)
        {
            CalculateDistance();
        }
    }
}