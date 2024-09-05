using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBridge : RoadBlock
{
    public override void DoTriggerEnter()
    {
        base.DoTriggerEnter();
        Destroy(gameObject);
    }
}
