using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class Show_PicL2 : MonoBehaviour
{

    public GameObject Step1, Step2, Step3, Step4, Step5, Step6, Step7, Step8;
    public float currenttime;
    public int printcount;
   // public int loop;
    private bool NEXT; //go check
    public Animator PreHow;
    public GameObject[] Pic = new GameObject[5];

    // Use this for initialization
    void Start()
    {
        Time.fixedDeltaTime = 0.5f;
        Step1.SetActive(true);
        //Step2.SetActive(false);
        //Step3.SetActive(false);
        //Step4.SetActive(false);
        //Step5.SetActive(false);
        //Step6.SetActive(false);
        //Step7.SetActive(false);
        //Step8.SetActive(false);
        currenttime = Time.time;
        printcount = 0;
      //  loop = 0;
       
        NEXT = false;
        showPic(5);
    }

    //	IEnumerator WaitTime(){
    //		print (Time.time);
    //		yield return new WaitForSeconds (3);
    //		print (Time.time);
    //	}

    // Update is called once per frame

    void FixedUpdate()
    {
        #region old
        /* 
        if (loop < 3)
            {
                if (printcount % 10 == 0)
                {
                    Step1.SetActive(true);
                    Step2.SetActive(false);
                    Step3.SetActive(false);
                    Step4.SetActive(false);
                    Step5.SetActive(false);
                    Step6.SetActive(false);
                    Step7.SetActive(false);
                    Step8.SetActive(false);
                }
                else if (printcount % 10 == 1)
                {
                    Step1.SetActive(false);
                    Step2.SetActive(true);
                    Step3.SetActive(false);
                    Step4.SetActive(false);
                    Step5.SetActive(false);
                    Step6.SetActive(false);
                    Step7.SetActive(false);
                    Step8.SetActive(false);
                }
                else if (printcount % 10 == 2)
                {
                    Step1.SetActive(false);
                    Step2.SetActive(false);
                    Step3.SetActive(true);
                    Step4.SetActive(false);
                    Step5.SetActive(false);
                    Step6.SetActive(false);
                    Step7.SetActive(false);
                    Step8.SetActive(false);
                }
                else if (printcount % 10 == 3)
                {
                    Step1.SetActive(false);
                    Step2.SetActive(false);
                    Step3.SetActive(false);
                    Step4.SetActive(true);
                    Step5.SetActive(false);
                    Step6.SetActive(false);
                    Step7.SetActive(false);
                    Step8.SetActive(false);
                }
                else if (printcount % 10 == 4)
                {
                    Step1.SetActive(false);
                    Step2.SetActive(false);
                    Step3.SetActive(false);
                    Step4.SetActive(false);
                    Step5.SetActive(true);
                    Step6.SetActive(false);
                    Step7.SetActive(false);
                    Step8.SetActive(false);
                }
                else if (printcount % 10 == 5)
                {
                    Step1.SetActive(false);
                    Step2.SetActive(false);
                    Step3.SetActive(false);
                    Step4.SetActive(false);
                    Step5.SetActive(false);
                    Step6.SetActive(true);
                    Step7.SetActive(false);
                    Step8.SetActive(false);
                }
                else if (printcount % 10 == 6)
                {
                    Step1.SetActive(false);
                    Step2.SetActive(false);
                    Step3.SetActive(false);
                    Step4.SetActive(false);
                    Step5.SetActive(false);
                    Step6.SetActive(false);
                    Step7.SetActive(true);
                    Step8.SetActive(false);
                }
                else if (printcount % 10 == 7)
                {
                    Step1.SetActive(false);
                    Step2.SetActive(false);
                    Step3.SetActive(false);
                    Step4.SetActive(false);
                    Step5.SetActive(false);
                    Step6.SetActive(false);
                    Step7.SetActive(false);
                    Step8.SetActive(true);
                    loop++;

                }
                printcount++;
            }
            //if (loop >= 3)
            //{
            //    SceneManager.LoadScene("Game2", LoadSceneMode.Single);
            //}
        */
        #endregion

        #region new
        if (NEXT)
        {
            if (G2scripts.A || Input.GetKeyDown(KeyCode.A))
            {
                showPic(0);
            }
            else if (G2scripts.B || Input.GetKeyDown(KeyCode.S))
            {
                showPic(1);
            }
            else if (G2scripts.C || Input.GetKeyDown(KeyCode.D))
            {
                showPic(2);
            }
            else if (G2scripts.D || Input.GetKeyDown(KeyCode.F))
            {
                showPic(3);
            }
            else if (G2scripts.E || Input.GetKeyDown(KeyCode.G))
            {
                showPic(4);
            }
            else
                showPic(5);
        }
        #endregion

    }

    void Update()
    {
        if (PreHow.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && !NEXT)
        {
            NEXT = true;
        }
        
        if(Input.GetKeyDown(KeyCode.Space) )
        {
            SceneManager.LoadScene("Game2", LoadSceneMode.Single);
        }

        if (NEXT)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                showPic(0);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                showPic(1);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                showPic(2);
            }
            else if ( Input.GetKeyDown(KeyCode.F))
            {
                showPic(3);
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                showPic(4);
            }
            /*else
                showPic(5);*/
        }
    }
    void showPic(int WHI) {
        for (int i=0; i < 5; i++) {
            if (i == WHI) {
                Pic[i].SetActive(true);
            }
            else
            Pic[i].SetActive(false);
        }
    }
}
