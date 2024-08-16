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
    // Start is called before the first frame update
    void Start()
    {
        selfobject = gameObject;
        CarMapCreate carmap = new CarMapCreate();
        Data[,] mapdata = carmap.Main();


        int rows = mapdata.GetLength(0);
        int cols = mapdata.GetLength(1);
        int top = 0, bottom = rows - 1;
        int left = 0, right = cols - 1;
        while (top <= bottom && left <= right)
        {
            // Traverse from left to right along the top row
            for (int i = left; i <= right; i++)
            {
                dataCreateCar(top, i, mapdata);
            }
            top++;

            // Traverse from top to bottom along the right column
            for (int i = top; i <= bottom; i++)
            {
                dataCreateCar(i, right, mapdata);
            }
            right--;

            if (top <= bottom)
            {
                // Traverse from right to left along the bottom row
                for (int i = right; i >= left; i--)
                {
                    dataCreateCar(bottom, i, mapdata);
                }
                bottom--;
            }

            if (left <= right)
            {
                // Traverse from bottom to top along the left column
                for (int i = bottom; i >= top; i--)
                {
                    dataCreateCar(i, left, mapdata);
                }
                left++;
            }
        }
        selfobject.transform.Rotate(0, 45, 0);
        selfobject.transform.position = new UnityEngine.Vector3(0, 0, -20);
        foreach (GameObject car in carlist)
        {
            car.transform.SetParent(otherobject.transform);
        }
    }
    private void dataCreateCar(int i, int j, Data[,] datalist)
    {
        if (datalist[i, j].ishead)
        {
            GameObject car = null;
            switch (datalist[i, j].car_length)
            {
                case 1:
                    car = Instantiate(car1, selfobject.transform);
                    break;
                case 2:
                    car = Instantiate(car2, selfobject.transform);
                    break;
                case 3:
                    car = Instantiate(car3, selfobject.transform);
                    break;
                default:
                    Debug.Log("汽车长度数据有错误");
                    break;
            }
            carlist.Add(car);
            switch (datalist[i, j].car_lookat)
            {
                case 1:
                    car.transform.Rotate(0, 90, 0);
                    break;
                case 2:
                    car.transform.Rotate(0, 180, 0);
                    break;
                case 3:
                    car.transform.Rotate(0, -90, 0);
                    break;
                case 4:
                    car.transform.Rotate(0, 0, 0);
                    break;
                default:
                    Debug.Log("汽车朝向数据有错误");
                    break;
            }
            float x = (float)(i * 1.1);
            float z = (float)(j * 1.1);
            car.transform.position = new UnityEngine.Vector3(x, 0, z);
            
        }
    }
}
