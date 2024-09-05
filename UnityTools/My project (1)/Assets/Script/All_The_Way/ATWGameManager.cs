using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATWGameManager : MonoBehaviour
{
    private static ATWGameManager instance;
    public static ATWGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                //动态创建动态挂载
                //创建空物体
                GameObject obj = new GameObject();
                //将对象名称改为脚本类名
                obj.name = typeof(ATWGameManager).ToString();
                //动态挂载对应的单例模式脚本
                instance = obj.AddComponent<ATWGameManager>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

    public void Start()
    {

    }
}
