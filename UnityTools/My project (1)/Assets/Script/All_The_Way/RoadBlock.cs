using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RoadBlock : MonoBehaviour
{
    public int scorenum;
    public string operation;
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Car")
        {
            DoTriggerEnter();
        }
    }
    public int calculator(int currentscore, int num, string operation)
    {
        int result = 0;
        switch (operation)
        {
            case "+":
                result = currentscore + num;
                break;
            case "-":
                result = currentscore - num;
                break;
            case "x":
                result = currentscore * num;
                break;
            case "/":
                result = currentscore / num;
                break;
            default:
                Debug.Log("分数计算器出错");
                break;
        }
        if(result == 0){
            Debug.Log("result未赋值");
        }
        return result;
    }
    public virtual void DoTriggerEnter() { }
}
