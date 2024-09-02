using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class Bridging : RoadBlock
{
    public int bridgelength = 0;
    /// <summary>
    /// 记录当前桥铺到了第几个
    /// </summary>
    private int bridgei = 0;
    public GameObject carObj;
    //在多远的距离开始铺桥
    private float bridgedistance = 2f;

    public void Start()
    {
        scorenum = 1;
        operation = "-";
        carObj = GameObject.Find("Car");
        if (carObj == null)
        {
            Debug.Log("Car不存在!");
        }
    }
    public override void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Car" && !isTriggered)
        {
            isTriggered = true;
            carObj.GetComponent<PlayerCar>().dropingBridging = true;
            StartCoroutine(DropBridge(collision.gameObject.transform.position.x));
        }
    }
    IEnumerator DropBridge(float Positionx)
    {
        if (bridgelength <= 0)
        {
            Debug.LogWarning("bridgelength 初始值为负数或0,无法继续执行 DropBridge。");
            carObj.GetComponent<PlayerCar>().dropingBridging = false;
            yield break;
        }
        yield return new WaitForSeconds(0.1f);

        try
        {
            ATWDataGameManager awtGM = ATWDataGameManager.Instance;
            awtGM.BoardGrade = calculator(awtGM.boardGrade, scorenum, operation);
            //如果这次放桥动作结束后车上的桥不足则中断递归
            if (awtGM.boardGrade <= 0)
            {
                carObj.GetComponent<PlayerCar>().dropingBridging = false;
                yield break;
            }
            //这里将对象池中的对象拿来铺桥(记得回收)
            GameObject bridgeobj = carObj.GetComponent<PlayerCar>().bridgePool.GetBridge();
            bridgeobj.GetComponent<BoxCollider>().enabled = true;
            bridgeobj.transform.position = new Vector3(Positionx, 0.3f, bridgei * 1f + gameObject.transform.position.z + bridgedistance);
        }
        catch (Exception ex)
        {
            Debug.Log("铺桥方法出错" + ex.Message);
        }
        //因为在内部减少所以这里应该是1 因为执行完后还会执行一次
        if (bridgelength > 1)
        {
            bridgelength--;
            bridgei++;
            yield return StartCoroutine(DropBridge(Positionx));
        }
        else
        {
            //这里告诉汽车停止铺桥 可以拖动屏幕移动汽车了
            carObj.GetComponent<PlayerCar>().dropingBridging = false;
        }
    }
}
