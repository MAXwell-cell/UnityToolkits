using System;
using System.Collections.Generic;
using OpenCover.Framework.Model;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    private float speed = 5;
    private Vector3 distance;
    public TextMeshProUGUI scoreBoard;
    /// <summary>
    /// 桥的预制体
    /// </summary>
    public GameObject Bridge;
    [HideInInspector] public List<GameObject> Bridges = new List<GameObject> { };
    private Vector2 startTouchposition;
    private bool isDragging = false;
    /// <summary>
    /// 当前是否正在铺桥
    /// </summary>
    public bool dropingBridging = false;
    private float _time = 0f;
    /// <summary>
    /// 桥的对象池
    /// </summary>
    [HideInInspector] public BridgePool bridgePool;
    void Start()
    {
        distance = new Vector3(0, 0, 1);
        if (GetComponent<Rigidbody>())
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        }
        //注册事件监听
        ATWDataGameManager.Instance.OnboardGradeChanged += HandleBoardGradeChanged;
        //实例化桥的对象池
        bridgePool = gameObject.AddComponent<BridgePool>();
        bridgePool.BridgePrefab = Bridge;
        bridgePool.initBridgePool();
    }

    void Update()
    {
        gameObject.transform.Translate(distance * Time.deltaTime * speed, Space.World);
        //如果没在放桥那么可以触摸操作
        if (!dropingBridging)
        {
            //这之后的updata都是触摸操作
            if (Input.GetMouseButtonDown(0))
            {
                //记录鼠标按下时的位置
                startTouchposition = Input.mousePosition;
                isDragging = true;
            }
            if (Input.GetMouseButton(0) && isDragging)
            {
                _time += Time.deltaTime;
                if (_time > 0.01f)
                {
                    //获取鼠标当前位置
                    Vector2 currentposition = Input.mousePosition;
                    Vector2 direction = currentposition - startTouchposition;
                    if (direction.x > 0 && gameObject.transform.position.x < 3)
                    {
                        gameObject.transform.Translate(new Vector3(0.5f * Time.deltaTime * speed, 0, 0), Space.World);
                    }
                    if (direction.x < 0 && gameObject.transform.position.x > -3)
                    {
                        gameObject.transform.Translate(new Vector3(-0.5f * Time.deltaTime * speed, 0, 0), Space.World);
                    }
                    //更新初始位置为当前位置
                    startTouchposition = currentposition;
                    _time -= 0.01f;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }


            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    startTouchposition = Input.mousePosition;
                    isDragging = true;
                }
                if (touch.phase == TouchPhase.Moved && isDragging)
                {
                    _time += Time.deltaTime;
                    if (_time > 0.01f)
                    {
                        Vector2 currentposition = touch.position;
                        Vector2 direction = currentposition - startTouchposition;
                        if (direction.x > 0 && gameObject.transform.position.x < 3)
                        {
                            gameObject.transform.Translate(new Vector3(1f, 0, 0), Space.World);
                        }
                        if (direction.x < 0 && gameObject.transform.position.x > -3)
                        {
                            gameObject.transform.Translate(new Vector3(-1f, 0, 0), Space.World);
                        }
                        startTouchposition = currentposition;
                        Debug.Log("有手指触摸移动操作");
                        _time -= 0.01f;
                    }
                }
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    isDragging = false;
                }
            }
        }
    }

    private void HandleBoardGradeChanged(int newGrade)
    {
        scoreBoard.text = "X" + newGrade.ToString();

        //更改得分同时往车载桥梁数组添加元素（记得回收）;
        int bridgescount = Bridges.Count;
        int count = newGrade - bridgescount;
        if (count > 0)
        {
            for (int i = 0; i < Math.Abs(count); i++)
            {
                GameObject bridge = bridgePool.GetBridge();
                bridge.transform.parent = gameObject.transform;
                bridge.transform.rotation = Quaternion.identity;
                bridge.transform.position = new Vector3(0, (i + bridgescount) * 0.4f + 1f, gameObject.transform.position.z);
                Bridges.Add(bridge);
            }
        }
        else if (count < 0)
        {
            for (int i = 0; i < Math.Abs(count); i++)
            {
                bridgePool.ReturnBridge(Bridges[Bridges.Count - 1]);
                Bridges.RemoveAt(Bridges.Count - 1);
            }
        }
    }
}
