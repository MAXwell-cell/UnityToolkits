using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    public float speed;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 distance = new Vector3(0,0,0);
        direction = gameObject.transform.forward;
    }

    // Update is called once per framse
    void Update()
    {
        gameObject.transform.Translate(direction.normalized*Time.deltaTime*speed);
    }
}
