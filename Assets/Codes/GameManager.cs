using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    // 음악 소리
    public Slider musicHandle;
    static public float musicValue;
    // 효과음 
    public Slider soundEffectHandle;
    static public float soundEffectValue;
    // 난이도 
    public Dropdown level;
    static public int movingPercent;
    static public int spikePercent;
    static public int pointPercent;
    static public int timePercent;
    static public int sec;
    // 맵 크기 
    public InputField inputgrid;
    static public int grid;
    public bool isOkGrid = false;
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


    private void Update()
    {
        if (inputgrid != null)
            CheckLevel();

        if (musicHandle != null)
            musicValue = musicHandle.value;

        if (soundEffectHandle != null)
            soundEffectValue = soundEffectHandle.value;
    }

    private void CheckLevel()
    {

        if (inputgrid.text == "")
            return;
        grid = Convert.ToInt32(inputgrid.text);

        switch (level.value)
        {
            case 0: // 쉬움
                movingPercent = grid;
                spikePercent = 1000 / grid;
                pointPercent = grid;
                timePercent = grid;
                sec = 90;
                break;
            case 1: // 보통
                movingPercent = grid * 2;
                spikePercent = (1000 / grid) / 2;
                pointPercent = grid * 2;
                timePercent = grid * 2;
                sec = 60;
                break;
            case 2: // 어려움
                movingPercent = grid / 3;
                spikePercent = (1000 / grid) / 3;
                pointPercent = grid * 3;
                timePercent = grid * 3;
                sec = 30;
                break;
        }

        if (grid > 9)
        {
            Debug.Log("1");
            isOkGrid = true;
        }
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
