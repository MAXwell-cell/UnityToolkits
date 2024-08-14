using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CreateCar : MonoBehaviour
{
    public GameObject car1;
    public GameObject car2;
    public GameObject car3;
    // Start is called before the first frame update
    void Start()
    {
        CarMapCreate carmap = new CarMapCreate();
        Data[,] mapdata = carmap.Main();
        for (int i = 0; i < mapdata.GetLength(0); i++)
        {
            for (int j = 0; j < mapdata.GetLength(1); j++)
            {
                if (mapdata[i, j].ishead)
                {
                    GameObject car = null;
                    switch (mapdata[i, j].car_length)
                    {
                        case 1:
                            car = Instantiate(car1);
                            break;
                        case 2:
                            car = Instantiate(car2);
                            break;
                        case 3:
                            car = Instantiate(car3);
                            break;
                        default:
                            Debug.Log("汽车长度数据有错误");
                            break;
                    }
                    switch (mapdata[i, j].car_lookat)
                    {
                        case 1:
                            car.transform.Rotate(0,0,-90);
                            break;
                        case 2:
                            car.transform.Rotate(0,0,0);
                            break;
                        case 3:
                            car.transform.Rotate(0,0,90);
                            break;
                        case 4:
                            car.transform.Rotate(0,0,180);
                            break;
                        default:
                            Debug.Log("汽车朝向数据有错误");
                            break;
                    }
                    float x = (float)(i*1.2);
                    float y = (float)(j*1.5);
                    car.transform.position = new UnityEngine.Vector3(x,-y,0);
                }
            }
        }
    }
}
