using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementScript : MonoBehaviour
{
    public float walkAcceleration;
    public float walkDeceleration = 5;
    public GameObject cameraObject;
    private static float currYRotation;
    private MouseLook mouseLook;
    public float maxWalkSpeed = 20;
    public float jumpVelocity = 20;
    private bool grounded = false;
    public float maxSlope = 60;
    Vector2 horizontalMovement;
    Vector3 movement;

    Rigidbody rigid;

    public GameObject pigeon;
    public Text pigeonText;

    private void Start()
    {
        mouseLook = GetComponent<MouseLook>();
        rigid = GetComponent<Rigidbody>();
        
    }

    void Update()
    {

        horizontalMovement = new Vector2(rigid.velocity.x, rigid.velocity.z);
        if (horizontalMovement.magnitude > maxWalkSpeed)
        {
            horizontalMovement = horizontalMovement.normalized;
            horizontalMovement *= maxWalkSpeed;
        }

        movement = new Vector3(horizontalMovement.x, 0, horizontalMovement.y);
        rigid.velocity = movement;


        transform.rotation = Quaternion.Euler(0, cameraObject.GetComponent<MouseLook>().yRotation, 0);
        rigid.AddRelativeForce(Input.GetAxis("Horizontal") * walkAcceleration, 0, Input.GetAxis("Vertical") * walkAcceleration);

        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid.AddForce(0, jumpVelocity, 0);
        }
        */

    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "pigeon collider")
        {
            pigeonText.text = ("Press F to Launch Pigeons");
        }
    }
    */
    /*
    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Angle(contact.normal, Vector3.up) < maxSlope)
                grounded = true;
        }
    }
    
    }
    */

}
