using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{



    // �� ��ȯ
    public void startScene()
    {
        SceneManager.LoadScene("InGame");
    }
}
