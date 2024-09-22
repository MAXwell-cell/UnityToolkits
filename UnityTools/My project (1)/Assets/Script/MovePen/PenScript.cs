using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenScript : MonoBehaviour
{
    [HideInInspector] public bool ischecked = false;
    public int penColor;
    private void Start()
    {
        MovePenGameManager.Instance.checkPenCapColor += checkPenCapColor;
    }
    private void checkPenCapColor()
    {
        if (penColor == gameObject.transform.parent.transform.Find("pencap").GetComponent<PenCap>().capColorNumber)
        {
            MovePenGameManager.Instance.Accuracy++;
            Debug.Log(MovePenGameManager.Instance.Accuracy);
        }
    }
}
