using System;

public class Data
{
    /**汽车长度,1-3*/
    public int car_length;
    /**汽车类型,目前是1-8*/
    public int car_class;
    /**汽车朝向,1-4*/
    public int car_lookat;
    /**汽车位置*/
    public UnityEngine.Vector3 car_pos;
    /**汽车旋转方向*/
    public float car_angle;
    /**是车头*/
    public Boolean ishead = false;
    public Boolean havedata = false;
}