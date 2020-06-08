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
    private static readonly int sort1 = Animator.StringToHash("sort1");
    private static readonly int sort2 = Animator.StringToHash("sort2");
    private static readonly int esquiveAvant = Animator.StringToHash("esquiveAvant");
    private static readonly int esquiveArriere = Animator.StringToHash("esquiveArriere");
    private static readonly int esquiveDroite = Animator.StringToHash("esquiveDroite");
    private static readonly int esquiveGauche = Animator.StringToHash("esquiveGauche");
    private static readonly int arise = Animator.StringToHash("arise");
    
    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.gameOver == true)
        {
            return;
        }
        else
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        MoveDuringDash();

        var moveIntent = Vector3.zero;
        var moveSpeed = walkSpeed;
        var rotationAngle = 0f;
        var tmpEuler = transform.eulerAngles;
        short keysCount = 0;
        float RotationIntent = 0f;
        
        if (Input.GetKey(KeyCode.Space))
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                animation.SetTrigger(esquiveAvant);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                animation.SetTrigger(esquiveArriere);
                
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                animation.SetTrigger(esquiveDroite);
            }

            if(Input.GetKeyDown(KeyCode.Q))
            {
                animation.SetTrigger(esquiveGauche);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Z))
            {
                animation.SetBool(Moving,true);
                rotationAngle += Input.GetKey(KeyCode.Q) ? 360f : 0f;
                keysCount++;
            }

            if (Input.GetKey(KeyCode.S))
            {
                animation.SetBool(Moving,true);
                
                rotationAngle += 180f;
                keysCount++;
            }
        
            if (Input.GetKey(KeyCode.Q) & !animation.GetCurrentAnimatorStateInfo(0).IsName("sort2") &
                !animation.GetCurrentAnimatorStateInfo(0).IsName("sort1"))
            {
                animation.SetBool(Moving,true);
                /*transform.eulerAngles = new Vector3(0f, cameraTarget.transform.eulerAngles.y - 90f, 0f);
                moveIntent += Vector3.forward;*/
                rotationAngle += 270f;
                keysCount++;
            }
        
            if (Input.GetKey(KeyCode.D) & !animation.GetCurrentAnimatorStateInfo(0).IsName("sort2") &
                !animation.GetCurrentAnimatorStateInfo(0).IsName("sort1"))
            {
                animation.SetBool(Moving,true);
                /*transform.eulerAngles = new Vector3(0f, cameraTarget.transform.eulerAngles.y + 90f, 0f);
                moveIntent += Vector3.forward;*/
                rotationAngle += 90f;
                keysCount++;
            }
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                animation.SetTrigger(sort1);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                animation.SetTrigger(sort2);
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                animation.SetTrigger(Attack);
            }
            if (animation.GetCurrentAnimatorStateInfo(0).IsName("Attack") || 
                animation.GetCurrentAnimatorStateInfo(0).IsName("sort1") || 
                animation.GetCurrentAnimatorStateInfo(0).IsName("sort2"))
            {
                moveIntent = Vector3.zero;
                transform.eulerAngles = tmpEuler;
                attackCollider.enabled = true;
            }
            else
            {
                attackCollider.enabled = false;
            }
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

        
        
        
        /*
        if (Input.GetKey(KeyCode.S))
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                animation.SetTrigger(esquiveArriere);
            }
        }
        */
        /*
        if (animation.GetCurrentAnimatorStateInfo(0).IsName("sort1"))
        {
            moveIntent = Vector3.zero;
            transform.eulerAngles = tmpEuler;
            attackCollider.enabled = true;
        }
        else
        {
            attackCollider.enabled = false;
        }
*/
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
        if(animation.GetCurrentAnimatorStateInfo(0).IsName("sort2"))
        {
            transform.position += transform.rotation * moveIntent * moveSpeed/2 * Time.deltaTime;
        }
        else if(!animation.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            transform.position += transform.rotation * moveIntent * moveSpeed * Time.deltaTime;
        }
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

    public bool Attacking()
    {
        return attackCollider.enabled;
    }

    public void stopMoving()
    {
        animation.SetBool(Moving, false);
    }

    public void animationReanimation()
    {
        animation.SetTrigger(arise);
    }

    public void MoveDuringDash()
    {
        float factor = 80;
        if (animation.GetCurrentAnimatorStateInfo(0).IsName("esquiveAvant"))
        {
            transform.position += transform.rotation * Vector3.forward * Time.deltaTime * factor;
        }
        else if (animation.GetCurrentAnimatorStateInfo(0).IsName("esquiveArriere"))
        {
            transform.position += transform.rotation * Vector3.back * Time.deltaTime * factor;
        }
        else if (animation.GetCurrentAnimatorStateInfo(0).IsName("esquiveDroite"))
        {
            if (animation.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.6)
            {
                transform.position += transform.rotation * Vector3.right * Time.deltaTime * factor;
            }
        }
        else if (animation.GetCurrentAnimatorStateInfo(0).IsName("esquiveGauche"))
        {
            if (animation.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.6)
            {
                transform.position += transform.rotation * Vector3.left * Time.deltaTime * factor;
            }
        }
        else if (animation.GetCurrentAnimatorStateInfo(0).IsName("sort1"))
        {
            if (animation.GetCurrentAnimatorStateInfo(0).normalizedTime >0.2 && 
                animation.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.6)
            {
                transform.position += transform.rotation * Vector3.forward * Time.deltaTime * 10;
            }
        }
    }
}
    
    