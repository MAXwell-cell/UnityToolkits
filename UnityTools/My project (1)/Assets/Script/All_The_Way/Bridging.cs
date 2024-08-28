using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Bridging : RoadBlock
{
    public int bridgelength = 0;
    //发送铺桥事件 携带本次铺桥所用板子数量
    public event Action<int> isbridging;
    public void Start()
    {
        scorenum = 1;
        operation = "-";
    }
    public override void DoTriggerEnter()
    {
        StartCoroutine(DropBridge());
    }
    IEnumerator DropBridge()
    {
        if (bridgelength < 0)
        {
            Debug.LogWarning("bridgelength 初始值为负数，无法继续执行 DropBridge。");
            yield break;
        }
        yield return new WaitForSeconds(0.5f);

        try
        {
            ATWDataGameManager awtGM = ATWDataGameManager.Instance;
            awtGM.BoardGrade = calculator(awtGM.boardGrade, scorenum, operation);
            Debug.Log("当前分数为" + awtGM.boardGrade);
        }
        catch (Exception ex)
        {
            Debug.Log("铺桥方法出错" + ex.Message);
        }
        //因为在内部减少所以这里应该是1 因为执行完后还会执行一次
        if (bridgelength > 1)
        {
            bridgelength--;
            yield return StartCoroutine(DropBridge());
        }
    }
}
