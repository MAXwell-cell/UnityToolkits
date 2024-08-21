using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class object_Forward : MonoBehaviour
{
    private Vector3 circle_center;
    public GameObject circle_center_point;
    public GameObject start_point;
    public Vector3 start_point_position;
    public int segments;
    void Start()
    {
        circle_center = circle_center_point.transform.position;
        start_point_position = take_Next_Point(start_point_position, circle_center);
    }
    private Vector3 take_Next_Point(Vector3 _start_point, Vector3 _circle_center)
    {
        float angle = 45f;
        Vector3 direction = start_point_position - circle_center;
        Vector3 normal = new Vector3(0, 1, 0).normalized;
        Quaternion rotation = Quaternion.AngleAxis(angle, normal);
        Vector3 _target_direction = rotation * direction;
        Vector3 _target_point = _circle_center + _target_direction;
        return _target_point;
    }
}
