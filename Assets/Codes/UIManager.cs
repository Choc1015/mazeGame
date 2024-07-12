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
        currentPoint.text = 0.ToString();
    }

    private void Update()
    {
    }

    public void updatePoint()
    {
        currentPoint.text = point.ToString();
    }

    // �� ��ȯ
    public void startScene()
    {
        SceneManager.LoadScene("InGame");
    }
}
