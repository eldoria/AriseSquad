using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wolfScript : MonoBehaviour
{

    private Transform player;
    public float moveSpeed;
    


    // Update is called once per frame
    void Update()
    {

        player = GameObject.FindWithTag("Player").transform;
        transform.LookAt(player);
        transform.position = Vector3.Lerp(transform.position, player.position, 0.01f);
    }
}