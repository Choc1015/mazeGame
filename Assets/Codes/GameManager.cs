using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    // 음악 소리
    // 효과음 
    // 난이도 
    // 최고 점수
    public static int bestPoint;
    // 지난 점수 
    public static int lastPoint;
    // 들어온 점수 
    public static int inputPoint;

    private void Awake()
    {
        Instance = this;
        CheckingPoint();
    }

    private void Start()
    {
        
    }

    private void CheckingPoint()
    {
        lastPoint = inputPoint;
        if (bestPoint == 0)
        {
            bestPoint = inputPoint;
        }

        if (bestPoint < inputPoint)
        {
            bestPoint = inputPoint;
        }
    }
}
