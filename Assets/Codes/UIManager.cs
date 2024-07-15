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
    public int point;

    private void Awake()
    {
        Instance = this;
        if(currentPoint != null)
        currentPoint.text = 0.ToString();
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
        endPannel.gameObject.SetActive(true);
    }

    // ¾À º¯È¯
    public void startScene()
    {
        SceneManager.LoadScene("InGame");
    }

    public void robbyScene()
    {
        SceneManager.LoadScene("OutGame_Start");
    }
}
