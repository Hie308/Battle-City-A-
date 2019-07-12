using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
using UnityEngine;

public class GameUI : MonoBehaviour {

    public Rect WaveBanner;
    public Text WaveNo;
    public Text WaveEneCount;
    Spawner spawner;

    void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
        
    }
    void OnNewWave(int waveNo)
    {

    }
}
