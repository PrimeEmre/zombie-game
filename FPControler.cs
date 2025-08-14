using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPControler : MonoBehaviour
{
    public GameObject cam;
    float speed = 0.1f;
    float Xsensitivity =  5;
    float Ysensitivity = 5;
    Rigidbody rb;
    CapsuleCollider capsule;

    Quaternion cameraRot;
    Quaternion charecterRot;

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

        this.transform.localRotation = charecterRot;
        cam.transform.localRotation = cameraRot;

        if(Input.GetKeyDown (KeyCode.Space) && Isgrounded())
        rb.AddForce(0, 300, 0);
       
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        transform.position += new Vector3(x, 0, z) * speed;
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

}
