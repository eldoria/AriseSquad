using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wolfScript : MonoBehaviour
{

    private Transform player;
    [SerializeField] private Animator animation;
    [SerializeField] private BoxCollider attackCollider;
    public float moveSpeed;
    public float detectionDistance = 100f;
    public float attackDistance = 20f;
    public int hitPoints = 2;

    private static readonly int Moving = Animator.StringToHash("moving");
    private static readonly int Attack = Animator.StringToHash("attack");

    private bool hasHit = false;
    public bool hasBeenHit = false;


    // Update is called once per frame
    void Update()
    {

        if (GameManager.gameOver == true)
        {
            animation.SetBool(Attack, false);
            animation.SetBool(Moving, false);
            return;
        }
        
        player = GameObject.FindWithTag("Player").transform;
        if (!player.GetComponent<playerScript>().Attacking())
        {
            hasBeenHit = false;
        }
        if (Vector3.Distance(transform.position, player.position) < attackDistance && !animation.GetCurrentAnimatorStateInfo(0).IsName("AttackSalto"))
        {
            transform.LookAt(player);
            animation.SetTrigger(Attack);
        }
        else if (Vector3.Distance(transform.position, player.position) < detectionDistance && !animation.GetCurrentAnimatorStateInfo(0).IsName("AttackSalto"))
        {
            animation.SetBool(Moving, true);
            transform.LookAt(player);
            var direction = Vector3.forward;
            transform.position += transform.rotation * direction * moveSpeed * Time.deltaTime;
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
        if ((other.gameObject.layer == 8 || other.gameObject.layer == 9) && attackCollider.enabled && !hasHit)
        {
            other.GetComponent<playerScript>().TakeDamage(1);
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
}