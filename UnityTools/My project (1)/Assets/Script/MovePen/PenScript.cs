using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenScript : MonoBehaviour
{
    private bool ischecked = false;
    private int penColor;
    private void Start()
    {
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 从鼠标点击位置发射一条射线
            RaycastHit hit; // 用于存储射线碰撞的信息
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);
            // 如果射线碰撞到物体的 Collider
            if (Physics.Raycast(ray, out hit))
            {
                // 检查碰撞的物体是否是当前物体
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    if (!ischecked)
                    {
                        gameObject.transform.Translate(new Vector3(0, 2, 0), Space.Self);
                        ischecked = true;
                    }
                    else
                    {
                        gameObject.transform.Translate(new Vector3(0, -2, 0), Space.Self);
                        ischecked = false;
                    }

                }
            }
        }
    }
}
