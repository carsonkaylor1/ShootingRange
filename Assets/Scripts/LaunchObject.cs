using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LaunchObject : MonoBehaviour
{
    public float forceX = -90;
    public float forceY = 200;
    public float forceZ = -90;

    Rigidbody rigid;
    public GameObject fence;

    public Renderer rend;
    private int totalBallCount;
    private BulletScript bulletScript;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        gameObject.transform.position = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        rigid.useGravity = false;
        rend = GetComponent<Renderer>();
        rend.enabled = false;
        
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            rend.enabled = true;
            rigid.AddRelativeForce(forceX, forceY, forceZ);
            rigid.useGravity = true;
        }
        
    }
}
