using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

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

    // ¾À º¯È¯
    public void startScene()
    {
        SceneManager.LoadScene("InGame");
    }
}
