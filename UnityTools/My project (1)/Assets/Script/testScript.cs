using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    public GameObject car1;
    public GameObject car2;
    public GameObject car3;
    // Start is called before the first frame update
    void Start()
    {
        GameObject testobject = Instantiate(car3);
        testobject.transform.position = new Vector3(0,0,0);
        testobject.transform.Rotate(0,0,90);
    }
    // rotatearound 父节点作为枢轴点 四元数旋转 smoothdampAngle
}
