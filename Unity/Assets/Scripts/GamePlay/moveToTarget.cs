using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToTarget : MonoBehaviour
{
    public Transform target;

    private float attackDistance;
    private float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        if (CompareTag("wolfAlly"))
        {
            moveSpeed = GetComponent<wolfReanimated>().moveSpeed;
            attackDistance = 22;
        }
        else if (CompareTag("wolfEnemy"))
        {
            moveSpeed = GetComponent<wolfScript>().moveSpeed;
            attackDistance = 22;
        }
        else if (CompareTag("wolfBoss"))
        {
            moveSpeed = GetComponent<bossScript>().moveSpeed;
            attackDistance = 30;
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveUnitToHisTarget();
    }

    private void moveUnitToHisTarget()
    {
        if (target)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            
            if (distance <= 120)
            {
                transform.LookAt(target);
                // l'unité attaque
                if (distance <= attackDistance)
                {
                    if (CompareTag("wolfAlly"))
                    {
                        GetComponent<wolfReanimated>().AnimationAttack();
                    }
                    else if (CompareTag("wolfEnemy"))
                    {
                        GetComponent<wolfScript>().AnimationAttack();
                    }
                    else if (CompareTag("wolfBoss"))
                    {
                        GetComponent<bossScript>().AnimationAttack();
                    }
                }
                // sinon elle se déplace vers celle-ci
                else
                {
                    transform.position += transform.rotation * Vector3.forward * moveSpeed * Time.deltaTime;
                    if (CompareTag("wolfAlly"))
                    {
                        GetComponent<wolfReanimated>().AnimationMove();
                    }
                    else if (CompareTag("wolfEnemy"))
                    {
                        GetComponent<wolfScript>().AnimationMove();
                    }
                    else if (CompareTag("wolfBoss"))
                    {
                        GetComponent<bossScript>().AnimationMove();
                    }
                }
            }
            // l'unité est trop loin et arrête de bouger
            else
            {
                if (CompareTag("wolfAlly"))
                {
                    GetComponent<wolfReanimated>().StopMoving();
                }
                else if (CompareTag("wolfEnemy"))
                {
                    GetComponent<wolfScript>().StopMoving();
                }
                else if (CompareTag("wolfBoss"))
                {
                    GetComponent<bossScript>().StopMoving();
                }
            }
        }
    }
    /*
    private void AttackCible()
    {
        transform.LookAt(cible.transform);
        float distance = Vector3.Distance(transform.position, cible.transform.position);
        if (distance > 20f && distance < 120f)
        {
            animation.SetBool(Moving, true);
            var direction = Vector3.forward;
            transform.position += transform.rotation * direction * moveSpeed * Time.deltaTime;
        }
        else if(distance < 20f)
        {
            animation.SetTrigger(Attack);
        }
        else
        {
            animation.SetBool(Moving, false);
        }
    }*/
}
