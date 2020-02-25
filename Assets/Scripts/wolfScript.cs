using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wolfScript : MonoBehaviour
{

    private Transform player;
    [SerializeField] private Animator animation;
    public float moveSpeed;
    private static readonly int Moving = Animator.StringToHash("moving");


    // Update is called once per frame
    void Update()
    {
        animation.SetBool(Moving, true);
        player = GameObject.FindWithTag("Player").transform;
        transform.LookAt(player);
        //transform.position = Vector3.Lerp(transform.position, player.position, 0.01f);
        var direction = Vector3.forward;
        transform.position += transform.rotation * direction * moveSpeed * Time.deltaTime;
    }
}