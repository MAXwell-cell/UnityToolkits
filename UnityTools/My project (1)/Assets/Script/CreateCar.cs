using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class CreateCar : MonoBehaviour
{
    public GameObject car1;
    public GameObject car2;
    public GameObject car3;
    public GameObject otherobject;
    private GameObject selfobject;
    public List<GameObject> carlist = new List<GameObject> { };
    private float Gridlength;
    // Start is called before the first frame update
    void Start()
    {
        selfobject = gameObject;
        CarMapCreate carmap = new CarMapCreate();
        List<Data> mapdata = carmap.Main();
        Gridlength = 0.4f;

        for (int i = 0; i < mapdata.Count; i++)
        {
            dataCreateCar(i, mapdata);
        }

        foreach (GameObject car in carlist)
        {
            car.transform.SetParent(otherobject.transform);
        }
    }
    private void dataCreateCar(int i, List<Data> datalist)
    {
        if (datalist[i].ishead)
        {
            GameObject car = null;
            switch (datalist[i].car_length)
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
            float x = 0f;
            float z = 0f;
            try
            {
                x = datalist[i - 1].car_pos.x + ((Gridlength * datalist[i].car_length) + 1.2f);
                z = datalist[i - 1].car_pos.z + ((Gridlength * datalist[i].car_length) + 1.2f);
            }
            catch (IndexOutOfRangeException)
            {
                x = (Gridlength * datalist[i].car_length) + 1.2f;
                z = (Gridlength * datalist[i].car_length) + 1.2f;
            }
            carlist.Add(car);
            switch (datalist[i].car_lookat)
            {
                case 1:
                    car.transform.Rotate(0, 90, 0);
                    car.transform.position = datalist[i].car_pos = new UnityEngine.Vector3(x, 0, z);
                    break;
                case 2:
                    car.transform.Rotate(0, 180, 0);
                    car.transform.position = datalist[i].car_pos = new UnityEngine.Vector3(x, 0, z);
                    break;
                case 3:
                    car.transform.Rotate(0, -90, 0);
                    car.transform.position = datalist[i].car_pos = new UnityEngine.Vector3(x, 0, z);
                    break;
                case 4:
                    car.transform.Rotate(0, 0, 0);
                    car.transform.position = datalist[i].car_pos = new UnityEngine.Vector3(x, 0, z);
                    break;
                default:
                    Debug.Log("汽车朝向数据有错误");
                    break;
            }
            // switch (datalist[i, j].car_lookat)
            // {
            //     case 1:
            //         car.transform.Rotate(0, 90, 0);
            //         if (datalist[i, j].car_length > 1)
            //         {
            //             car.transform.position = new UnityEngine.Vector3(x - ((datalist[i, j].car_length - 1f) / 2), 0, z);
            //         }
            //         else
            //         {
            //             car.transform.position = new UnityEngine.Vector3(x, 0, z);
            //         }
            //         break;
            //     case 2:
            //         car.transform.Rotate(0, 180, 0);
            //         if (datalist[i, j].car_length > 1)
            //         {
            //             car.transform.position = new UnityEngine.Vector3(x, 0, z + ((datalist[i, j].car_length - 1f) / 2));
            //         }
            //         else
            //         {
            //             car.transform.position = new UnityEngine.Vector3(x, 0, z);
            //         }
            //         break;
            //     case 3:
            //         car.transform.Rotate(0, -90, 0);
            //         if (datalist[i, j].car_length > 1)
            //         {
            //             car.transform.position = new UnityEngine.Vector3(x + ((datalist[i, j].car_length - 1f) / 2), 0, z);
            //         }
            //         else
            //         {
            //             car.transform.position = new UnityEngine.Vector3(x, 0, z);
            //         }
            //         break;
            //     case 4:
            //         car.transform.Rotate(0, 0, 0);
            //         if (datalist[i, j].car_length > 1)
            //         {
            //             car.transform.position = new UnityEngine.Vector3(x, 0, z - ((datalist[i, j].car_length - 1f) / 2));
            //         }
            //         else
            //         {
            //             car.transform.position = new UnityEngine.Vector3(x, 0, z);
            //         }
            //         break;
            //     default:
            //         Debug.Log("汽车朝向数据有错误");
            //         break;
            // }
        }
    }
}
