using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
public class G2scripts : MonoBehaviour {

    public static char ChoosenBall;
    public static bool A, B, C, D, E;
    public GameObject Pa, Pb, Pc, Pd, Pe, Pwin, Pnon,Pwrong,Pnext,Pnext1;
	public GameObject La, Ld, Da, Db, Dc, Dd, De, Hand;
    public static int WINNUM = 13;
    public GameObject[] WIN = new GameObject[WINNUM];
   // public GameObject PALL1, PALL2, PALL3, PALL4;
    public Text Text;
    public static double positionX;
    public static double positionY;
    public static double positionZ;
    //public char[] First = { 'A', 'B', 'C', 'D', 'E' };
    public int whichF;
    public int nowKeep;
    public static int Min = 1;
    public static int Max = 6;
	public bool nowplaying;
	public float winwintime;

	public static int[] flowerlight_L0 = new int[3]; 
    public static int[] flowerlight_L1 = new int[4];
    public static int[] flowerlight_L2 = new int[6];
    public static int[] flowerlight_L3 = new int[8];

    public int showcount;
    public int playercount;
    public bool cantouch;
    public int currentlevel;
    public char Last;
	public bool play;

	public Animator animatorhand, animator2, animator3;


    public float KKKEEP;
    public bool tempkeep;

    //timer
    private static System.Timers.Timer aTimer;
    public int counti;
    public bool KK;

    //movie
    public GameObject PayMovie;
    public float PlayTime;
    public bool MovieOn;

    public GameObject NEXTL;

    //int frameRate = 250;

    // Use this for initialization
    void Start() {
        //  Time.captureFramerate = frameRate;
        
    	Time.fixedDeltaTime = 1.0f;
        Pa.SetActive(false);
        Pb.SetActive(false);
        Pc.SetActive(false);
        Pd.SetActive(false);
        Pe.SetActive(false);
        Pwin.SetActive(false);
        Pnon.SetActive(true);
        Pwrong.SetActive(false);
        Pnext.SetActive(false);
        Pnext1.SetActive(false);
        NEXTL.SetActive(false);

		animatorhand = Hand.GetComponent<Animator>();
		//print(animatorhand.GetInstanceID ());

		La.SetActive (false);Ld.SetActive (false);
		Da.SetActive (false);Db.SetActive (false);Dc.SetActive (false);Dd.SetActive (false);De.SetActive (false);Hand.SetActive (false);
        whichF = 0;
        nowKeep = 0;
        showcount = 0;
        playercount = 0;
        currentlevel = 0;

        Last = 'F';
		nowplaying = false;

        int temptnum;
        #region random
        System.Random randNum = new System.Random();


		for (int i = 0; i < flowerlight_L0.Length;)
		{
			temptnum = randNum.Next(Min, Max);
			if (i == 0)
			{
				flowerlight_L0[i] = 0;
				i++;
			}

			else if (i == 1) {
				flowerlight_L0[i] = 1;
				i++;
			}

			else if (i ==2) {
				flowerlight_L0[i] = 4;
				i++;
			}
		}

        for (int i = 0; i < flowerlight_L1.Length;)
        {
            temptnum = randNum.Next(Min, Max);
            if (i == 0)
            {
                flowerlight_L1[i] = 0;
                i++;
            }
           
            else if (temptnum != flowerlight_L1[i - 1])
            {
                flowerlight_L1[i] = temptnum;
                i++;
            }
        }

        for (int i = 0; i < flowerlight_L2.Length;)
        {
            temptnum = randNum.Next(Min, Max);
            if (i == 0)
            {
                flowerlight_L2[i] = 0;
                i++;
            }

            else if (temptnum != flowerlight_L2[i - 1])
            {
                flowerlight_L2[i] = temptnum;
                i++;
            }
        }

        for (int i = 0; i < flowerlight_L3.Length;)
        {
            temptnum = randNum.Next(Min, Max);
            if (i == 0)
            {
                flowerlight_L3[i] = 0;
                i++;
            }

            else if (temptnum != flowerlight_L3[i - 1])
            {
                flowerlight_L3[i] = temptnum;
                i++;
            }
        }
        #endregion
        cantouch = false;
        tempkeep = false;

        #region show WIN
        for (int i = 0; i < WINNUM; i++)
        {
            WIN[i].SetActive(false);
        }
        KK = false;
        #endregion
    }

    void FixedUpdate()
    {
		#region touch L0
		if (currentlevel == 0) {
			if (showcount <= flowerlight_L0.Length - 1)
			{
				//print("showcount " + showcount + " flower " + flowerlight_L1[showcount]);
				if (flowerlight_L0[showcount] == 0)
				{
					//showPicture('S');
					showcount++;
					//    print("in S");
				}
				else if (flowerlight_L0[showcount] == 1)
				{
					showPicture('A');
					showcount++;
				}

				else if (flowerlight_L0[showcount] == 2)
				{
					showPicture('B');
					showcount++;
				}

				else if (flowerlight_L0[showcount] == 3)
				{
					showPicture('C');
					showcount++;
				}

				else if (flowerlight_L0[showcount] == 4)
				{
					showPicture('D');
					showcount++;
				}

				else if (flowerlight_L0[showcount] == 5)
				{
					showPicture('E');
					showcount++;
				}
			}

			else if (showcount == flowerlight_L0.Length)
			{
				showPicture('N');
				showcount++;
				cantouch = true;
			}
		}
		#endregion
        #region touch L1
        if (currentlevel == 1) {
            if (showcount <= flowerlight_L1.Length - 1)
            {
                //print("showcount " + showcount + " flower " + flowerlight_L1[showcount]);
                if (flowerlight_L1[showcount] == 0)
                {
                    showPicture('S');
                    showcount++;
                //    print("in S");
                }
                else if (flowerlight_L1[showcount] == 1)
                {
                    showPicture('A');
                    showcount++;
                }

                else if (flowerlight_L1[showcount] == 2)
                {
                    showPicture('B');
                    showcount++;
                }

                else if (flowerlight_L1[showcount] == 3)
                {
                    showPicture('C');
                    showcount++;
                }

                else if (flowerlight_L1[showcount] == 4)
                {
                    showPicture('D');
                    showcount++;
                }

                else if (flowerlight_L1[showcount] == 5)
                {
                    showPicture('E');
                    showcount++;
                }
            }

            else if (showcount == flowerlight_L1.Length)
            {
                showPicture('N');
                showcount++;
                cantouch = true;
            }
        }
        #endregion
        #region touch L2
        else if (currentlevel == 2)
        {
            
            if (showcount <= flowerlight_L2.Length - 1)
            {
                print("showcount " + showcount + " flower " + flowerlight_L2[showcount]);
                if (flowerlight_L2[showcount] == 0)
                {
                    showPicture('S');
                    showcount++;
                    //    print("in S");
                }
                else if (flowerlight_L2[showcount] == 1)
                {
                    showPicture('A');
                    showcount++;
                }

                else if (flowerlight_L2[showcount] == 2)
                {
                    showPicture('B');
                    showcount++;
                }

                else if (flowerlight_L2[showcount] == 3)
                {
                    showPicture('C');
                    showcount++;
                }

                else if (flowerlight_L2[showcount] == 4)
                {
                    showPicture('D');
                    showcount++;
                }

                else if (flowerlight_L2[showcount] == 5)
                {
                    showPicture('E');
                    showcount++;
                }
            }

            else if (showcount == flowerlight_L2.Length)
            {
                showPicture('N');
                showcount++;
                cantouch = true;
            }
        }
        #endregion
        #region touch L3
        else if (currentlevel == 3)
        {
           
            if (showcount <= flowerlight_L3.Length - 1)
            {
                //print("showcount " + showcount + " flower " + flowerlight_L1[showcount]);
                if (flowerlight_L3[showcount] == 0)
                {
                    showPicture('S');
                    showcount++;
                    //    print("in S");
                }
                else if (flowerlight_L3[showcount] == 1)
                {
                    showPicture('A');
                    showcount++;
                }

                else if (flowerlight_L3[showcount] == 2)
                {
                    showPicture('B');
                    showcount++;
                }

                else if (flowerlight_L3[showcount] == 3)
                {
                    showPicture('C');
                    showcount++;
                }

                else if (flowerlight_L3[showcount] == 4)
                {
                    showPicture('D');
                    showcount++;
                }

                else if (flowerlight_L3[showcount] == 5)
                {
                    showPicture('E');
                    showcount++;
                }
            }

            else if (showcount == flowerlight_L3.Length)
            {
                showPicture('N');
                showcount++;
                cantouch = true;
            }
        }
        #endregion

    }
    

    void PlayLightBall(int ball)
    {
        #region check L1
        if (currentlevel == 1)
        {
            if (playercount == 0) { playercount++; }

            if (ball == flowerlight_L1[playercount] && playercount < flowerlight_L1.Length)
            {
                
                if (ball == 1)
                {
                    showPicture('A');
                }
                else if (ball == 2)
                {
                    showPicture('B');
                }
                else if (ball == 3)
                {
                    showPicture('C');
                }
                else if (ball == 4)
                {
                    showPicture('D');
                }
                else if (ball == 5)
                {
                    showPicture('E');
                }

                playercount++;
                //cantouch = true;

                if (playercount == flowerlight_L1.Length)
                {
                    print("finally 1");
                    NEXTL.SetActive(true);
                    //if (NEXTL.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f){

                    currentlevel++;
                    showcount = 0;
                    cantouch = false;
                    playercount = 0;
                    Last = 'F';
               // }
                }
            }

            else
            {
                print("type wrong bagayalo" + ball);
                Last = 'F';
                showPicture('W');
                //showcount = 0;
                //playercount = 0;
                //cantouch = false;
                
                KKKEEP = Time.time;
                tempkeep = true;
            }
        }
#endregion
        #region check L2
        else if (currentlevel == 2)
        {
            if (playercount == 0) { playercount++; }

            if (ball == flowerlight_L2[playercount] && playercount < flowerlight_L2.Length)
            {
                if (ball == 1)
                {
                    showPicture('A');
                    
                }
                else if (ball == 2)
                {
                    showPicture('B');
                    
                }
                else if (ball == 3)
                {
                    showPicture('C');
                    
                }
                else if (ball == 4)
                {
                    showPicture('D');
                   
                }
                else if (ball == 5)
                {
                    showPicture('E');
                    
                }

                playercount++;

                if (playercount == flowerlight_L2.Length)
                {
                    print("finally 2");
                    NEXTL.SetActive(true);
                    currentlevel++;
                    showcount = 0;
                    cantouch = false;
                    playercount = 0;
                    Last = 'F';
                }
            }

            else
            {
                print("type wrong baga");
                Last = 'F';
                showPicture('W');
                KKKEEP = Time.time;
                tempkeep = true;
            }
        }
        #endregion
        #region check L3
        else if (currentlevel == 3)
        {
            if (playercount == 0) { playercount++; }

            if (ball == flowerlight_L3[playercount] && playercount < flowerlight_L3.Length)
            {
                if (ball == 1)
                {
                    showPicture('A');
                    
                }
                else if (ball == 2)
                {
                    showPicture('B');
                    
                }
                else if (ball == 3)
                {
                    showPicture('C');
                    
                }
                else if (ball == 4)
                {
                    showPicture('D');
                    
                }
                else if (ball == 5)
                {
                    showPicture('E');
                    
                }

                playercount++;
                //cantouch = true;

                if (playercount == flowerlight_L3.Length)
                {
                    print("All Finish Congrats");
                    currentlevel++;
                    showcount = 0;
                    cantouch = false;
                    playercount = 0;
                    showWinPicture();
                    Last = 'F';
                }
            }

            else
            {
                print("type wrong bagayalo");
                Last = 'F';
                showPicture('W');
                //showcount = 0;
                //playercount = 0;
                //cantouch = false;
                KKKEEP = Time.time;
                tempkeep = true;
            }
        }
        #endregion
    }



	void PlayAnimation(){
		Da.SetActive (true);Db.SetActive (true);Dc.SetActive (true);Dd.SetActive (true);De.SetActive (true);
		Hand.SetActive(true);
        PlayTime = Time.time;
        MovieOn = true;
	}


    // Update is called once per frame
    void Update()
    {
        //SHOW MOVE
        if (MovieOn) {
          //  print(Math.Round(Time.time - PlayTime, 1));
            if (Math.Round(Time.time - PlayTime, 1) == 1.2) {
                showPicture('A');
                print("inshowA");
            }
            if (Math.Round(Time.time - PlayTime, 1) == 3.3) {
                showPicture('D');
                print("inshowD");
            }
        }

        if ((Time.time - KKKEEP) > 1.0f && tempkeep)
        {
            showcount = 0;
            playercount = 0;
            cantouch = false;
            tempkeep = false;
        }

        //SHOW WIN
        if (KK && counti<WINNUM && counti >=0)
        {
            WIN[counti].SetActive(true);
            Text.text = "counti" + counti;
        }
        else if (counti==WINNUM)
        {
        SceneManager.LoadScene("G2End", LoadSceneMode.Single);
        }
        
		#region if cantouch
        if (cantouch)
        {

			if(currentlevel == 0) {
				if(!nowplaying){
					PlayAnimation();
					winwintime = Time.time;
					nowplaying = true;
				}
			
				else if (Time.time-winwintime > 4.8 && currentlevel == 0){
					Da.SetActive (false);Db.SetActive (false);Dc.SetActive (false);Dd.SetActive (false);De.SetActive (false);
					Hand.SetActive(false);
					currentlevel++;
					showcount = 0;
					cantouch = false;
					playercount = 0;
					Last = 'F';
				}
			}

			else {
			//	print("level up!");

	            if ((A && Last != 'A') || Input.GetKeyDown(KeyCode.A))
	            {
	                Last = 'A';
	                PlayLightBall(1);
	            }
	            else if ((B && Last != 'B') || Input.GetKeyDown(KeyCode.B))
	            {
	                Last = 'B';
	                PlayLightBall(2);
	            }
	            else if ((C && Last != 'C') || Input.GetKeyDown(KeyCode.C))
	            {
	                Last = 'C';
	                PlayLightBall(3);
	            }

	            else if ((D && Last != 'D') || Input.GetKeyDown(KeyCode.D))
	            {
	                Last = 'D';
	                PlayLightBall(4);
	            }

	            else if ((E && Last != 'E') || Input.GetKeyDown(KeyCode.E))
	            {
	                Last = 'E';
	                PlayLightBall(5);
	            }

                //else if (!A && !B && !C && !D && !E)
                //{
                //    showPicture('N');
                //}
			}

           // print(Last);

            //實際測試要記得打開，要記得調Timer設定

            
        }
        #endregion

    }

    void showWinPicture()
    {
        Text.text = "you win";
        Pnon.SetActive(false);
        Pa.SetActive(false);
        Pb.SetActive(false);
        Pc.SetActive(false);
        Pd.SetActive(false);
        Pe.SetActive(false);
        Pwin.SetActive(false);
        KK = ShowPic(WINNUM+1, WIN);
    }

    void showPicture (char WH)
    {
        switch (WH)
        {
            case 'S':
                Text.text = "you're in next LEVEL";
                Pnon.SetActive(false);
                Pwrong.SetActive(false);
                Pa.SetActive(false);
                Pb.SetActive(false);
                Pc.SetActive(false);
                Pd.SetActive(false);
                Pe.SetActive(false);
                Pwin.SetActive(false);
                Pnext.SetActive(true);
                Pnext1.SetActive(true);
                NEXTL.SetActive(false);
                break;
            case 'A':
                Text.text = "A" + "X " + positionX + "Y " + positionY + "Z " + positionZ;
                Pnon.SetActive(false);
                Pwrong.SetActive(false);
                Pa.SetActive(true);
                Pb.SetActive(false);
                Pc.SetActive(false);
                Pd.SetActive(false);
                Pe.SetActive(false);
                Pwin.SetActive(false);
                Pnext.SetActive(false);
                Pnext1.SetActive(false);
                NEXTL.SetActive(false);
                break;
            case 'B':
                Text.text = "B" + "X " + positionX + "Y " + positionY + "Z " + positionZ;
                Pnon.SetActive(false);
                Pwrong.SetActive(false);
                Pa.SetActive(false);
                Pb.SetActive(true);
                Pc.SetActive(false);
                Pd.SetActive(false);
                Pe.SetActive(false);
                Pwin.SetActive(false);
                Pnext.SetActive(false);
                Pnext1.SetActive(false);
                NEXTL.SetActive(false);
                break;
            case 'C':
                Text.text = "C" + "X " + positionX + "Y " + positionY + "Z " + positionZ;
                Pnon.SetActive(false);
                Pwrong.SetActive(false);
                Pa.SetActive(false);
                Pb.SetActive(false);
                Pc.SetActive(true);
                Pd.SetActive(false);
                Pe.SetActive(false);
                Pwin.SetActive(false);
                Pnext.SetActive(false);
                Pnext1.SetActive(false);
                NEXTL.SetActive(false);
                break;
            case 'D':
                Text.text = "D" + "X " + positionX + "Y " + positionY + "Z " + positionZ;
                Pnon.SetActive(false);
                Pwrong.SetActive(false);
                Pa.SetActive(false);
                Pb.SetActive(false);
                Pc.SetActive(false);
                Pd.SetActive(true);
                Pe.SetActive(false);
                Pwin.SetActive(false);
                Pnext.SetActive(false);
                Pnext1.SetActive(false);
                NEXTL.SetActive(false);
                break;
            case 'E':
                Text.text = "E" + "X " + positionX + "Y " + positionY + "Z " + positionZ;
                Pnon.SetActive(false);
                Pwrong.SetActive(false);
                Pa.SetActive(false);
                Pb.SetActive(false);
                Pc.SetActive(false);
                Pd.SetActive(false);
                Pe.SetActive(true);
                Pwin.SetActive(false);
                Pnext.SetActive(false);
                Pnext1.SetActive(false);
                NEXTL.SetActive(false);
                break;
            case 'N':
                Text.text = "NON" + "X " + positionX + "Y " + positionY + "Z " + positionZ;
                Pnon.SetActive(true);
                Pwrong.SetActive(false);
                Pa.SetActive(false);
                Pb.SetActive(false);
                Pc.SetActive(false);
                Pd.SetActive(false);
                Pe.SetActive(false);
                Pwin.SetActive(false);
                Pnext.SetActive(false);
                Pnext1.SetActive(false);
                NEXTL.SetActive(false);
                break;
            case 'W':
                Pnon.SetActive(false);
                Pwrong.SetActive(true);
                Pa.SetActive(false);
                Pb.SetActive(false);
                Pc.SetActive(false);
                Pd.SetActive(false);
                Pe.SetActive(false);
                Pwin.SetActive(false);
                Pnext.SetActive(false);
                Pnext1.SetActive(false);
                NEXTL.SetActive(false);
                //KKKEEP = false;
                break;
        }
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

}
