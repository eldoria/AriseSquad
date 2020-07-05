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
    private float moveSpeed;
    private Camera cam;


    // Contrôleur mouvement/Rotation
    public CharacterController controller;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVel;

    private Vector3 PlayerPos;
    private Vector3 Direction;
    public float SphereRadius = 4;
    public LayerMask layerMask;
    public float MaxDistance;
    private float currentHitDistance;
    public GameObject currentHitObject;

    public Interactable focus;
    
    private bool hasHit = false;
    
    private float RotationSpeed;

    [SerializeField]private new Animator animation;
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

    [SerializeField]private GameObject objectWithScripts;
    private void Start()
    {
        objectWithScripts = GameObject.Find("Scripts_Map_boss");
        objectWithScripts.GetComponent<monstersFight>().AddAlly(gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.gameOver == true)
        {
            return;
        }
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;

        Vector3 pos = transform.position;

        if (pos.y > 0)
        {
            pos.y = 0;
            transform.position = pos;
        }
        
        // Get the key that player hit and move in consequence by the call of movePlayer function
        getKeyCode();
        // Move the player during dash
        MoveDuringDash();
        GetPotion();



    }

    private void OnTriggerStay(Collider other)
    {
        if (attackCollider.enabled && !hasHit)
        {
            if(other.CompareTag("wolfEnemy"))
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

    private void getKeyCode()
    {
        short keysCount = 0;
        //float RotationIntent = 0f;
        
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
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = runSpeed;
                animation.SetBool(Running, true);
            }
            else
            {
                moveSpeed = walkSpeed;
                animation.SetBool(Running, false);
            }
            
            if (Input.GetKey(KeyCode.Z))
            {
                animation.SetBool(Moving,true);
                keysCount++;
            }
            if (Input.GetKey(KeyCode.S))
            {
                animation.SetBool(Moving,true);
                keysCount++;
            }
            if (Input.GetKey(KeyCode.Q) & !animation.GetCurrentAnimatorStateInfo(0).IsName("sort2") &
                !animation.GetCurrentAnimatorStateInfo(0).IsName("sort1"))
            {
                animation.SetBool(Moving,true);
                keysCount++;
            }
            if (Input.GetKey(KeyCode.D) & !animation.GetCurrentAnimatorStateInfo(0).IsName("sort2") &
                !animation.GetCurrentAnimatorStateInfo(0).IsName("sort1"))
            {
                animation.SetBool(Moving,true);
                keysCount++;
            }
            
            if (keysCount == 0 || keysCount == 4)
            {
                animation.SetBool(Moving, false);
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
                RemoveFocus();
            }
            if (animation.GetCurrentAnimatorStateInfo(0).IsName("Attack") || 
                animation.GetCurrentAnimatorStateInfo(0).IsName("sort1") || 
                animation.GetCurrentAnimatorStateInfo(0).IsName("sort2"))
            {
                if (animation.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f &&
                    animation.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f)
                {
                    attackCollider.enabled = true;
                }
                else
                {
                    attackCollider.enabled = false;
                    hasHit = false;
                }
            }
            else
            {
                movePlayer();
            }
        }
    }

    private void movePlayer()
    {
        float MoveHorizontal = Input.GetAxisRaw("Horizontal");
        float MoveVertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(MoveHorizontal,0f,MoveVertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTarget.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f,angle,0f);

            Vector3 MoveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                
            controller.Move(MoveDir.normalized * moveSpeed * Time.deltaTime);
        }
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

    public void GetPotion()
    {
        PlayerPos = transform.position;
        Direction = transform.forward;
        RaycastHit hit;
        if (Physics.SphereCast(PlayerPos,SphereRadius,Direction, out hit,MaxDistance,layerMask, QueryTriggerInteraction.UseGlobal))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            currentHitObject = hit.transform.gameObject;
            currentHitDistance = hit.distance;
            //Debug.Log("hit" + hit.collider.name + " " + hit.point);
            if (interactable)
            {
                SetFocus(interactable);
            }
        }
        else
        {
            currentHitDistance = MaxDistance;
            currentHitObject = null;
            RemoveFocus();
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();
                
            focus = newFocus;
        }
        
        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();
        focus = null;
    }
   
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(PlayerPos ,PlayerPos + Direction * currentHitDistance);
        Gizmos.DrawWireSphere(PlayerPos + Direction * currentHitDistance ,SphereRadius);
    }
}
    
