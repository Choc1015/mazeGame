using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject endPannel;
    public Text currentPoint;
    public Text bestPoint;
    public Text lastPoint;
    public int point;

    private void Awake()
    {
        Instance = this;
        
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
        SceneManager.LoadScene("InGame");
    }

    public void robbyScene()
    {
        SceneManager.LoadScene("OutGame_Start");
    }

    
}
