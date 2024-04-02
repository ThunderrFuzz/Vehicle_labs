using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target == null) return; // Ensure target is assigned before following

        Vector3 desiredPosition = target.position + offset;
        transform.position = desiredPosition;
        transform.LookAt(target.position + Vector3.up);
    }
}
