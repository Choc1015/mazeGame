using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public Text timeTxt;

    private int min = 0;
    private float sec = 10;

    private void Update()
    {
        if (min != 0 || sec > 0)
        {
            CheckTimer();
        }
    }

    private void CheckTimer()
    {
          sec -= Time.deltaTime;
        
       

        string TIMETEXT = $"{min} : {(int)sec}";
        timeTxt.text = TIMETEXT.ToString();

       // Debug.Log($"{min}�� {(int)sec} ��");

        if ((int)sec == 0)
        {
            if (min == 0)
            {
                Debug.Log(" �ð� ���Դϴ�. ");
            }
            else
            {
                min -= 1;
                sec = 60;
            }
        }
       
    }
}

