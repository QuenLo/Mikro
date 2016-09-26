using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;


public class PreG1 : MonoBehaviour
{

    private float STARTTime;
    public float time;
    public AudioSource NOTE;
    
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("in");
            SceneManager.LoadScene("G1Movie", LoadSceneMode.Single);

        }
        if (Input.GetKeyDown(KeyCode.A)) {
            NOTE.Play();
        }

    }
}
