using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public float rotationSpeed;
    public Transform target;
    private float mouseX, mouseY;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        mouseX += rotationSpeed * Input.GetAxis("Mouse X");
        mouseY -= rotationSpeed * Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, -60f, 30f);
        
        transform.LookAt(target);
        target.rotation = Quaternion.Euler(mouseY, mouseX, 0f);
    }
}
