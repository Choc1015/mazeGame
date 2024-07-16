using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource BGM;
    public AudioSource soundEffect;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        BGM.volume = GameManager.musicValue;
        soundEffect.volume = GameManager.soundEffectValue;

        if(SceneManager.GetActiveScene().name == "OutGame_Start")
        {
            if (Input.GetMouseButtonDown(0))
            {
                soundEffect.Play();
            }
        }
       
    }
}
