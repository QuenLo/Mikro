using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;


public class G1ovie : MonoBehaviour
{

    private float STARTTime;
    public float time;
    public AudioSource BKMusic;
    // Use this for initialization
    void Start()
    {
        STARTTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.time;
        //print(Math.Round(Time.time - STARTTime, 1));
       
        if (Math.Round(Time.time - STARTTime, 1) == 41.0f)
        {
            print("in");
            SceneManager.LoadScene("Game1", LoadSceneMode.Single);

        }

    }
}
