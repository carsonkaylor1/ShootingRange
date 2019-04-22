using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //Most of this is reused from the in class example

    float velX, velY, velZ;
    float X, Y, Z;

    public float maxVel;
    public float jumpSpeed = 100.0f;
    public float mouseSpeed = 3;
    public static bool doubleJump = false;

    private bool touchGround = true;
    private bool canDoubleJump = false;

    Rigidbody rigid;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        X = Input.GetAxis("Mouse X") * mouseSpeed;
        Y = Input.GetAxis("Mouse Y") * mouseSpeed;
        Z = 0;

        transform.Rotate(0, X, 0);

        velX = Input.GetAxis("Vertical");
        velY = Input.GetAxis("Horizontal");

        
        
        Vector3 forwardVel = transform.forward * maxVel * velX;
        Vector3 horizontalVel = transform.right * maxVel * velY;
        Vector3 sumVel = forwardVel + horizontalVel;

        sumVel.y = rigid.velocity.y;
        rigid.velocity = sumVel;

        if (Input.GetButtonDown("Jump"))
        {
            if (touchGround)
            {
                rigid.AddForce(Vector3.up * jumpSpeed);
            }
            else if (canDoubleJump)
            {
                rigid.velocity = new Vector3(0, 0, 0);
                rigid.AddForce(Vector3.up * jumpSpeed);
                canDoubleJump = false;
            }
        }

        //rigid.velocity = sumVel;
       
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            touchGround = true;
            if (doubleJump)
            {
                canDoubleJump = true;
            }
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            touchGround = false;
        }
    }

}
