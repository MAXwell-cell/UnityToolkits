using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgePool : MonoBehaviour
{
    private Stack<GameObject> Bridgepool;
    [HideInInspector]public GameObject BridgePrefab;
    private int bridgepoolMaxSize = 150;
    public void initBridgePool()
    {
        //初始化栈的长度以防止频繁的内存分配
        Bridgepool = new Stack<GameObject>(bridgepoolMaxSize);
        for (int i = 0; i < bridgepoolMaxSize; i++)
        {
            GameObject _obj = Instantiate(BridgePrefab);
            _obj.SetActive(false);
            Bridgepool.Push(_obj);
        }
    }
    public GameObject GetBridge()
    {
        Boolean _havebridge = Bridgepool.TryPop(out GameObject _result);
        if (_havebridge)
        {
            return _result;
        }
        else
        {
            GameObject _obj = Instantiate(BridgePrefab);
            return _obj;
        }
    }
    public void ReturnBridge(GameObject _bridge)
    {
        _bridge.SetActive(false);
        Bridgepool.Push(_bridge);
    }
}
