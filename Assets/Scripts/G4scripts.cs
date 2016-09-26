using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
//using System;


public class G4scripts : MonoBehaviour
{
    public static int WinNum = 15;
    //public GameObject Rain;
    public GameObject RainA;
    public GameObject RainB;
    public GameObject RainC;
    public GameObject RainD;
    public GameObject RainE;
    public GameObject InstanceA;
    public GameObject InstanceB;
    public GameObject InstanceC;
    public GameObject InstanceD;
    public GameObject InstanceE;
    //public  GameObject RainDropKeep;

    #region background
    public GameObject[] F2 = new GameObject[8];
    public GameObject F2_1;
    public GameObject F2_2;
    public GameObject F2_3;
    public GameObject F2_4;
    public GameObject F2_5;
    public GameObject F2_6;
    public GameObject F2_7;

    public GameObject[] F3 = new GameObject[5];
    public GameObject F3_1;
    public GameObject F3_2;
    public GameObject F3_3;
    public GameObject F3_4;

    public GameObject[] F4 = new GameObject[8];
    public GameObject F4_1, F4_2, F4_3, F4_4, F4_5, F4_6, F4_7;
    #endregion

    
    public int stage;
    public double StartTime;
    public double NowTime;
    public double L1EndTime = 0;
    public double L2EndTime = 0;
    public float startTA;
    public float startTB;
    public float startTC;
    public float startTD;
    public float startTE;

    public int count;
    public int countA;
    public int countB;
    public int countC;
    public int countD;
    public int countE;
    public int WinCount1;
    public int WinCount2;
    public int WinCount3;
    public bool Space;
    GameObject RainCloneA;
    GameObject RainCloneB;
    GameObject RainCloneC;
    GameObject RainCloneD;
    GameObject RainCloneE;
    public SpriteRenderer caseA;
    public SpriteRenderer caseB;
    public SpriteRenderer caseC;
    public SpriteRenderer caseD;
    public SpriteRenderer caseE;
    public GameObject[] ScoreLine = new GameObject[16];
    public GameObject S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, S10, S11, S12, S13, S14, S15;
    public GameObject[] L2ScoreLine = new GameObject[16];
    public GameObject L2S0, L2S1, L2S2, L2S3, L2S4, L2S5, L2S6, L2S7, L2S8, L2S9, L2S10, L2S11, L2S12, L2S13, L2S14, L2S15;
    public GameObject[] L3ScoreLine = new GameObject[16];
    public GameObject L3S0, L3S1, L3S2, L3S3, L3S4, L3S5, L3S6, L3S7, L3S8, L3S9, L3S10, L3S11, L3S12, L3S13, L3S14, L3S15;
    public bool GoL1;
    public bool GoL2;
    public bool GoL3;

    //timer
    private static System.Timers.Timer aTimer;
    public int counti;
    public bool EndL1, EndL2, EndL3;

    #region new_rain
    public static float Keepdelay = 2.9f;
    List<newraindrop> new_rain = new List<newraindrop>()        {
            new newraindrop('A', 3.0f-Keepdelay),  new newraindrop('B', 3.7f-Keepdelay),  new newraindrop('C', 4.4f-Keepdelay),
            new newraindrop('D', 6.8f-Keepdelay),  new newraindrop('E', 7.6f-Keepdelay),  new newraindrop('C', 8.4f-Keepdelay),
            new newraindrop('A', 10.9f-Keepdelay), new newraindrop('B', 11.7f-Keepdelay), new newraindrop('D', 12.4f-Keepdelay),
            new newraindrop('A', 15.0f-Keepdelay), new newraindrop('E', 15.8f-Keepdelay), new newraindrop('B', 16.5f-Keepdelay),
            new newraindrop('C', 19.1f-Keepdelay), new newraindrop('D', 19.9f-Keepdelay), new newraindrop('E', 20.6f-Keepdelay),
            new newraindrop('A', 23.1f-Keepdelay),  new newraindrop('B', 23.8f-Keepdelay),  new newraindrop('C', 24.6f-Keepdelay),
            new newraindrop('D', 27.2f-Keepdelay),  new newraindrop('E', 28.0f-Keepdelay),  new newraindrop('C', 28.8f-Keepdelay),
            new newraindrop('A', 31.2f-Keepdelay), new newraindrop('B', 32.0f-Keepdelay), new newraindrop('D', 32.9f-Keepdelay),
            new newraindrop('A', 35.3f-Keepdelay), new newraindrop('E', 36.3f-Keepdelay), new newraindrop('B', 36.9f-Keepdelay),
            new newraindrop('C', 39.3f-Keepdelay), new newraindrop('D', 40.9f-Keepdelay), new newraindrop('E', 41.7f-Keepdelay),new newraindrop('C', 43.4f-Keepdelay),
            new newraindrop('A', 44.2f-Keepdelay),  new newraindrop('B', 45.0f-Keepdelay),  new newraindrop('C', 47.5f-Keepdelay),
            new newraindrop('D', 48.2f-Keepdelay),  new newraindrop('E', 49.0f-Keepdelay),  new newraindrop('C', 51.5f-Keepdelay),
            new newraindrop('A', 52.4f-Keepdelay), new newraindrop('B', 53.1f-Keepdelay), new newraindrop('D', 55.6f-Keepdelay),
            new newraindrop('A', 56.4f-Keepdelay), new newraindrop('E', 57.2f-Keepdelay), new newraindrop('D', 59.7f-Keepdelay),
            new newraindrop('E', 60.5f-Keepdelay)
    };
    /*
    List<newraindrop> new_rain2 = new List<newraindrop>()        {
            new newraindrop('A', 1.5f ),  new newraindrop('A', 3.0f),  new newraindrop('A', 5.4f),
            new newraindrop('A', 8.0f),  new newraindrop('A', 9.3f),  new newraindrop('A', 10.0f),
            new newraindrop('A', 11.5f), new newraindrop('A', 13.8f), new newraindrop('A', 14.3f),
            new newraindrop('A', 17.5f), new newraindrop('A', 18.7f), new newraindrop('A', 19.8f),
            new newraindrop('A', 21.2f), new newraindrop('A', 22.5f), new newraindrop('A', 23.0f)
    };

    List<newraindrop> new_rain3 = new List<newraindrop>()        {
            new newraindrop('C', 1.5f),  new newraindrop('C', 3.0f),  new newraindrop('C', 5.4f),
            new newraindrop('C', 8.0f),  new newraindrop('C', 9.3f),  new newraindrop('C', 10.0f),
            new newraindrop('C', 11.5f), new newraindrop('C', 13.8f), new newraindrop('C', 14.3f),
            new newraindrop('C', 17.5f), new newraindrop('C', 18.7f), new newraindrop('C', 19.8f),
            new newraindrop('C', 21.2f), new newraindrop('C', 22.5f), new newraindrop('C', 23.0f)
    };
    */

    Queue queueA = new Queue();
    Queue queueB = new Queue();
    Queue queueC = new Queue();
    Queue queueD = new Queue();
    Queue queueE = new Queue();
    #endregion

    
    //0=not ;1=true ;2=false;

    //#region playmusic
    //public AudioMixerSnapshot out
    //#endregion

    public class newraindrop
    {
        public char where { get; set; }
        public double releasetime { get; set; }


        public newraindrop(char a, double b)
        {
            where = a;
            releasetime = b;
        }
    }


    // Use this for initialization
    void Start()
    {

        Time.fixedDeltaTime = 0.1f;
        StartTime = Time.time;
        count = 0;
        WinCount1 = 0;
        GoL1 = true;
        GoL2 = false;
        GoL3 = false;
        stage = 1;
        RainA = GameObject.Find("RainDropObjectA");
        RainB = GameObject.Find("RainDropObjectB");
        RainC = GameObject.Find("RainDropObjectC");
        RainD = GameObject.Find("RainDropObjectD");
        RainE = GameObject.Find("RainDropObjectE");

        queueA = new Queue();
        queueB = new Queue();
        queueC = new Queue();
        queueD = new Queue();
        queueE = new Queue();
        startTA = 0;
        startTB = 0;
        startTC = 0;
        startTD = 0;
        startTE = 0;
        //  caseA.color = new Color(1f, 1f, 0f, 0f);

        #region 初始能量條
        //初始能量條
        ScoreLine[0] = S0; ScoreLine[1] = S1; ScoreLine[2] = S2; ScoreLine[3] = S3; ScoreLine[4] = S4; ScoreLine[5] = S5;
        ScoreLine[6] = S6; ScoreLine[7] = S7; ScoreLine[8] = S8; ScoreLine[9] = S9; ScoreLine[10] = S10; ScoreLine[11] = S11;
        ScoreLine[12] = S12; ScoreLine[13] = S13; ScoreLine[14] = S14; ScoreLine[15] = S15;
        for (int i = 1; i < 16; i++)
        {
            ScoreLine[i].SetActive(false);
        }

        //初始能量條L2
        L2ScoreLine[0] = L2S0; L2ScoreLine[1] = L2S1; L2ScoreLine[2] = L2S2; L2ScoreLine[3] = L2S3; L2ScoreLine[4] = L2S4; L2ScoreLine[5] = L2S5;
        L2ScoreLine[6] = L2S6; L2ScoreLine[7] = L2S7; L2ScoreLine[8] = L2S8; L2ScoreLine[9] = L2S9; L2ScoreLine[10] = L2S10; L2ScoreLine[11] = L2S11;
        L2ScoreLine[12] = L2S12; L2ScoreLine[13] = L2S13; L2ScoreLine[14] = L2S14; L2ScoreLine[15] = L2S15;
        for (int i = 0; i < 16; i++)
        {
            L2ScoreLine[i].SetActive(false);
        }

        //初始能量條L3
        L3ScoreLine[0] = L3S0; L3ScoreLine[1] = L3S1; L3ScoreLine[2] = L3S2; L3ScoreLine[3] = L3S3; L3ScoreLine[4] = L3S4; L3ScoreLine[5] = L3S5;
        L3ScoreLine[6] = L3S6; L3ScoreLine[7] = L3S7; L3ScoreLine[8] = L3S8; L3ScoreLine[9] = L3S9; L3ScoreLine[10] = L3S10; L3ScoreLine[11] = L3S11;
        L3ScoreLine[12] = L3S12; L3ScoreLine[13] = L3S13; L3ScoreLine[14] = L3S14; L3ScoreLine[15] = L3S15;
        for (int i = 0; i < 16; i++)
        {
            L3ScoreLine[i].SetActive(false);
        }
        #endregion


        #region 初始畫面
        //F2
        F2[0] = F2_1; F2[1] = F2_2; F2[2] = F2_3; F2[3] = F2_4; F2[4] = F2_5; F2[5] = F2_6; F2[6] = F2_7;
        for (int i = 0; i < 7; i++)
        {
            F2[i].SetActive(false);
        }
        //F3
        F3[0] = F3_1; F3[1] = F3_2; F3[2] = F3_3; F3[3] = F3_4;
        for (int i = 0; i < 4; i++)
        {
            F3[i].SetActive(false);
        }
        //F4
        F4[0] = F4_1; F4[1] = F4_2; F4[2] = F4_3; F4[3] = F4_4; F4[4] = F4_5; F4[5] = F4_6; F4[6] = F4_7;
        for (int i = 0; i < 7; i++)
        {
            F4[i].SetActive(false);
        }
        EndL1 = false;
        EndL2 = false;
        EndL3 = false;
        #endregion

    }

    //change color
    //caseB.GetComponent<Renderer>().material.color = Color.HSVToRGB(0.172f, 0.62f, 0.99f);

    void Update ()
    {
            if (Input.GetKeyDown(KeyCode.Space)) {
                print(Math.Round(Time.time, 2));
            }



            #region show case Color

            if (startTA != 0)
            {

                caseA.GetComponent<Renderer>().material.color = Color.HSVToRGB(0.172f, 0.62f, 0.99f);
                if (Time.time - startTA > 0.2f)
                {
                    caseA.GetComponent<Renderer>().material.color = Color.white;
                    startTA = 0;
                }
            }
            if (startTB != 0)
            {

                caseB.GetComponent<Renderer>().material.color = Color.HSVToRGB(0.172f, 0.62f, 0.99f);
                if (Time.time - startTB > 0.2f)
                {
                    caseB.GetComponent<Renderer>().material.color = Color.white;
                    startTB = 0;
                }
            }
            if (startTC != 0)
            {

                caseC.GetComponent<Renderer>().material.color = Color.HSVToRGB(0.172f, 0.62f, 0.99f);
                if (Time.time - startTC > 0.2f)
                {
                    caseC.GetComponent<Renderer>().material.color = Color.white;
                    startTC = 0;
                }
            }
            if (startTD != 0)
            {

                caseD.GetComponent<Renderer>().material.color = Color.HSVToRGB(0.172f, 0.62f, 0.99f);
                if (Time.time - startTD > 0.2f)
                {
                    caseD.GetComponent<Renderer>().material.color = Color.white;
                    startTD = 0;
                }
            }
            if (startTE != 0)
            {

                caseE.GetComponent<Renderer>().material.color = Color.HSVToRGB(0.172f, 0.62f, 0.99f);
                if (Time.time - startTE > 0.2f)
                {
                    caseE.GetComponent<Renderer>().material.color = Color.white;
                    startTE = 0;
                }
            }


            #endregion

            #region check if succed L1
            if (GoL1)
            {
                if (stage == 1) {

                    if (WinCount1 == 0)
                    {
                        ShowLineL1(0);
                    }

                    if (queueA.Count > 0)
                    {
                        GameObject newAobject = queueA.Peek() as GameObject;

                        if (newAobject.transform.position.y < -2.0 && newAobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.A) || G2scripts.A))
                        {

                            print("yes u succed! A ");
                        
                            startTA = Time.time;
                            WinCount1 = WinCount1 + 3;

                            if (WinCount1 == 14) {
                                WinCount1 = 15;
                            }

                            ShowLineL1(WinCount1);
                            print("wincount is " + WinCount1);
                            Destroy(newAobject);
                            queueA.Dequeue();
                        }

                        else if (newAobject.transform.position.y < -4.5)
                        {
                            Destroy(newAobject);
                            queueA.Dequeue();

                        }

                        else if (Input.GetKeyDown(KeyCode.A) || G2scripts.A)
                        {
                            // write different color please
                            startTA = Time.time;
                          
                            Destroy(newAobject);
                            queueA.Dequeue();
                            print("what the fuck A ");
                        }

                        if (newAobject == null)
                        {
                            caseA.GetComponent<Renderer>().material.color = Color.white;
                        }

                    }

                    if (queueB.Count > 0)
                    {
                        GameObject newBobject = queueB.Peek() as GameObject;

                        if (newBobject.transform.position.y < -2.0 && newBobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.S) || G2scripts.B))
                        {

                            print("yes u succed! B ");
                            startTB = Time.time;
                            WinCount1 = WinCount1 + 3;

                            if (WinCount1 == 14) {
                                WinCount1 = 15;
                            }
                            ShowLineL1(WinCount1);
                            print("wincount is " + WinCount1);
                            Destroy(newBobject);
                            queueB.Dequeue();
                        }

                        else if (newBobject.transform.position.y < -4.5)
                        {
                            Destroy(newBobject);
                            queueB.Dequeue();
                        }

                        else if (Input.GetKeyDown(KeyCode.S) || G2scripts.B)
                        {
                            Destroy(newBobject);
                            queueB.Dequeue();
                            print("what the fuck B ");
                        }

                    }

                    if (queueC.Count > 0)
                    {
                        GameObject newCobject = queueC.Peek() as GameObject;

                        if (newCobject.transform.position.y < -2.0 && newCobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.D) || G2scripts.C))
                        {
                            print("yes u succed! C ");
                            startTC = Time.time;
                            WinCount1 = WinCount1 + 3;

                            if (WinCount1 == 14) {
                                WinCount1 = 15;
                            }
                            ShowLineL1(WinCount1);
                            print("wincount is " + WinCount1);
                            Destroy(newCobject);
                            queueC.Dequeue();
                        }

                        else if (newCobject.transform.position.y < -4.5)
                        {
                            Destroy(newCobject);
                            queueC.Dequeue();
                        }

                        else if (Input.GetKeyDown(KeyCode.D) || G2scripts.C)
                        {
                            Destroy(newCobject);
                            queueC.Dequeue();
                            print("what the fuck C " + Math.Round(Time.time, 2));
                        }

                    }

                    if (queueD.Count > 0)
                    {
                        GameObject newDobject = queueD.Peek() as GameObject;

                        if (newDobject.transform.position.y < -2.0 && newDobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.F) || G2scripts.D))
                        {
                            print("yes u succed! D ");
                            startTD = Time.time;
                            WinCount1 = WinCount1 + 3;

                            if (WinCount1 == 14) {
                                WinCount1 = 15;
                            }
                            ShowLineL1(WinCount1);
                            print("wincount is " + WinCount1);
                            Destroy(newDobject);
                            queueD.Dequeue();
                        }

                        else if (newDobject.transform.position.y < -4.5)
                        {
                            Destroy(newDobject);
                            queueD.Dequeue();
                        }

                        else if (Input.GetKeyDown(KeyCode.F) || G2scripts.D)
                        {
                            Destroy(newDobject);
                            queueD.Dequeue();
                            print("what the fuck D");
                        }
                    }

                    if (queueE.Count > 0)
                    {
                        GameObject newEobject = queueE.Peek() as GameObject;

                        if (newEobject.transform.position.y < -2.0 && newEobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.G) || G2scripts.E))
                        {
                            print("yes u succed! E ");
                            startTE = Time.time;
                            WinCount1 = WinCount1 + 3;

                            if (WinCount1 == 14) {
                                WinCount1 = 15;
                            }
                            ShowLineL1(WinCount1);
                            print("wincount is " + WinCount1);
                            Destroy(newEobject);
                            queueE.Dequeue();
                        }

                        else if (newEobject.transform.position.y < -4.5)
                        {
                            Destroy(newEobject);
                            queueE.Dequeue();
                        }

                        else if (Input.GetKeyDown(KeyCode.G) || G2scripts.E)
                        {
                            Destroy(newEobject);
                            queueE.Dequeue();
                            print("what the fuck E");
                        }

                    }
                }

                if (stage == 2) {

                    print("stage si 2");

                    if (WinCount1 == 0)
                    {
                        ShowLineL2(0);
                    }

                    if (queueA.Count > 0)
                    {
                        GameObject newAobject = queueA.Peek() as GameObject;

                        if (newAobject.transform.position.y < -2.0 && newAobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.A) || G2scripts.A))
                        {

                            print("yes u succed! A ");
                            startTA = Time.time;
                            WinCount1 = WinCount1 + 3;

                            if (WinCount1 == 14) {
                                WinCount1 = 15;
                            }

                            ShowLineL2(WinCount1);
                            print("wincount is " + WinCount1);
                            Destroy(newAobject);
                            queueA.Dequeue();
                        }

                        else if (newAobject.transform.position.y < -4.5)
                        {
                            Destroy(newAobject);
                            queueA.Dequeue();

                        }

                        else if (Input.GetKeyDown(KeyCode.A) || G2scripts.A)
                        {
                            // write different color please
                            Destroy(newAobject);
                            queueA.Dequeue();
                            print("what the fuck A ");
                        }

                        if (newAobject == null)
                        {
                            caseA.GetComponent<Renderer>().material.color = Color.white;
                        }

                    }

                    if (queueB.Count > 0)
                    {
                        GameObject newBobject = queueB.Peek() as GameObject;

                        if (newBobject.transform.position.y < -2.0 && newBobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.S) || G2scripts.B))
                        {

                            print("yes u succed! B ");
                            startTB = Time.time;
                            WinCount1 = WinCount1 + 3;

                            if (WinCount1 == 14) {
                                WinCount1 = 15;
                            }
                            ShowLineL2(WinCount1);
                            print("wincount is " + WinCount1);
                            Destroy(newBobject);
                            queueB.Dequeue();
                        }

                        else if (newBobject.transform.position.y < -4.5)
                        {
                            Destroy(newBobject);
                            queueB.Dequeue();
                        }

                        else if (Input.GetKeyDown(KeyCode.S) || G2scripts.B)
                        {
                            Destroy(newBobject);
                            queueB.Dequeue();
                            print("what the fuck B ");
                        }

                    }

                    if (queueC.Count > 0)
                    {
                        GameObject newCobject = queueC.Peek() as GameObject;

                        if (newCobject.transform.position.y < -2.0 && newCobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.D) || G2scripts.C))
                        {
                            print("yes u succed! C ");
                            startTC = Time.time;
                            WinCount1 = WinCount1 + 3;

                            if (WinCount1 == 14) {
                                WinCount1 = 15;
                            }
                            ShowLineL2(WinCount1);
                            print("wincount is " + WinCount1);
                            Destroy(newCobject);
                            queueC.Dequeue();
                        }

                        else if (newCobject.transform.position.y < -4.5)
                        {
                            Destroy(newCobject);
                            queueC.Dequeue();
                        }

                        else if (Input.GetKeyDown(KeyCode.D) || G2scripts.C)
                        {
                            Destroy(newCobject);
                            queueC.Dequeue();
                            print("what the fuck C " + Math.Round(Time.time, 2));
                        }

                    }

                    if (queueD.Count > 0)
                    {
                        GameObject newDobject = queueD.Peek() as GameObject;

                        if (newDobject.transform.position.y < -2.0 && newDobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.F) || G2scripts.D))
                        {
                            print("yes u succed! D ");
                            startTD = Time.time;
                            WinCount1 = WinCount1 + 3;

                            if (WinCount1 == 14) {
                                WinCount1 = 15;
                            }

                            ShowLineL2(WinCount1);
                            print("wincount is " + WinCount1);
                            Destroy(newDobject);
                            queueD.Dequeue();
                        }

                        else if (newDobject.transform.position.y < -4.5)
                        {
                            Destroy(newDobject);
                            queueD.Dequeue();
                        }

                        else if (Input.GetKeyDown(KeyCode.F) || G2scripts.D)
                        {
                            Destroy(newDobject);
                            queueD.Dequeue();
                            print("what the fuck D");
                        }
                    }

                    if (queueE.Count > 0)
                    {
                        GameObject newEobject = queueE.Peek() as GameObject;

                        if (newEobject.transform.position.y < -2.0 && newEobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.G) || G2scripts.E))
                        {
                            print("yes u succed! E ");
                            startTE = Time.time;
                            WinCount1 = WinCount1 + 3;

                            if (WinCount1 == 14) {
                                WinCount1 = 15;
                            }
                            ShowLineL2(WinCount1);
                            print("wincount is " + WinCount1);
                            Destroy(newEobject);
                            queueE.Dequeue();
                        }

                        else if (newEobject.transform.position.y < -4.5)
                        {
                            Destroy(newEobject);
                            queueE.Dequeue();
                        }

                        else if (Input.GetKeyDown(KeyCode.G) || G2scripts.E)
                        {
                            Destroy(newEobject);
                            queueE.Dequeue();
                            print("what the fuck E");
                        }

                    }
                }

                if (stage == 3) {

                    if (WinCount1 == 0)
                    {
                        ShowLineL3(0);
                    }

                    if (queueA.Count > 0)
                    {
                        GameObject newAobject = queueA.Peek() as GameObject;

                        if (newAobject.transform.position.y < -2.0 && newAobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.A) || G2scripts.A))
                        {

                            print("yes u succed! A ");
                            startTA = Time.time;
                            WinCount1 = WinCount1 + 3;

                            if (WinCount1 == 14) {
                                WinCount1 = 15;
                            }

                            ShowLineL3(WinCount1);
                            print("wincount is " + WinCount1);
                            Destroy(newAobject);
                            queueA.Dequeue();
                        }

                        else if (newAobject.transform.position.y < -4.5)
                        {
                            Destroy(newAobject);
                            queueA.Dequeue();

                        }

                        else if (Input.GetKeyDown(KeyCode.A) || G2scripts.A)
                        {
                            // write different color please
                            Destroy(newAobject);
                            queueA.Dequeue();
                            print("what the fuck A ");
                        }

                        if (newAobject == null)
                        {
                            caseA.GetComponent<Renderer>().material.color = Color.white;
                        }

                    }

                    if (queueB.Count > 0)
                    {
                        GameObject newBobject = queueB.Peek() as GameObject;

                        if (newBobject.transform.position.y < -2.0 && newBobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.S) || G2scripts.B))
                        {

                            print("yes u succed! B ");
                            startTB = Time.time;
                            WinCount1 = WinCount1 + 3;

                            if (WinCount1 == 14) {
                                WinCount1 = 15;
                            }
                            ShowLineL3(WinCount1);
                            print("wincount is " + WinCount1);
                            Destroy(newBobject);
                            queueB.Dequeue();
                        }

                        else if (newBobject.transform.position.y < -4.5)
                        {
                            Destroy(newBobject);
                            queueB.Dequeue();
                        }

                        else if (Input.GetKeyDown(KeyCode.S) || G2scripts.B)
                        {
                            Destroy(newBobject);
                            queueB.Dequeue();
                            print("what the fuck B ");
                        }

                    }

                    if (queueC.Count > 0)
                    {
                        GameObject newCobject = queueC.Peek() as GameObject;

                        if (newCobject.transform.position.y < -2.0 && newCobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.D) || G2scripts.C))
                        {
                            print("yes u succed! C ");
                            startTC = Time.time;
                            WinCount1 = WinCount1 + 3;

                            if (WinCount1 == 14) {
                                WinCount1 = 15;
                            }
                            ShowLineL3(WinCount1);
                            print("wincount is " + WinCount1);
                            Destroy(newCobject);
                            queueC.Dequeue();
                        }

                        else if (newCobject.transform.position.y < -4.5)
                        {
                            Destroy(newCobject);
                            queueC.Dequeue();
                        }

                        else if (Input.GetKeyDown(KeyCode.D) || G2scripts.C)
                        {
                            Destroy(newCobject);
                            queueC.Dequeue();
                            print("what the fuck C " + Math.Round(Time.time, 2));
                        }

                    }

                    if (queueD.Count > 0)
                    {
                        GameObject newDobject = queueD.Peek() as GameObject;

                        if (newDobject.transform.position.y < -2.0 && newDobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.F) || G2scripts.D))
                        {
                            print("yes u succed! D ");
                            startTD = Time.time;
                            WinCount1 = WinCount1 + 3;

                            if (WinCount1 == 14) {
                                WinCount1 = 15;
                            }
                            ShowLineL3(WinCount1);
                            print("wincount is " + WinCount1);
                            Destroy(newDobject);
                            queueD.Dequeue();
                        }

                        else if (newDobject.transform.position.y < -4.5)
                        {
                            Destroy(newDobject);
                            queueD.Dequeue();
                        }

                        else if (Input.GetKeyDown(KeyCode.F) || G2scripts.D)
                        {
                            Destroy(newDobject);
                            queueD.Dequeue();
                            print("what the fuck D");
                        }
                    }

                    if (queueE.Count > 0)
                    {
                        GameObject newEobject = queueE.Peek() as GameObject;

                        if (newEobject.transform.position.y < -2.0 && newEobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.G) || G2scripts.E))
                        {
                            print("yes u succed! E ");
                            startTE = Time.time;
                            WinCount1 = WinCount1 + 3;

                            if (WinCount1 == 14) {
                                WinCount1 = 15;
                            }
                            ShowLineL3(WinCount1);
                            print("wincount is " + WinCount1);
                            Destroy(newEobject);
                            queueE.Dequeue();
                        }

                        else if (newEobject.transform.position.y < -4.5)
                        {
                            Destroy(newEobject);
                            queueE.Dequeue();
                        }

                        else if (Input.GetKeyDown(KeyCode.G) || G2scripts.E)
                        {
                            Destroy(newEobject);
                            queueE.Dequeue();
                            print("what the fuck E");
                        }

                    }
                }

            }
            #endregion L1


            #region check if succed L2
            /*
        if (!GoL1 && GoL2 && !GoL3)
        {
            if (WinCount2 == 0)
            {
                ShowLineL2(0);
            }
            if (queueA.Count > 0)
            {
                GameObject newAobject = queueA.Peek() as GameObject;

                if (newAobject.transform.position.y < -2.8 && newAobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.A) || G2scripts.A))
                {

                    print("22222yes u succed! A ");
                    startTA = Time.time;
                    WinCount2++;
                    ShowLineL2(WinCount2);
                    print("22222wincount is " + WinCount2);
                    Destroy(newAobject);
                    queueA.Dequeue();
                }
                else if (newAobject.transform.position.y < -4.5)
                {
                    Destroy(newAobject);
                    queueA.Dequeue();

                }

                else if (Input.GetKeyDown(KeyCode.A) || G2scripts.A)
                {
                    Destroy(newAobject);
                    queueA.Dequeue();
                    print("22222what the fuck A");
                }

                if (newAobject == null)
                {
                    caseA.GetComponent<Renderer>().material.color = Color.white;
                }

            }

            if (queueB.Count > 0)
            {
                GameObject newBobject = queueB.Peek() as GameObject;

                if (newBobject.transform.position.y < -2.8 && newBobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.S) || G2scripts.B))
                {

                    print("22222yes u succed! B ");
                    startTB = Time.time;
                    WinCount2++;
                    ShowLineL2(WinCount2);
                    print("22222wincount is " + WinCount2);
                    Destroy(newBobject);
                    queueB.Dequeue();
                }

                else if (newBobject.transform.position.y < -4.5)
                {
                    Destroy(newBobject);
                    queueB.Dequeue();
                }

                else if (Input.GetKeyDown(KeyCode.S) || G2scripts.B)
                {
                    Destroy(newBobject);
                    queueB.Dequeue();
                    print("22222what the fuck B");
                }

            }

            if (queueC.Count > 0)
            {
                GameObject newCobject = queueC.Peek() as GameObject;

                if (newCobject.transform.position.y < -2.8 && newCobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.D) || G2scripts.C))
                {
                    print("22222yes u succed! C ");
                    startTC = Time.time;
                    WinCount2++;
                    ShowLineL2(WinCount2);
                    print("22222wincount is " + WinCount2);
                    Destroy(newCobject);
                    queueC.Dequeue();
                }

                else if (newCobject.transform.position.y < -4.5)
                {
                    Destroy(newCobject);
                    queueC.Dequeue();
                }

                else if (Input.GetKeyDown(KeyCode.D) || G2scripts.C)
                {
                    Destroy(newCobject);
                    queueC.Dequeue();
                    print("22222what the fuck C");
                }

            }

            if (queueD.Count > 0)
            {
                GameObject newDobject = queueD.Peek() as GameObject;

                if (newDobject.transform.position.y < -2.8 && newDobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.F) || G2scripts.D))
                {
                    print("222222yes u succed! D ");
                    startTD = Time.time;
                    WinCount2++;
                    ShowLineL2(WinCount2);
                    print("222222wincount is " + WinCount2);
                    Destroy(newDobject);
                    queueD.Dequeue();
                }

                else if (newDobject.transform.position.y < -4.5)
                {
                    Destroy(newDobject);
                    queueD.Dequeue();
                }

                else if (Input.GetKeyDown(KeyCode.F) || G2scripts.D)
                {
                    Destroy(newDobject);
                    queueD.Dequeue();
                    print("222222what the fuck D");
                }
                else if (WinCount2 >= WinNum)
                {
                    Destroy(newDobject);
                }
            }

            if (queueE.Count > 0)
            {
                GameObject newEobject = queueE.Peek() as GameObject;
                if (newEobject.transform.position.y < -2.8 && newEobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.G) || G2scripts.E))
                {
                    print("222222yes u succed! E ");
                    startTE = Time.time;
                    WinCount2++;
                    ShowLineL2(WinCount2);
                    print("222222wincount is " + WinCount2);
                    Destroy(newEobject);
                    queueE.Dequeue();
                }

                else if (newEobject.transform.position.y < -4.5)
                {
                    Destroy(newEobject);
                    queueE.Dequeue();
                }

                else if (Input.GetKeyDown(KeyCode.G) || G2scripts.E)
                {
                    Destroy(newEobject);
                    queueE.Dequeue();
                    print("222222what the fuck E");
                }

            }
        }
        #endregion L2

        #region check if succed L3
        if (!GoL1 && !GoL2 && GoL3)
        {
            if (WinCount3 == 0)
            {
                ShowLineL3(0);
            }
            if (queueA.Count > 0)
            {
                GameObject newAobject = queueA.Peek() as GameObject;

                if (newAobject.transform.position.y < -2.8 && newAobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.A) || G2scripts.A))
                {

                    print("333333yes u succed! A ");
                    startTA = Time.time;
                    WinCount3++;
                    ShowLineL3(WinCount3);
                    print("333333wincount is " + WinCount3);
                    Destroy(newAobject);
                    queueA.Dequeue();
                }
                else if (newAobject.transform.position.y < -4.5)
                {
                    Destroy(newAobject);
                    queueA.Dequeue();

                }

                else if (Input.GetKeyDown(KeyCode.A) || G2scripts.A)
                {
                    Destroy(newAobject);
                    queueA.Dequeue();
                    print("3333333what the fuck A");
                }

                if (newAobject == null)
                {
                    caseA.GetComponent<Renderer>().material.color = Color.white;
                }

            }

            if (queueB.Count > 0)
            {
                GameObject newBobject = queueB.Peek() as GameObject;

                if (newBobject.transform.position.y < -2.8 && newBobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.S) || G2scripts.B))
                {

                    print("3333333yes u succed! B ");
                    startTB = Time.time;
                    WinCount3++;
                    ShowLineL3(WinCount3);
                    print("33333332wincount is " + WinCount3);
                    Destroy(newBobject);
                    queueB.Dequeue();
                }

                else if (newBobject.transform.position.y < -4.5)
                {
                    Destroy(newBobject);
                    queueB.Dequeue();
                }

                else if (Input.GetKeyDown(KeyCode.S) || G2scripts.B)
                {
                    Destroy(newBobject);
                    queueB.Dequeue();
                    print("3333333what the fuck B");
                }

            }

            if (queueC.Count > 0)
            {
                GameObject newCobject = queueC.Peek() as GameObject;

                if (newCobject.transform.position.y < -2.8 && newCobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.D) || G2scripts.C))
                {
                    print("3333333yes u succed! C ");
                    startTC = Time.time;
                    WinCount3++;
                    ShowLineL3(WinCount3);
                    print("3333333wincount is " + WinCount3);
                    Destroy(newCobject);
                    queueC.Dequeue();
                }

                else if (newCobject.transform.position.y < -4.5)
                {
                    Destroy(newCobject);
                    queueC.Dequeue();
                }

                else if (Input.GetKeyDown(KeyCode.D) || G2scripts.C)
                {
                    Destroy(newCobject);
                    queueC.Dequeue();
                    print("333333what the fuck C");
                }

            }

            if (queueD.Count > 0)
            {
                GameObject newDobject = queueD.Peek() as GameObject;

                if (newDobject.transform.position.y < -2.8 && newDobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.F) || G2scripts.D))
                {
                    print("33333333yes u succed! D ");
                    startTD = Time.time;
                    WinCount3++;
                    ShowLineL3(WinCount3);
                    print("3333333wincount is " + WinCount3);
                    Destroy(newDobject);
                    queueD.Dequeue();
                }

                else if (newDobject.transform.position.y < -4.5)
                {
                    Destroy(newDobject);
                    queueD.Dequeue();
                }

                else if (Input.GetKeyDown(KeyCode.F) || G2scripts.D)
                {
                    Destroy(newDobject);
                    queueD.Dequeue();
                    print("333333what the fuck D");
                }
                else if (WinCount3 >= WinNum)
                {
                    Destroy(newDobject);
                }
            }

            if (queueE.Count > 0)
            {
                GameObject newEobject = queueE.Peek() as GameObject;

                if (newEobject.transform.position.y < -2.8 && newEobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.G) || G2scripts.E))
                {
                    print("3333333yes u succed! E ");
                    startTE = Time.time;
                    WinCount3++;
                    ShowLineL3(WinCount3);
                    print("3333333wincount is " + WinCount3);
                    Destroy(newEobject);
                    queueE.Dequeue();
                }

                else if (newEobject.transform.position.y < -4.5)
                {
                    Destroy(newEobject);
                    queueE.Dequeue();
                }

                else if (Input.GetKeyDown(KeyCode.G) || G2scripts.E)
                {
                    Destroy(newEobject);
                    queueE.Dequeue();
                    print("3333333what the fuck E");
                }

            }
        }*/
            #endregion

            #region showPic
            if (!EndL2 && EndL1 && counti <= 7)
            {
                if (counti == 7)
                {
                    L1EndTime = Math.Round(Time.time, 2);
                    counti++;
                }
                else if (counti < 7)
                {
                    F2[counti].SetActive(true);
                }
            }
            if (EndL2 && counti <= 4)
            {
                if (counti == 4)
                {
                    L2EndTime = Math.Round(Time.time, 2);
                    counti++;
                }
                else
                {
                    //GoL2 = false;
                    F3[counti].SetActive(true);
                }
            }
            if (EndL3 && counti <= 7)
            {
                if (counti == 7)
                {
                    counti++;
                SceneManager.LoadScene("G4End", LoadSceneMode.Single);
            }
                else
                {
                    F4[counti].SetActive(true);
                    // print("L4 " + counti);
                }
            }

            #endregion

        }//end update

    void ShowLineL1(int score)
    {
        if (WinCount1 <= WinNum)
        {
            ScoreLine[score].SetActive(true);
        }
        // print("in show" + score);
    }
    void ShowLineL2(int score)
    {
        if (WinCount2 <= WinNum)
        {
            L2ScoreLine[score].SetActive(true);
        }
        // print("in show" + score);
    }
    void ShowLineL3(int score)
    {
        if (WinCount3 <= WinNum)
        {
            L3ScoreLine[score].SetActive(true);
        }
         print("in show333" + score);
    }

    void EndLevel1()
    {
        bool KK;
        print("Show Level1");
        KK = ShowPic(7, F2);
        EndL1 = true;
        stage++;
        WinCount1 = 0;
    }
    void EndLevel2()
    {
        bool KK;
        print("Show Level2");
        KK = ShowPic(4, F3);
        EndL2 = true;
        stage++;
        WinCount1 = 0;
    }
    void EndLevel3()
    {
        bool KK;
        print("Show Level3");
        KK = ShowPic(7, F4);
        EndL3 = true;
        stage++;
        //WinCount1 = 0;
        //
    }

    #region set Timer
    public static void SetTimer()
    {
        // Create a timer with a two second interval.

    }

    bool ShowPic(int num, GameObject[] WHI)
    {
        counti = 0;
        aTimer = new System.Timers.Timer(250);//delay 0.2s
        aTimer.Elapsed += (sender, args) => OnTimedEvent(sender, num, WHI);
        aTimer.AutoReset = true;
        aTimer.Enabled = true;

        return true;
    }

    public void OnTimedEvent(object source, int a, GameObject[] WHI)
    {
        counti++;
        if (counti >= a)
        {
            aTimer.Stop();
            aTimer.Dispose();
        }
    }
    #endregion

    // Update is called once per frame
    // create new raindrop
    public void FixedUpdate()
    {
        #region GoL1
        if (GoL1)
        {
            NowTime = Time.time;
            if (WinCount1 == WinNum && stage == 1)
            {
                //go to next level
                print("you win level ONE");
                EndLevel1();
               // GoL2 = true;
                // count = new_rain.Count + 1; //do not in the other if 
            }

            if (WinCount1 == WinNum && stage == 2)
            {
                //go to next level
                print("you win level ONE");
                EndLevel2();
               // GoL2 = true;
                // count = new_rain.Count + 1; //do not in the other if 
            }

            if (WinCount1 == WinNum && stage == 3)
            {
                //go to next level
                print("you win level ONE");
                EndLevel3();
               // GoL2 = true;
                // count = new_rain.Count + 1; //do not in the other if 
            }


            else if (count < new_rain.Count && WinCount1 < WinNum)
            {
                if (Math.Round(NowTime - StartTime, 1) == Math.Round(new_rain[count].releasetime, 1))
                {
                    if (new_rain[count].where == 'A')
                    {
                        RainCloneA = Instantiate(RainA, InstanceA.transform.position, gameObject.transform.rotation) as GameObject; count++;
                        queueA.Enqueue(RainCloneA);
                    }
                    else if (new_rain[count].where == 'B')
                    {
                        RainCloneB = Instantiate(RainB, InstanceB.transform.position, gameObject.transform.rotation) as GameObject; count++;
                        queueB.Enqueue(RainCloneB);
                    }
                    else if (new_rain[count].where == 'C')
                    {
                        RainCloneC = Instantiate(RainC, InstanceC.transform.position, gameObject.transform.rotation) as GameObject; count++;
                        queueC.Enqueue(RainCloneC);
                    }
                    else if (new_rain[count].where == 'D')
                    {
                        RainCloneD = Instantiate(RainD, InstanceD.transform.position, gameObject.transform.rotation) as GameObject; count++;
                        queueD.Enqueue(RainCloneD);
                    }
                    else if (new_rain[count].where == 'E')
                    {
                        RainCloneE = Instantiate(RainE, InstanceE.transform.position, gameObject.transform.rotation) as GameObject; count++;
                        queueE.Enqueue(RainCloneE);
                    }
                }
            }
            //song is end but score is less than 15
            else if (WinCount1 < WinNum && count >= new_rain.Count)
            {
                //replay this song again 
                StartTime = NowTime;
                count = 0;
            }
        }
        #endregion GoL1
        
        #region //GoL2 GoL3
        /*
        if (GoL2 && !GoL1)
        {
            //print("start Level2");
           // print("Outside " + Math.Round(NowTime - L1EndTime, 1) + "  " + Math.Round(new_rain2[count].releasetime, 1));
            NowTime = Time.time;
            if (WinCount2 == WinNum)
            {
                //go to next level
                print("you win level TWO");
                //L2EndTime = Math.Round(Time.time, 2);
                count = 0;
                GameObject[] DestroyG2;
                DestroyG2 = GameObject.FindGameObjectsWithTag("Raindrop");
                for (var i = 0; i < DestroyG2.Length; i++)
                {
                    if (DestroyG2[i].transform.position.x > -18.0)
                    {
                        Destroy(DestroyG2[i]);
                    }
                   
                }
                queueA.Clear(); queueB.Clear(); queueC.Clear(); queueD.Clear(); queueE.Clear();
                GoL2 = false;
                EndLevel2();
                count = 0;


                // count = new_rain.Count + 1; //do not in the other if 
            }
            else if (count < new_rain2.Count && WinCount2 < WinNum)
            {
               // print(Math.Round(NowTime - L1EndTime, 1) + "  " + Math.Round(new_rain2[count].releasetime, 1));
                if (Math.Round(NowTime - L1EndTime, 1) == Math.Round(new_rain2[count].releasetime, 1))
                {
                    if (new_rain2[count].where == 'A')
                    {
                        RainCloneA = Instantiate(RainA, InstanceA.transform.position, gameObject.transform.rotation) as GameObject; count++;
                        queueA.Enqueue(RainCloneA);
                    }
                    else if (new_rain2[count].where == 'B')
                    {
                        RainCloneB = Instantiate(RainB, InstanceB.transform.position, gameObject.transform.rotation) as GameObject; count++;
                        queueB.Enqueue(RainCloneB);
                    }
                    else if (new_rain2[count].where == 'C')
                    {
                        RainCloneC = Instantiate(RainC, InstanceC.transform.position, gameObject.transform.rotation) as GameObject; count++;
                        queueC.Enqueue(RainCloneC);
                    }
                    else if (new_rain2[count].where == 'D')
                    {
                        RainCloneD = Instantiate(RainD, InstanceD.transform.position, gameObject.transform.rotation) as GameObject; count++;
                        queueD.Enqueue(RainCloneD);
                    }
                    else if (new_rain2[count].where == 'E')
                    {
                        RainCloneE = Instantiate(RainE, InstanceE.transform.position, gameObject.transform.rotation) as GameObject; count++;
                        queueE.Enqueue(RainCloneE);
                    }
                }
            }
            //song is end but score is less than 15
            else if (WinCount2 < WinNum && count >= new_rain2.Count)
            {
                //replay this song again 
                L1EndTime = NowTime;
                count = 0;
            }
        }
        #endregion GoL2

        #region GoL3
        if (GoL3)
        {
           // print("start Level3");
            NowTime = Time.time;
            if (WinCount3 == WinNum)
            {
                //go to next level
                print("you win level THREE");
                //L2EndTime = Math.Round(Time.time, 2);
                GameObject[] DestroyG3;
                DestroyG3 = GameObject.FindGameObjectsWithTag("Raindrop");
                for (var i = 0; i < DestroyG3.Length; i++)
                {
                    if (DestroyG3[i].transform.position.x > -18.0)
                    {
                        Destroy(DestroyG3[i]);
                    }

                }
                queueA.Clear(); queueB.Clear(); queueC.Clear(); queueD.Clear(); queueE.Clear();
                GoL2 = false;
                GoL3 = false;
                EndLevel3();
                // count = new_rain.Count + 1; //do not in the other if 
            }
            else if (count < new_rain3.Count && WinCount3 < WinNum)
            {
                if (Math.Round(NowTime - L2EndTime, 1) == Math.Round(new_rain3[count].releasetime, 1))
                {
                    if (new_rain3[count].where == 'A')
                    {
                        RainCloneA = Instantiate(RainA, InstanceA.transform.position, gameObject.transform.rotation) as GameObject; count++;
                        queueA.Enqueue(RainCloneA);
                    }
                    else if (new_rain3[count].where == 'B')
                    {
                        RainCloneB = Instantiate(RainB, InstanceB.transform.position, gameObject.transform.rotation) as GameObject; count++;
                        queueB.Enqueue(RainCloneB);
                    }
                    else if (new_rain3[count].where == 'C')
                    {
                        RainCloneC = Instantiate(RainC, InstanceC.transform.position, gameObject.transform.rotation) as GameObject; count++;
                        queueC.Enqueue(RainCloneC);
                    }
                    else if (new_rain3[count].where == 'D')
                    {
                        RainCloneD = Instantiate(RainD, InstanceD.transform.position, gameObject.transform.rotation) as GameObject; count++;
                        queueD.Enqueue(RainCloneD);
                    }
                    else if (new_rain3[count].where == 'E')
                    {
                        RainCloneE = Instantiate(RainE, InstanceE.transform.position, gameObject.transform.rotation) as GameObject; count++;
                        queueE.Enqueue(RainCloneE);
                    }
                }
            }
            //song is end but score is less than 15
            else if (WinCount3 < WinNum && count >= new_rain2.Count)
            {
                //replay this song again 
                L2EndTime = NowTime;
                count = 0;
            }
        }*/
        #endregion GoL3 GL3
        
    }
}
