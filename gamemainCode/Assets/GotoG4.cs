using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;

public class GotoG4 : MonoBehaviour
{
    public float time;
    private float STARTTime;
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
        if (Math.Round(Time.time - STARTTime, 1) == 5.0f)
        {
            print("in");
            SceneManager.LoadScene("G4Movie", LoadSceneMode.Single);

        }

    }
}
