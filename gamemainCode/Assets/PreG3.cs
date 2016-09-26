using UnityEngine;
using System.Collections;
using System.Timers;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Threading;
using System;


public class PreG3 : MonoBehaviour {

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
		if(Math.Round(Time.time-STARTTime, 1) == 46.5f)
    	{
    		SceneManager.LoadScene("Preview_Three", LoadSceneMode.Single);
  		}	
	}
}