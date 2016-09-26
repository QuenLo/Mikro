using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class Show_PicL3 : MonoBehaviour {

	public GameObject Step1,Step2,Step3,Step4,Step5,Step6,Step7,Step8,Step9;
	public float currenttime;
	public int printcount;
	public int loop;
    private bool TAB;

	// Use this for initialization
	void Start () {
		Time.fixedDeltaTime = 0.5f;
		Step1.SetActive (false);
		Step2.SetActive (false);
		Step3.SetActive (false);
		Step4.SetActive (false);
		Step5.SetActive (false);
		Step6.SetActive (false);
		Step7.SetActive (false);
		Step8.SetActive (false);
		Step9.SetActive (false);
		currenttime = Time.time;
		printcount = 0;
		loop = -1;
        
	}

    //	IEnumerator WaitTime(){
    //		print (Time.time);
    //		yield return new WaitForSeconds (3);
    //		print (Time.time);
    //	}

    // Update is called once per frame
    void Update() {
        if (Time.time - currenttime >= 6.0f && loop==-1) {
            loop++;
        }
    }

	void FixedUpdate () {
            if (loop>=0 && loop < 1)
            {
                if (printcount % 11 == 0)
                {
                    Step1.SetActive(true);
                    Step2.SetActive(false);
                    Step3.SetActive(false);
                    Step4.SetActive(false);
                    Step5.SetActive(false);
                    Step6.SetActive(false);
                    Step7.SetActive(false);
                    Step8.SetActive(false);
                    Step9.SetActive(false);
                }
                else if (printcount % 11 == 1)
                {
                    Step1.SetActive(false);
                    Step2.SetActive(true);
                    Step3.SetActive(false);
                    Step4.SetActive(false);
                    Step5.SetActive(false);
                    Step6.SetActive(false);
                    Step7.SetActive(false);
                    Step8.SetActive(false);
                    Step9.SetActive(false);
                }
                else if (printcount % 11 == 2)
                {
                    Step1.SetActive(false);
                    Step2.SetActive(false);
                    Step3.SetActive(true);
                    Step4.SetActive(false);
                    Step5.SetActive(false);
                    Step6.SetActive(false);
                    Step7.SetActive(false);
                    Step8.SetActive(false);
                    Step9.SetActive(false);
                }
                else if (printcount % 11 == 3)
                {
                    Step1.SetActive(false);
                    Step2.SetActive(false);
                    Step3.SetActive(false);
                    Step4.SetActive(true);
                    Step5.SetActive(false);
                    Step6.SetActive(false);
                    Step7.SetActive(false);
                    Step8.SetActive(false);
                    Step9.SetActive(false);
                }
                else if (printcount % 11 == 4)
                {
                    Step1.SetActive(false);
                    Step2.SetActive(false);
                    Step3.SetActive(false);
                    Step4.SetActive(false);
                    Step5.SetActive(true);
                    Step6.SetActive(false);
                    Step7.SetActive(false);
                    Step8.SetActive(false);
                    Step9.SetActive(false);
                }
                else if (printcount % 11 == 5)
                {
                    Step1.SetActive(false);
                    Step2.SetActive(false);
                    Step3.SetActive(false);
                    Step4.SetActive(false);
                    Step5.SetActive(false);
                    Step6.SetActive(true);
                    Step7.SetActive(false);
                    Step8.SetActive(false);
                    Step9.SetActive(false);
                }
                else if (printcount % 11 == 6)
                {
                    Step1.SetActive(false);
                    Step2.SetActive(false);
                    Step3.SetActive(false);
                    Step4.SetActive(false);
                    Step5.SetActive(false);
                    Step6.SetActive(false);
                    Step7.SetActive(true);
                    Step8.SetActive(false);
                    Step9.SetActive(false);
                }
                else if (printcount % 11 == 7)
                {
                    Step1.SetActive(false);
                    Step2.SetActive(false);
                    Step3.SetActive(false);
                    Step4.SetActive(false);
                    Step5.SetActive(false);
                    Step6.SetActive(false);
                    Step7.SetActive(false);
                    Step8.SetActive(true);
                    Step9.SetActive(false);
                }
                else if (printcount % 11 == 8)
                {
                    Step1.SetActive(false);
                    Step2.SetActive(false);
                    Step3.SetActive(false);
                    Step4.SetActive(false);
                    Step5.SetActive(false);
                    Step6.SetActive(false);
                    Step7.SetActive(false);
                    Step8.SetActive(false);
                    Step9.SetActive(true);
                    loop++;
                }
                printcount++;
            }
            if (loop == 1)
            {
                SceneManager.LoadScene("Game3", LoadSceneMode.Single);
            }
        
	}
}
