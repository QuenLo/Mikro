using UnityEngine;
using System.Collections;
using System.Threading;
using System.Timers;
using System;
using UnityEngine.SceneManagement;



public class G1scripts : MonoBehaviour
{
    //public GameObject Bullet;
    //public GameObject PlayMovie;
    public bool SSeed;
    public bool RRain;
    public bool DDigging;
    public bool PPut;
    public bool GoSeed, GoPut, GoRain;
    public GameObject STARTPic;
    public GameObject[] A1_2 = new GameObject[7];
    public GameObject A1_2_1, A1_2_2, A1_2_3, A1_2_4, A1_2_5, A1_2_6, A1_2_7; //Seed
    public GameObject[] A1_4 = new GameObject[11];
    public GameObject A1_4_1, A1_4_2, A1_4_3, A1_4_4, A1_4_5, A1_4_6; //Rain
    public GameObject[] A1_1 = new GameObject[4];
    public GameObject A1_1_1, A1_1_2, A1_1_3, A1_1_4;//Digging
    public GameObject[] A1_3 = new GameObject[5];
    public GameObject A1_3_1, A1_3_2, A1_3_3, A1_3_4, A1_3_5;//Put
    public GameObject[] A1_5 = new GameObject[5];
    public GameObject A1_5_1, A1_5_2, A1_5_3, A1_5_4;//WON
    private bool RKeep;
    private bool DKeep;
    private bool PKeep;
    private bool SKeep;
    private bool WKeep;
    //timer
    private static System.Timers.Timer aTimer;
    public int counti;

    //說明
    public GameObject[] A_RM = new GameObject[5];
    public GameObject A0_RM,A1_RM, A2_RM, A3_RM, A4_RM;//RM

    //fade
    public SpriteRenderer sprite_RM;
    public SpriteRenderer sprite_BK;
    public float STARTTime;
    public bool StartEnd;
    public bool goHOW;
    public GameObject HOWPic;
    public GameObject A1;
    
    public AudioSource BKmusic;

    // Use this for initialization
    void Start()
    {
        
        #region 初始化畫面
        A_RM[0] = A1_RM;A_RM[1] = A2_RM;A_RM[2] = A3_RM;A_RM[3] = A4_RM;A_RM[4] = A0_RM;
        for (int i = 0; i < 4; i++)
        {
            A_RM[i].SetActive(false);
        }
        A1_1[0] = A1_1_1; A1_1[1] = A1_1_2; A1_1[2] = A1_1_3; A1_1[3] = A1_1_4;
        for (int i = 0; i < 4; i++)
        {
            A1_1[i].SetActive(false);
        }
        A1_2[0] = A1_2_1; A1_2[1] = A1_2_2; A1_2[2] = A1_2_3; A1_2[3] = A1_2_4; A1_2[4] = A1_2_5; A1_2[5] = A1_2_6; A1_2[6] = A1_2_7;
        for (int i = 0; i < 7; i++)
        {
            A1_2[i].SetActive(false);
        }
        A1_3[0] = A1_3_1; A1_3[1] = A1_3_2; A1_3[2] = A1_3_3; A1_3[3] = A1_3_4; A1_3[4] = A1_3_5;
        for (int i = 0; i < 5; i++)
        {
            A1_3[i].SetActive(false);
        }
        A1_4[0] = A1_4_1; A1_4[1] = A1_4_2; A1_4[2] = A1_4_3; A1_4[3] = A1_4_4; A1_4[4] = A1_4_5; A1_4[5] = A1_4_6;
        for (int i = 0; i < 6; i++)
        {
            A1_4[i].SetActive(false);
        }
        
        A1_5[0] = A1_5_1; A1_5[1] = A1_5_2; A1_5[2] = A1_5_3; A1_5[3] = A1_5_4;
        for (int i = 0; i < 4; i++)
        {
            A1_5[i].SetActive(false);
        }
        
        #endregion 初始化畫面
        RKeep = false;
        DKeep = false;
        PKeep = false;
        SKeep = false;

        GoPut = false;
        GoRain = false;
        GoSeed = false;

        STARTTime = Time.time;
        StartEnd = false;
        goHOW = true;
        A1.SetActive(false);
        goHOW = true;
       
        HOWPic.SetActive(true);
        BKmusic.PlayDelayed(2.5f);
        BKmusic.volume = 0.5f; 
    }

    //dig->dees->put->rain
    // Update is called once per frame

    void showRM(int SHOW)
    {
        for (int i = 0; i < 5; i++)
        {
            if (i == SHOW)
            {
                A_RM[i].SetActive(true);
                print("in i" + i);
            }
            else
            {
                A_RM[i].SetActive(false);
            }
        }
    }

    void Update()
    {
        //遊戲開始要按space
        //if (Input.GetKeyDown(KeyCode.Space) && !goHOW) {
        //    goHOW = true;
        //    STARTTime = Time.time;
        //    HOWPic.SetActive(true);
        //    STARTPic.SetActive(false);
        //}
        //*****實測記得打開!!!!
    	//if((Time.time - STARTTime) >= 27.0f && !goHOW)
    	//{
    	//	STARTTime = Time.time;
    	//	HOWPic.SetActive(true);
    	//	PlayMovie.SetActive(false);
    	//	A1.SetActive(false);
    	//	goHOW = true;
    	//}
    	//else{
    	//	//A1.SetActive(false);
    	//	A1_RM.SetActive(false);
    	//	print("in");
    	//}

        if ((Time.time - STARTTime) < 10.0f && goHOW)
        {
           // BKmusic.Play();
        	A1.SetActive(true);
            sprite_RM.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(6f, 0f, ((Time.time - STARTTime) / 15)));
           // sprite_BK.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(0.5f, 1f, ((Time.time - STARTTime) / 5)));
        }
        if(goHOW && Input.GetKeyDown(KeyCode.Tab))
        {
        	//A1.SetActive(true);
            StartEnd = true;
            showRM(0);
            print("in next");
            BKmusic.volume = 1.0f;
        }

        //print(!DKeep);
        //DIG
        if ( StartEnd && (Input.GetKeyDown(KeyCode.D) || DDigging) && !DKeep )
        {
            startDig();
        }
        if (DKeep && counti < 4)
        {
            A1_1[counti].SetActive(true);
            //SKeep = true;
        }
        else if (counti == 4)
        {
            //print("dig in " + counti);
            GoSeed = true;
            showRM(1);
        }

        //SEED
        if ( (Input.GetKeyDown(KeyCode.S) || SSeed)  && DKeep && !SKeep && GoSeed)
        {
            startSeed();

        }
        if (DKeep && SKeep && counti < 7)
        {
            A1_2[counti].SetActive(true);
          // PKeep = true;
        }
        else if (counti == 7)
        {
           // print("seed in " + counti);
            GoPut = true;
            showRM(2);
        }

        //PUT
        if ( (Input.GetKeyDown(KeyCode.P) || PPut ) && DKeep && SKeep && !PKeep && GoPut)
        {
            startPut();
        }
        if (DKeep && SKeep && PKeep && counti < 5)
        {
            A1_3[counti].SetActive(true);
        }
        else if (GoPut && counti == 5)
        {
            //print("put in " + counti);
            GoRain = true;
            showRM(3);
        }

        //RAIN
        if ((Input.GetKeyDown(KeyCode.R) || RRain ) && DKeep && SKeep && PKeep && !RKeep && GoRain)
        {
            startRain();
        }
        if (DKeep && SKeep && PKeep && RKeep && counti <= 9)
        {
            if (counti == 6)
            {
                startWIN();
            }
            else
            {
                A1_4[counti].SetActive(true);
            }
        } 	

        //win
        if (WKeep && counti < 4)
        {
            A1_5[counti].SetActive(true);
        }
        else if(WKeep && counti == 5){
           // BKmusic.volume = 0.5f;
            print("inNext");
            SceneManager.LoadScene("G1End", LoadSceneMode.Single);
           print("inNext");
        }
    }
    #region 反應動作
    void startDig()
    {
        bool KK;
        print("digging ON \n");
        KK = ShowPic(4, A1_1);
        if (KK)
        {
            DKeep = true;
            print("in digging" + counti);
        }
        else { DKeep = false; }

    }
    void startSeed()
    {
        print("SEED ON \n");
        bool KK;
        KK = ShowPic(7, A1_2);
        if (KK)
        {
            SKeep = true;
        }
        else { SKeep = false; }
    }
    void startPut()
    {
        print("put ON \n");
        bool KK;
        KK = ShowPic(5, A1_3);
        if (KK)
        {
            PKeep = true;
        }
        else { PKeep = false; }
    }
    void startRain()
    {
        print("RAIN ON \n");
        bool KK;
        KK = ShowPic(6, A1_4);
        if (KK)
        {
            RKeep = true;
        }
        else { RKeep = false; }
    }
    void startWIN()
    {
        print("WIN WIN \n");
        bool KK;
        KK = ShowPic(5, A1_5);
        if (KK)
        {
            WKeep = true;
        }
        else { WKeep = false; }
    }
    #endregion 反應動作


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

    public void OnTimedEvent(object source,int a,GameObject[] WHI)
    {
        counti++;
        if (counti >= a)
        {
            aTimer.Stop();
            aTimer.Dispose();
        }
    }
    #endregion






}
