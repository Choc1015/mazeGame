using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    // ���� �Ҹ�
    // ȿ���� 
    // ���̵� 
    // �ְ� ����
    public static int bestPoint;
    // ���� ���� 
    public static int lastPoint;
    // ���� ���� 
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
