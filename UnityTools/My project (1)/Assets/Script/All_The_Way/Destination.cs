using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : RoadBlock
{
    public GameObject winWindow;
    public void Start()
    {
        winWindow = GameObject.Find("WinWindow");
        winWindow.SetActive(false);
    }
    public override void DoTriggerEnter()
    {
        winWindow.SetActive(true);
        PlayerCar.carCanMove = false;
    }
}
