using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class wolfReanimated : MonoBehaviour
{

    private Transform player;
    [SerializeField] private Animator animation;
    [SerializeField] private BoxCollider attackCollider;
    public float moveSpeed;
    public float detectionDistance = 100f;
    public float attackDistance = 20f;
    public int hitPoints = 2;
    private float dist;
    public bool isAttacking = false;

    private static readonly int Moving = Animator.StringToHash("moving");
    private static readonly int Attack = Animator.StringToHash("attack");

    private bool hasHit = false;
    public bool hasBeenHit = false;

    void Update()
    {
        player = GameObject.FindWithTag("Player").transform;
        dist = Vector3.Distance(transform.position, player.transform.position);
        if (!player.GetComponent<playerScript>().Attacking())
        {
            hasBeenHit = false;
        }
        if (dist > 40 && isAttacking == false)
        {
            animation.SetBool(Moving, true);
            transform.LookAt(player.transform);
            transform.position += transform.rotation * Vector3.forward * moveSpeed * Time.deltaTime;
        }
        else
        {
            animation.SetBool(Moving, false);
        }

        if (animation.GetCurrentAnimatorStateInfo(0).IsName("AttackSalto"))
        {
            if (animation.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f && animation.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f)
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
        if (other.gameObject.layer == 10 && attackCollider.enabled && !hasHit)
        {
            other.GetComponent<wolfScript>().TakeDamage(1);
            hasHit = true;
        }
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    public void AnimationAttack()
    {
        animation.SetTrigger(Attack);
    }

    public void AnimationMove()
    {
        animation.SetBool(Moving, true);
    }
}