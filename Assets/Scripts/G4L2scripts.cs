using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;
//using System;


public class G4L2scripts : MonoBehaviour
{

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


    public double StartTime;
    public double NowTime;
    public double L2Endtime = 0;
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
    public int WinCount;
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
    public GameObject[] L2ScoreLine = new GameObject[16];
    public GameObject L2S0, L2S1, L2S2, L2S3, L2S4, L2S5, L2S6, L2S7, L2S8, L2S9, L2S10, L2S11, L2S12, L2S13, L2S14, L2S15;
    


    List<newraindrop> new_rain = new List<newraindrop>()        { 
            new newraindrop('A', 1.5f ),  new newraindrop('B', 3.0f),  new newraindrop('C', 5.4f),
            new newraindrop('D', 8.0f),  new newraindrop('E', 9.3f),  new newraindrop('D', 10.0f),
            new newraindrop('C', 11.5f), new newraindrop('B', 13.8f), new newraindrop('B', 14.3f),
            new newraindrop('A', 17.5f), new newraindrop('B', 18.7f), new newraindrop('C', 19.8f),
            new newraindrop('D', 21.2f), new newraindrop('E', 22.5f), new newraindrop('E', 23.0f) 
    };

    Queue queueA = new Queue();
    Queue queueB = new Queue();
    Queue queueC = new Queue();
    Queue queueD = new Queue();
    Queue queueE = new Queue();

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
        
      

    }

    //change color
    //caseB.GetComponent<Renderer>().material.color = Color.HSVToRGB(0.172f, 0.62f, 0.99f);
    void MyStart()
    {
        StartTime = Time.time;
        count = 0;
        WinCount = 0;
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

        //初始能量條
        L2ScoreLine[0] = L2S0; L2ScoreLine[1] = L2S1; L2ScoreLine[2] = L2S2; L2ScoreLine[3] = L2S3; L2ScoreLine[4] = L2S4; L2ScoreLine[5] = L2S5;
        L2ScoreLine[6] = L2S6; L2ScoreLine[7] = L2S7; L2ScoreLine[8] = L2S8; L2ScoreLine[9] = L2S9; L2ScoreLine[10] = L2S10; L2ScoreLine[11] = L2S11;
        L2ScoreLine[12] = L2S12; L2ScoreLine[13] = L2S13; L2ScoreLine[14] = L2S14; L2ScoreLine[15] = L2S15;
        for (int i = 0; i < 16; i++)
        {
            L2ScoreLine[i].SetActive(false);
        }
    }
    public void Update()
    {
        
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
        if (queueA.Count > 0)
        {
            GameObject newAobject = queueA.Peek() as GameObject;
            if (newAobject.transform.position.y < -2.8 && newAobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.A) || G2scripts.A))
            {

                print("yes u succed! A ");
                startTA = Time.time;
                WinCount++;
                ShowLine(WinCount);
                print("wincount is " + WinCount);
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
                print("what the fuck A");
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

                print("yes u succed! B ");
                startTB = Time.time;
                WinCount++;
                ShowLine(WinCount);
                print("wincount is " + WinCount);
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
                print("what the fuck B");
            }
        }

        if (queueC.Count > 0)
        {
            GameObject newCobject = queueC.Peek() as GameObject;
            if (newCobject.transform.position.y < -2.8 && newCobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.D) || G2scripts.C))
            {
                print("yes u succed! C ");
                startTC = Time.time;
                WinCount++;
                ShowLine(WinCount);
                print("wincount is " + WinCount);
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
                print("what the fuck C");
            }
        }

        if (queueD.Count > 0)
        {
            GameObject newDobject = queueD.Peek() as GameObject;
            if (newDobject.transform.position.y < -2.8 && newDobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.F) || G2scripts.D))
            {
                print("yes u succed! D ");
                startTD = Time.time;
                WinCount++;
                ShowLine(WinCount);
                print("wincount is " + WinCount);
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
            if (newEobject.transform.position.y < -2.8 && newEobject.transform.position.y > -4.1 && (Input.GetKeyDown(KeyCode.G) || G2scripts.E))
            {
                print("yes u succed! E ");
                startTE = Time.time;
                WinCount++;
                ShowLine(WinCount);
                print("wincount is " + WinCount);
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

    }//end update
    
    void ShowLine(int score)
    {
        L2ScoreLine[score].SetActive(true);
    }
        
    // Update is called once per frame
    public void FixedUpdate()
    {

        NowTime = Time.time;
        if (count < new_rain.Count)
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

    }
}

