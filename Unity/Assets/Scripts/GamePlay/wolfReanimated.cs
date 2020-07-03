using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class wolfReanimated : MonoBehaviour
{

    private Transform player;
    [SerializeField] private new Animator animation;
    [SerializeField] private BoxCollider attackCollider;
    public float moveSpeed;
    //public float detectionDistance = 100f;
    //public float attackDistance = 20f;
    public int hitPoints = 1;
    private float dist;
    //public bool isAttacking = false;

    private static readonly int Moving = Animator.StringToHash("moving");
    private static readonly int Attack = Animator.StringToHash("attack");

    private bool hasHit = false;
    //public bool hasBeenHit = false;

    public int num;
    
    public GameObject objectWithScripts;
    private void Start()
    {
        objectWithScripts = GameObject.Find("Scripts_Map_boss");
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        followPlayer();
        /*
        if (!player.GetComponent<playerScript>().Attacking())
        {
            hasBeenHit = false;
        }
        else if(!isAttacking)
        {
            animation.SetBool(Moving, false);
        }*/
        if (animation.GetCurrentAnimatorStateInfo(0).IsName("AttackSalto"))
        {
            if (animation.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animation.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f)
            {
                attackCollider.enabled = true;
            }
            else
            {
                attackCollider.enabled = false;
                hasHit = false;
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (attackCollider.enabled && !hasHit)
        {
            if (other.CompareTag("wolfEnemy"))
            {
                other.GetComponent<wolfScript>().TakeDamage(1);
                hasHit = true;
            }
            else if (other.CompareTag("wolfBoss"))
            {
                other.GetComponent<bossScript>().TakeDamage(1);
                hasHit = true;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            objectWithScripts.GetComponent<monstersFight>().DeleteAlly(num);
            Destroy(gameObject);
        }
    }

    public void followPlayer()
    {
        if ((!GetComponent<moveToTarget>().target || (GetComponent<moveToTarget>().target && Vector3.Distance(transform.position, GetComponent<moveToTarget>().target.transform.position) >= 120f)) &&
            Vector3.Distance(transform.position, player.transform.position) > 40)
        {
            animation.SetBool(Moving, true);
            transform.LookAt(player.transform);
            transform.position += transform.rotation * Vector3.forward * moveSpeed * Time.deltaTime;
        }
        else if (!GetComponent<moveToTarget>().target) animation.SetBool(Moving, false);
    }
    
    public void AnimationAttack()
    {
        animation.SetTrigger(Attack);
    }

    public void AnimationMove()
    {
        animation.SetBool(Moving, true);
    }

    public void StopMoving()
    {
        animation.SetBool(Moving, false);
    }
}