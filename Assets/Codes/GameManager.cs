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
    // 지난 점수 
    private void Awake()
    {
        Instance = this;
    }
}
