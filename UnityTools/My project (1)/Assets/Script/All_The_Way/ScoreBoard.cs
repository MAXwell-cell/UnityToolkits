using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : RoadBlock
{
    /// <summary>
    /// 其他计分板
    /// </summary>
    /// <param name="collision"></param>
    [SerializeField] private GameObject otherBlock;
    [SerializeField] private GameObject showNumber;
    public void Start()
    {
        //显示数字
        showNumber.GetComponent<TextMeshPro>().text = operation + scorenum.ToString();
    }
    public override void DoTriggerEnter()
    {
        base.DoTriggerEnter();
        otherBlock.transform.GetComponent<Collider>().enabled = false;
        Destroy(gameObject);
    }
}
