using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;


public class G3Movie : MonoBehaviour
{

    private float STARTTime;
    public float time;
   

    // Use this for initialization
    void Start()
    {
        STARTTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //time = Time.time;
        //print(Math.Round(Time.time - STARTTime, 1));
        

        if (Math.Round(Time.time - STARTTime, 1) == 28.0f)
        {
            print("in");
            SceneManager.LoadScene("Preview_Three", LoadSceneMode.Single);

        }

    }
}
