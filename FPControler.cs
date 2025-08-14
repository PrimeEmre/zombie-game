using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPControler : MonoBehaviour
{
    public GameObject cam;
    float speed = 0.1f;
    float Xsensitivity =  5;
    float Ysensitivity = 5;
    float MininumX = -90;
    float MininumY = 90;
    float MaxisimumX = 90;

    Rigidbody rb;
    CapsuleCollider capsule;

    Quaternion cameraRot;
    Quaternion charecterRot;

    bool cursorIsLocked = true;
    bool lockCursor = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        capsule = this.GetComponent<CapsuleCollider>();
        cameraRot = cam.transform.rotation;
        charecterRot = this.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
    }
void FixedUpdate()
    {
        float yRot = Input.GetAxis("Mouse X") *Ysensitivity;
        float xRot = Input.GetAxis("Mouse Y") *Xsensitivity;

        cameraRot *= Quaternion.Euler(-xRot, 0, 0);
        charecterRot *= Quaternion.Euler( 0, yRot, 0);
        cameraRot = ClampRotationAroundXAxis (cameraRot);
        this.transform.localRotation = charecterRot;
        cam.transform.localRotation = cameraRot;

        if(Input.GetKeyDown (KeyCode.Space) && Isgrounded())
        rb.AddForce(0, 300, 0);
       
        float x = Input.GetAxis("Horizontal") *speed;
        float z = Input.GetAxis("Vertical") *speed;
        transform.position += new Vector3(x, 0, z) * speed;
        transform.position += cam.transform.forward * z + cam.transform.right * x;
        UpdateCursorLock();
    }

    Quaternion ClampRotationAroundXAxis (Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w /= 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);
        angleX = Mathf.Clamp(angleX,MininumX,MaxisimumX);
        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);
        return q;
    }

    bool Isgrounded()
    {
        RaycastHit hitInfo;
        if(Physics.SphereCast(transform.position , capsule.radius , Vector3.down,  out hitInfo ,capsule.height/2f - capsule.radius + 0.1f))
        {
            return true;
        }
        return false;
    }
    public void SetCursorLock(bool value)
    {
        lockCursor = value;
        if (!lockCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    public void UpdateCursorLock()
{
    // If the lockCursor boolean is true, call the InternalLockUpdate method.
    if (lockCursor)
        InternalLockUpdate();   
}

    public void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            cursorIsLocked = false;
        else if (Input.GetMouseButton(0))
            cursorIsLocked = true;

        if (cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }else if (!cursorIsLocked) { 
        Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        
        }
    }


}
