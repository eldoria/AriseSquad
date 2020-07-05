using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class wolfScript : MonoBehaviour
{
    //private Transform player;
    [SerializeField] private Animator animation;
    [SerializeField] private BoxCollider attackCollider;

    public float moveSpeed;
    //public float detectionDistance = 100f;
    //public float attackDistance = 20f;
    public int hitPoints = 2;

    private static readonly int Moving = Animator.StringToHash("moving");
    private static readonly int Attack = Animator.StringToHash("attack");

    private bool hasHit = false;
    //public bool hasBeenHit = false;

    public int num;

    [SerializeField] private GameObject objectWithScripts;

    private void Start()
    {
        objectWithScripts = GameObject.Find("Scripts_Map_boss");
        //player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (GameManager.gameOver == true)
        {
            animation.SetBool(Attack, false);
            animation.SetBool(Moving, false);
            return;
        }
        else
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        /*
        if (!player.GetComponent<playerScript>().Attacking())
        {
            hasBeenHit = false;
        }
        if (Vector3.Distance(transform.position, player.transform.position) < attackDistance && !animation.GetCurrentAnimatorStateInfo(0).IsName("AttackSalto") && isAttacking == false)
        {
            transform.LookAt(player.transform);
            animation.SetTrigger(Attack);
        }
        else if (Vector3.Distance(transform.position, player.transform.position) < detectionDistance && !animation.GetCurrentAnimatorStateInfo(0).IsName("AttackSalto") && isAttacking == false)
        {
            animation.SetBool(Moving, true);
            transform.LookAt(player.transform);
            var direction = Vector3.forward;
            transform.position += transform.rotation * direction * moveSpeed * Time.deltaTime;
        }
        else if(!isAttacking)
        {
            animation.SetBool(Moving, false);
        }
        */
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
                other.GetComponent<PlayerHealth>().TakeDamage(10);
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

    // use in monstersFights script
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