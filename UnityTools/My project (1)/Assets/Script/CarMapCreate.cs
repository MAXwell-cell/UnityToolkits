using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class CarMapCreate
{
    public int height = 15;
    public int width = 15;
    public List<Data> datalist1 = new List<Data> { };
    public Data[,] Main()
    {
        CarMapCreate data = new CarMapCreate();
        Data[,] datalist = new Data[data.height, data.width];
        data.create_data_array(datalist);
        data.SpiralTraverse(datalist);

        data.console_data_ishead_array(datalist);
        Console.WriteLine();
        data.console_data_car_length_array(datalist);
        Console.WriteLine();
        data.console_data_car_lookat_array(datalist);

        return datalist;
    }
    //螺旋遍历数组
    public void SpiralTraverse(Data[,] datalist)
    {
        if (datalist == null || datalist.GetLength(0) == 0 || datalist.GetLength(1) == 0)
        {
            UnityEngine.Debug.Log("datalist is empty or null");
            return;
        }
        int rows = datalist.GetLength(0);
        int cols = datalist.GetLength(1);
        int top = 0, bottom = rows - 1;
        int left = 0, right = cols - 1;
        while (top <= bottom && left <= right)
        {
            // Traverse from left to right along the top row
            for (int i = left; i <= right; i++)
            {
                processdata(top, i, datalist);
            }
            top++;

            // Traverse from top to bottom along the right column
            for (int i = top; i <= bottom; i++)
            {
                processdata(i, right, datalist);
            }
            right--;

            if (top <= bottom)
            {
                // Traverse from right to left along the bottom row
                for (int i = right; i >= left; i--)
                {
                    processdata(bottom, i, datalist);
                }
                bottom--;
            }

            if (left <= right)
            {
                // Traverse from bottom to top along the left column
                for (int i = bottom; i >= top; i--)
                {
                    processdata(i, left, datalist);
                }
                left++;
            }
        }
    }
    //
    public void processdata(int x, int y, Data[,] datalist)
    {
        if (datalist[x, y] == null)
        {
            Console.WriteLine("data is null");
            return;
        }
        else
        {
            if (datalist[x, y].havedata)
            {
                return;
            }
            else if (!datalist[x, y].havedata)
            {
                //该点设为车头
                datalist[x, y].ishead = true;
                datalist[x, y].havedata = true;
                //正式数据处理
                System.Random random = new System.Random();
                List<int> carlength = new List<int> { 1, 2, 3, };

                int[] carlookat2 = checkspace(2, x, y, datalist);
                int[] carlookat3 = checkspace(3, x, y, datalist);
                //确认是否可以生成某个长度的车辆
                if (carlookat2.Length == 0)
                {
                    carlength.Remove(2);
                }
                if (carlookat3.Length == 0)
                {
                    carlength.Remove(3);
                }
                //获得车的长度
                int carlengthnumber = carlength[random.Next(0, carlength.Count)];
                //获得车的朝向
                int carlookat = 0;
                if (carlengthnumber == 1)
                {
                    carlookat = random.Next(1, 4 + 1);
                }
                if (carlengthnumber == 2)
                {
                    carlookat = carlookat2[random.Next(0, carlookat2.Length)];
                }
                if (carlengthnumber == 3)
                {
                    carlookat = carlookat3[random.Next(0, carlookat3.Length)];
                }
                datalist[x, y].car_length = carlengthnumber;
                datalist[x, y].car_lookat = carlookat;
                for (int q = 1; q < carlengthnumber; q++)
                {
                    if (carlookat == 1)
                    {
                        datalist[x - q, y].havedata = true;
                    }
                    if (carlookat == 2)
                    {
                        datalist[x, y + q].havedata = true;

                    }
                    if (carlookat == 3)
                    {
                        datalist[x + q, y].havedata = true;
                    }
                    if (carlookat == 4)
                    {
                        datalist[x, y - q].havedata = true;
                    }
                }
            }
            datalist1.Add(datalist[x,y]);
        }

    }
    public Data? GetElement(int x, int y, Data[,] datalist)
    {
        if (x >= 0 && x < datalist.GetLength(0) && y >= 0 && y < datalist.GetLength(1) && !datalist[x, y].havedata)
        {
            return datalist[x, y];
        }
        return null;
    }
    public int[] checkspace(int carlength, int x, int y, Data[,] datalist)
    {
        int[] carlookat = new int[] { 1, 2, 3, 4 };
        List<int> carlooklist = new List<int>(carlookat);
        for (int i = 1; i < carlength; i++)
        {
            if (carlooklist.Contains(1))
            {
                Data? top = GetElement(x - i, y, datalist);
                if (top == null)
                {
                    carlooklist.Remove(1);
                }
            }
            if (carlooklist.Contains(2))
            {
                Data? right = GetElement(x, y + i, datalist);
                if (right == null)
                {
                    carlooklist.Remove(2);
                }
            }
            if (carlooklist.Contains(3))
            {
                Data? bottom = GetElement(x + i, y, datalist);
                if (bottom == null)
                {
                    carlooklist.Remove(3);
                }
            }
            if (carlooklist.Contains(4))
            {
                Data? left = GetElement(x, y - i, datalist);
                if (left == null)
                {
                    carlooklist.Remove(4);
                }
            }
        }
        carlookat = carlooklist.ToArray();
        return carlookat;
    }
    public void create_data_array(Data[,] data)
    {
        for (int i = 0; i < data.GetLength(0); i++)
        {
            for (int j = 0; j < data.GetLength(1); j++)
            {
                data[i, j] = new Data(); // 或者根据需要填充 Data 实例
            }
        }
    }
    public void console_data_ishead_array(Data[,] data)
    {

        int z = 0;
        for (int i = 0; i < data.GetLength(0); i++)
        {
            for (int j = 0; j < data.GetLength(1); j++)
            {
                if (j == data.GetLength(1) - 1)
                {
                    UnityEngine.Debug.Log(data[i, j].ishead);
                }
                else
                {
                    UnityEngine.Debug.Log(data[i, j].ishead + " ");
                }
                z++;
            }
        }
        UnityEngine.Debug.Log(z);
    }
    public void console_data_car_length_array(Data[,] data)
    {
        UnityEngine.Debug.Log("汽车长度矩阵");
        int z = 0;
        for (int i = 0; i < data.GetLength(0); i++)
        {
            for (int j = 0; j < data.GetLength(1); j++)
            {
                if (j == data.GetLength(1) - 1)
                {
                    UnityEngine.Debug.Log(data[i, j].car_length);
                }
                else
                {
                    UnityEngine.Debug.Log(data[i, j].car_length + " ");
                }
                z++;
            }
        }
        UnityEngine.Debug.Log(z);
    }
    public void console_data_car_lookat_array(Data[,] data)
    {
        UnityEngine.Debug.Log("汽车朝向矩阵");
        int z = 0;
        for (int i = 0; i < data.GetLength(0); i++)
        {
            for (int j = 0; j < data.GetLength(1); j++)
            {
                if (j == data.GetLength(1) - 1)
                {
                    UnityEngine.Debug.Log(data[i, j].car_lookat);
                }
                else
                {
                    UnityEngine.Debug.Log(data[i, j].car_lookat + " ");
                }
                z++;
            }
        }
        UnityEngine.Debug.Log(z);
    }
}