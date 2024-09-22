using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    private Rigidbody jumperRb;
    public float forceAmount;
    [SerializeField] private GameObject Timetable;
    //经书
    [SerializeField] private GameObject script;
    public bool isTiming = false;
    private void Start()
    {
        jumperRb = GetComponent<Rigidbody>();
    }
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "stick(Clone)")
        {
            jumperRb.velocity = Vector3.up * forceAmount;
        }
        if (collider.gameObject.name == "Scriptures")
        {
            script.SetActive(true);
            collider.gameObject.SetActive(false);
            Timetable.SetActive(true);
            isTiming = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.transform.position = new Vector3(6, 30, 15);
        }
    }
}
