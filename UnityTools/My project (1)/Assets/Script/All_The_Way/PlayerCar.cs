using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    private float speed = 5;
    private Vector3 distance;
    public TextMeshProUGUI scoreBoard;
    // Start is called before the first frame update
    void Start()
    {
        distance = new Vector3(0, 0, 1);
        if (GetComponent<Rigidbody>())
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        }
        ATWDataGameManager.Instance.OnboardGradeChanged += HandleBoardGradeChanged;
    }

    // Update is called once per framse
    void Update()
    {
        gameObject.transform.Translate(distance * Time.deltaTime * speed, Space.World);
    }
    private void HandleBoardGradeChanged(int newGrade)
    {
        scoreBoard.text = "X" + newGrade.ToString();
    }
}
