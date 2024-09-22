using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Bullet : MonoBehaviour
{
    private bool bulletstop = false;
    [SerializeField] private GameObject fissure;
    [SerializeField] private float speed = 3f;
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Wall" && bulletstop == false)
        {
            bulletstop = true;
            fissure.SetActive(true);
        }
        //如果棒子碰到身体的碰撞盒
        if (collider.gameObject.name == "bodyCollider")
        {
            Debug.Log("失败");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (bulletstop == false)
        {
            gameObject.transform.Translate(speed * Time.deltaTime, 0, 0);
        }
    }
}
