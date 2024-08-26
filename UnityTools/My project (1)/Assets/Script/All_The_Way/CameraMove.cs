using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private float speed = 5;
    private Vector3 distance;
    void Start()
    {
        distance = new Vector3(0, 0, 1);
        if (GetComponent<Rigidbody>())
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        }
    }
    void Update()
    {
        gameObject.transform.Translate(distance * Time.deltaTime * speed, Space.World);
    }
}
