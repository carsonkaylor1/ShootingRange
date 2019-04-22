using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float maxDistance = 100000;
    public GameObject decalHitWall;
    public float inFrontOfWall = 0.01f;

    public GameObject fence;
    private int totalBallCount;
    public int userBallCount;

    private void Start()
    {
        userBallCount = 0;
        totalBallCount = GameObject.FindGameObjectsWithTag("cannon ball").Length;
    }

    void Update()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            if (decalHitWall && hit.transform.tag == "LevelParts")
            {
                Instantiate(decalHitWall, hit.point + (hit.normal * inFrontOfWall), Quaternion.LookRotation(hit.normal));  
            }
            if(decalHitWall && hit.transform.tag == "Target")
            {
                Destroy(hit.transform.gameObject);
            }
            if (decalHitWall && hit.transform.tag == "cannon ball")
            {
                Destroy(hit.transform.gameObject);
                userBallCount += 1;
            }
        }
        Destroy(gameObject);
    }
    public int getUserBallCount()
    {
        return userBallCount;
    }
}
