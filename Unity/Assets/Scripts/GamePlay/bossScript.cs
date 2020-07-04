using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bossScript : MonoBehaviour
{
    //private Transform player;
    [SerializeField] private new Animator animation;
    [SerializeField] private BoxCollider attackCollider;
    public float moveSpeed;
    //public float detectionDistance = 110f;
    //public float attackDistance = 40f;
    public int hitPoints = 3;
    
    private static readonly int Moving = Animator.StringToHash("moving");
    private static readonly int Attack = Animator.StringToHash("attack");

    private bool hasHit = false;
    //public bool hasBeenHit = false;
    public int num;
    [SerializeField] private GameObject objectWithScripts;

    private void Start()
    {
        //player = GameObject.FindWithTag("Player").transform;
        objectWithScripts = GameObject.Find("Scripts_Map_boss");
        //num = objectWithScripts.GetComponent<monstersFight>().GetCountEnemy();
    }

    void Update()
    {
        if (GameManager.gameOver == true)
        {
            animation.SetBool(Attack, false);
            animation.SetBool(Moving, false);
            return;
        }
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        /*
        if (!player.GetComponent<playerScript>().Attacking())
        {
            hasBeenHit = false;
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
            if (other.CompareTag("wolfAlly"))
            {
                other.GetComponent<wolfReanimated>().TakeDamage(1);
                hasHit = true;
            }
            else if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerHealth>().TakeDamage(20);
                hasHit = true;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            objectWithScripts.GetComponent<monstersFight>().DeleteEnemy(num);
            Destroy(gameObject);
            objectWithScripts.GetComponent<reanimationMonstre>().UpdateNbMonstre();
        }
    }

    public void AnimationMove()
    {
        animation.SetBool(Moving, true);
    }

    public void StopMoving()
    {
        animation.SetBool(Moving, false);
    }

    public void AnimationAttack()
    {
        animation.SetTrigger(Attack);
    }
}

    