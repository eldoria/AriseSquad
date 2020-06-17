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
    public static int hitPoints = 3;
    
    private static readonly int Moving = Animator.StringToHash("moving");
    private static readonly int Attack = Animator.StringToHash("attack");

    private bool hasHit = false;
    //public bool hasBeenHit = false;

    private void Start()
    {
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
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        /*
        if (!player.GetComponent<playerScript>().Attacking())
        {
            hasBeenHit = false;
        }*/
        if (animation.GetCurrentAnimatorStateInfo(0).IsName("AttackSalto"))
        {
            if (animation.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animation.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9)
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
        Debug.Log("BOSS A TOUCHE : ", other);
        if (attackCollider.enabled && !hasHit)
        {
            if (other.GetComponent<entityType>().GetType() == "wolfAlly")
            {
                other.GetComponent<wolfReanimated>().TakeDamage(1);
                hasHit = true;
            }
            else if (other.GetComponent<entityType>().GetType() == "player")
            {
                other.GetComponent<PlayerHealth>().TakeDamage(20);
                hasHit = true;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("LE BOSS A PERDU 1 PV");
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            Destroy(gameObject);
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

    