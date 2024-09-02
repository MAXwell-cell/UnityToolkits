using System;
using UnityEngine;


public class RoadBlock : MonoBehaviour
{
    public int scorenum;
    public string operation;
    public bool isTriggered = false;
    public virtual void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Car" && !isTriggered)
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
        if (result == 0)
        {
            Debug.Log("result未赋值");
        }
        return result;
    }
    public virtual void DoTriggerEnter()
    {
        isTriggered = true;
        ATWDataGameManager awtGM = ATWDataGameManager.Instance;
        awtGM.BoardGrade = calculator(awtGM.boardGrade, scorenum, operation);
        Debug.Log("当前分数为" + awtGM.boardGrade);
    }
}
