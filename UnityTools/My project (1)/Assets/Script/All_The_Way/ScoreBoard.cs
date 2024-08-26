using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : RoadBlock
{
    public override void DoTriggerEnter()
    {
        ATWDataGameManager awtGM = ATWDataGameManager.Instance;
        awtGM.BoardGrade = calculator(awtGM.boardGrade,scorenum,operation);
        Debug.Log("当前分数为"+awtGM.boardGrade);
        Destroy(gameObject);
    }
}
