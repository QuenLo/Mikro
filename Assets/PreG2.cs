using UnityEngine;
using System.Collections;
using System.Timers;
using UnityEngine.SceneManagement;

using System.Threading;
using System;


public class PreG2 : MonoBehaviour {

	// Use this for initialization
	public GameObject Movie;
	public GameObject PreBK;
	public float STARTTime;
	void Start () {
		PreBK.SetActive(true);
		STARTTime = Time.time;
		Movie.SetActive(false);			
	}
	
	// Update is called once per frame
	void Update () {
		Movie.SetActive(true);
		print(Math.Round(Time.time-STARTTime, 1));
		if(Math.Round(Time.time-STARTTime, 1) == 36.5f)
    	{
    		SceneManager.LoadScene("Preview_First", LoadSceneMode.Single);
  		}	
	}
}
