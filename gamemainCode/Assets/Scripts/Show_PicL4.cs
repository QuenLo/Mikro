using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;

public class Show_PicL4 : MonoBehaviour
{

    private float STARTTime;
    // Use this for initialization
    void Start()
    {
        STARTTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        print(Math.Round(Time.time - STARTTime, 1));
        if (Math.Round(Time.time - STARTTime, 1) == 7.7)
        {
            print("in");
            SceneManager.LoadScene("Game4", LoadSceneMode.Single);

        }

    }
}
