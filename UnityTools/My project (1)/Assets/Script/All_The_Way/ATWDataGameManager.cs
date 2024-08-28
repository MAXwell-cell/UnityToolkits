using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATWDataGameManager
{
    private static ATWDataGameManager instance;
    public static ATWDataGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ATWDataGameManager();
            }
            return instance;
        }
    }
    public int boardGrade = 0;
    private ATWDataGameManager() { }
    public event Action<int> OnboardGradeChanged;
    public int BoardGrade
    {
        get { return boardGrade; }
        set
        {
            if (boardGrade != value && value >= 0)
            {
                boardGrade = value;
                OnboardGradeChanged?.Invoke(boardGrade);
            }
            else if (value < 0)
            {
                boardGrade = 0;
                OnboardGradeChanged?.Invoke(boardGrade);
            }
        }
    }
}
