using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    Vector3 offset;
    public float lerpCent = 0.3f;

    void Start()
    {
        offset = transform.position - target.position;    
    }

    void FixedUpdate()
    {
        Vector3 to = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, to, lerpCent);
    }
}
