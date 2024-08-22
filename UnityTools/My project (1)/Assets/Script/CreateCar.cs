
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


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
        Data[,] mapdata = carmap.Main();
        Gridlength = 1.1f;

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
        foreach (GameObject car in carlist)
        {
            car.transform.SetParent(selfobject.transform);
        }
    }
    private void dataCreateCar(int i, int j, Data[,] datalist)
    {
        if (datalist[i, j].ishead)
        {
            GameObject car = null;
            switch (datalist[i, j].car_length)
            {
                case 5:
                    car = Instantiate(car1);
                    break;
                case 6:
                    car = Instantiate(car2);
                    break;
                case 7:
                    car = Instantiate(car3);
                    break;
                default:
                    Debug.Log("汽车长度数据有错误");
                    break;
            }
            float x = i * Gridlength;
            float z = j * Gridlength;
            carlist.Add(car);
            switch (datalist[i, j].car_lookat)
            {
                case 1:
                    car.transform.Rotate(0, 90, 0);
                    // car.transform.Rotate(0, 135, 0);
                    if (datalist[i, j].car_length > 1)
                    {
                        car.transform.position = new Vector3(x - ((datalist[i, j].car_length * Gridlength) / 2), 0, z);
                        // car.transform.position = take_Next_Point(new Vector3(x - ((datalist[i, j].car_length - 1f) / 2), 0, z), selfobject.transform.position);
                    }
                    else
                    {
                        car.transform.position = new Vector3(x, 0, z);
                        // car.transform.position = take_Next_Point(new Vector3(x, 0, z), selfobject.transform.position);
                    }
                    break;
                case 2:
                    car.transform.Rotate(0, 180, 0);
                    // car.transform.Rotate(0, 225, 0);
                    if (datalist[i, j].car_length > 1)
                    {
                        car.transform.position = new Vector3(x, 0, z + ((datalist[i, j].car_length * Gridlength) / 2));
                        // car.transform.position = take_Next_Point(new Vector3(x, 0, z + ((datalist[i, j].car_length - 1f) / 2)), selfobject.transform.position);
                    }
                    else
                    {
                        car.transform.position = new Vector3(x, 0, z);
                        // car.transform.position = take_Next_Point(new Vector3(x, 0, z), selfobject.transform.position);
                    }
                    break;
                case 3:
                    car.transform.Rotate(0, -90, 0);
                    // car.transform.Rotate(0, -45, 0);
                    if (datalist[i, j].car_length > 1)
                    {
                        car.transform.position = new Vector3(x + ((datalist[i, j].car_length * Gridlength) / 2), 0, z);
                        // car.transform.position = take_Next_Point(new Vector3(x + ((datalist[i, j].car_length - 1f) / 2), 0, z), selfobject.transform.position);
                    }
                    else
                    {
                        car.transform.position = new Vector3(x, 0, z);
                        // car.transform.position = take_Next_Point(new Vector3(x, 0, z), selfobject.transform.position);
                    }
                    break;
                case 4:
                    car.transform.Rotate(0, 0, 0);
                    // car.transform.Rotate(0, 45, 0);
                    if (datalist[i, j].car_length > 1)
                    {
                        car.transform.position = new Vector3(x, 0, z - ((datalist[i, j].car_length * Gridlength) / 2));
                        // car.transform.position = take_Next_Point(new Vector3(x, 0, z - ((datalist[i, j].car_length - 1f) / 2)), selfobject.transform.position);
                    }
                    else
                    {
                        car.transform.position = new Vector3(x, 0, z);
                        // car.transform.position = take_Next_Point(new Vector3(x, 0, z), selfobject.transform.position);
                    }
                    break;
                default:
                    Debug.Log("汽车朝向数据有错误");
                    break;
            }
        }
    }
    private Vector3 take_Next_Point(Vector3 _start_point, Vector3 _circle_center)
    {
        float angle = 45f;
        Vector3 direction = _start_point - _circle_center;
        Vector3 normal = new Vector3(0, 1, 0).normalized;
        Quaternion rotation = Quaternion.AngleAxis(angle, normal);
        Vector3 _target_direction = rotation * direction;
        Vector3 _target_point = _circle_center + _target_direction;
        return _target_point;
    }
}
