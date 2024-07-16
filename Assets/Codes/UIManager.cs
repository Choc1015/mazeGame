using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject endPannel;
    public GameObject retryPannel;
    public Text currentPoint;
    public Text bestPoint;
    public Text lastPoint;
    public Text timeTxt;
    public int point;
    static public int min = 0;
    static public float sec = 20;
    private void Awake()
    {
        Instance = this;
        min = 0;
        sec = 20;
    }

    private void Start()
    {
        if (currentPoint != null)
            currentPoint.text = 0.ToString();

        if (bestPoint != null)
            bestPoint.text = "최고 기록 : " + GameManager.bestPoint.ToString();
        if (lastPoint != null)
            lastPoint.text = "지난 기록 : " + GameManager.lastPoint.ToString();
    }

    private void Update()
    {
        if (min != 0 || sec > 0)
        {
            if (timeTxt != null)
                CheckTimer();
        }
    }

    public void updatePoint()
    {
        if (currentPoint != null)
            currentPoint.text = point.ToString();
    }

    public void ActivePannel()
    {

        GameManager.inputPoint = point;
        endPannel.gameObject.SetActive(true);
    }

   
    // 씬 변환
    public void startScene()
    {
        if (GameManager.Instance.isOkGrid)
            SceneManager.LoadScene("InGame");
    }
    public void reStartScene()
    {
            SceneManager.LoadScene("InGame");
    }

    public void robbyScene()
    {
        SceneManager.LoadScene("OutGame_Start");
    }
    


    private void CheckTimer()
    {
        sec -= Time.deltaTime;



        string TIMETEXT = $"{min} : {(int)sec}";
        timeTxt.text = TIMETEXT.ToString();

        // Debug.Log($"{min}분 {(int)sec} 초");

        if ((int)sec == 0)
        {
            if (min == 0)
            {
                retryPannel.SetActive(true);
            }
            else
            {
                min -= 1;
                sec += 60;
            }
        }

        if((int)sec >= 60)
        {
            min += 1;
            sec -= 60;
        }

    }

}
