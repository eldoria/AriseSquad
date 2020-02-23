using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    public Transform cameraTarget;
    [SerializeField]private float walkSpeed = 8f;
    [SerializeField]private float runSpeed = 20f;
    
    private float RotationSpeed;

    [SerializeField]private Animator animation;
    private static readonly int Moving = Animator.StringToHash("moving");
    private static readonly int Running = Animator.StringToHash("running");

    // Update is called once per frame
    void Update()
    {
        var moveIntent = Vector3.zero;
        var moveSpeed = walkSpeed;
        var rotationAngle = 0f;
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

}
