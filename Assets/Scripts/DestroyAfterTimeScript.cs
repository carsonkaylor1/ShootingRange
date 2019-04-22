using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTimeScript : MonoBehaviour
{
    public float destroyAfterTime = 30f;
    public float destroyAfterTimeRandomization = 0f;
    private float countToTime;

    private void Awake()
    {
        destroyAfterTime += Random.value * destroyAfterTimeRandomization;
    }

    void Update()
    {
        countToTime += Time.deltaTime;
        if(countToTime >= destroyAfterTime)
        {
            Destroy(gameObject);
        }
    }
}
