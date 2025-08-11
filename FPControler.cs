using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPControler : MonoBehaviour
{
    float speed = 0.1f;
    Rigidbody rb;
    CapsuleCollider capsule;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb = this.GetComponent<Rigidbody>();
        capsule = this.GetComponent<CapsuleCollider>();

    }
void FixedUpdate()
    {
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
