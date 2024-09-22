using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchScripturesGameManage : MonoBehaviour
{
    public GameObject shooter;
    /// <summary>
    /// 子弹
    /// </summary>
    public GameObject bullet;
    private float elapsedTime = 0f;
    [SerializeField] private GameObject Scripturesobject;
    [SerializeField] private GameObject jumperMonk;
    [SerializeField] private Text takeTime;
    /// <summary>
    /// 唐僧的脚本对象
    /// </summary>
    private Jumper jumperScript;
    /// <summary>
    /// 限制点击事件判断条件
    /// </summary>
    private bool canclick = true;

    private void Start()
    {
        jumperScript = jumperMonk.GetComponent<Jumper>();


        Scripturesobject.SetActive(true);
        //重置计时器状态
        jumperScript.isTiming = true;
        elapsedTime = 0f;
        takeTime.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shoot();
        }
        if (jumperScript.isTiming)
        {
            elapsedTime += Time.deltaTime;
            takeTime.text = "用时" + elapsedTime.ToString("F2") + "秒" + "\n取经成功!";
        }
    }
    public void shoot()
    {
        Instantiate(bullet, shooter.transform.position, Quaternion.identity);
        shooter.transform.Translate(0, 3, 0);
        Camera.main.transform.Translate(0, 3, 0);
    }
}
