using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerScript : MonoBehaviour
{
    public Transform cameraTarget;
    [SerializeField]private float walkSpeed = 15f;
    [SerializeField]private float runSpeed = 35f;

    [SerializeField] private int hitPoints = 20;
    
    private float RotationSpeed;

    [SerializeField]private Animator animation;
    [SerializeField] private BoxCollider attackCollider;
    private static readonly int Moving = Animator.StringToHash("moving");
    private static readonly int Running = Animator.StringToHash("running");
    private static readonly int Attack = Animator.StringToHash("attack");

    

    private void Start()
    {
        GetComponent<hPDisplayScript>().ChangeHitPoints(hitPoints);
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.gameOver == true)
        {
            return;
        }
        
        var moveIntent = Vector3.zero;
        var moveSpeed = walkSpeed;
        var rotationAngle = 0f;
        var tmpEuler = transform.eulerAngles;
        short keysCount = 0;
        float RotationIntent = 0f;
        
        if (Input.GetKey(KeyCode.Z))
        {
            animation.SetBool(Moving,true);
            /*moveIntent = transform.position - camera.position;
            moveIntent.y = 0f;*/
            /*transform.eulerAngles = new Vector3(0f, cameraTarget.transform.eulerAngles.y, 0f);
            moveIntent += Vector3.forward;*/
            rotationAngle += Input.GetKey(KeyCode.Q) ? 360f : 0f;
            keysCount++;
        }

        if (Input.GetKey(KeyCode.S))
        {
            animation.SetBool(Moving,true);
            /*transform.eulerAngles = new Vector3(0f, cameraTarget.transform.eulerAngles.y - 180f, 0f);
            moveIntent += Vector3.forward;*/
            rotationAngle += 180f;
            keysCount++;
        }
        
        if (Input.GetKey(KeyCode.Q))
        {
            animation.SetBool(Moving,true);
            /*transform.eulerAngles = new Vector3(0f, cameraTarget.transform.eulerAngles.y - 90f, 0f);
            moveIntent += Vector3.forward;*/
            rotationAngle += 270f;
            keysCount++;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            animation.SetBool(Moving,true);
            /*transform.eulerAngles = new Vector3(0f, cameraTarget.transform.eulerAngles.y + 90f, 0f);
            moveIntent += Vector3.forward;*/
            rotationAngle += 90f;
            keysCount++;
        }

        /*if (!(Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.D)))
        {
            animation.SetBool(Moving, false);
        }*/
        
        if (keysCount == 0 || keysCount == 4)
        {
            animation.SetBool(Moving, false);
        }
        else
        {
            rotationAngle /= keysCount;
            if (keysCount % 2 == 0 && (rotationAngle - 180f < 0.01f && rotationAngle - 180f > -0.01f ||
                                       rotationAngle - 90f < 0.01f && rotationAngle - 90f > -0.01f))
            {
                moveIntent = Vector3.zero;
                animation.SetBool(Moving, false);
            }
            else
            {
                moveIntent = Vector3.forward;
                transform.eulerAngles = new Vector3(0f, cameraTarget.transform.eulerAngles.y + rotationAngle, 0f);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            animation.SetTrigger(Attack);
        }

        if (animation.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            moveIntent = Vector3.zero;
            transform.eulerAngles = tmpEuler;
            attackCollider.enabled = true;
        }
        else
        {
            attackCollider.enabled = false;
        }

        moveIntent = moveIntent.normalized;
        
        //transform.Rotate(0f,RotationIntent * Time.deltaTime * RotationSpeed,0f);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
            animation.SetBool(Running, true);
        }
        else
        {
            animation.SetBool(Running, false);
        }
        
        transform.position += transform.rotation * moveIntent * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 10 && attackCollider.enabled && !other.GetComponent<wolfScript>().hasBeenHit)
        {
            other.GetComponent<wolfScript>().hasBeenHit = true;
            other.GetComponent<wolfScript>().TakeDamage(1);
        }
        else if (other.gameObject.layer == 12 && attackCollider.enabled && !other.GetComponent<bossScript>().hasBeenHit)
        {
            other.GetComponent<bossScript>().hasBeenHit = true;
            other.GetComponent<bossScript>().TakeDamage(1);
        }
    }

   
    
    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        GetComponent<hPDisplayScript>().ChangeHitPoints(hitPoints);
        if (hitPoints <= 0)
        {
            FindObjectOfType<GameManager>().EndGame();
        } 
    }

    public bool Attacking()
    {
        return attackCollider.enabled;
    }

    public void stopMoving()
    {
        animation.SetBool(Moving, false);
    }
}
