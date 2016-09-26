using UnityEngine;
using System.Collections;

public class G5scripts : MonoBehaviour {

    public GameObject[] cristal_stage = new GameObject[13];
    public GameObject crystal1, crystal4, crystal5, crystal6, crystal7, illiwa;
    
    private Animator A, B, C, D, E, girl;
    private Animator SparkA, SparkB, SparkC, SparkD, SparkE;
    public static char WHatNow;
    float Keep;
    public AudioSource match;
    // Use this for initialization
    void Start()
    {
        print("3->4(C)->1(A)->5(E)->7(B)->6(D)");
        ShowCrystal(0);
        girl = illiwa.GetComponent<Animator>();
        A = crystal1.GetComponent<Animator>();
        C = crystal4.GetComponent<Animator>();
        E = crystal5.GetComponent<Animator>();
        D = crystal6.GetComponent<Animator>();
        B = crystal7.GetComponent<Animator>();
        SparkC = cristal_stage[2].GetComponent<Animator>();
        SparkA = cristal_stage[4].GetComponent<Animator>();
        SparkE = cristal_stage[6].GetComponent<Animator>();
        SparkB = cristal_stage[10].GetComponent<Animator>();
        SparkD = cristal_stage[12].GetComponent<Animator>();



        WHatNow = 'G';
        Time.fixedDeltaTime = 0.1f;
    }

    void ShowCrystal(int WhSC)
    {
        for (int i = 0; i < 13; i++) {
            cristal_stage[i].SetActive(false);
            if (i == WhSC)
            {
                cristal_stage[i].SetActive(true);
                print("show " + WhSC);
            }
            //else
            //{
            //    cristal_stage[i].SetActive(false);
            //}
        }
    }

    void ShowWrong(int Which)
    {
        if (Input.GetKeyDown(KeyCode.A) || G2scripts.A)
        {
            A.SetBool("Not", true);
        }
        else if (Input.GetKeyDown(KeyCode.B) || (G2scripts.B))
        {
            B.SetBool("Not", true);
        }
        else if (Input.GetKeyDown(KeyCode.C) || G2scripts.C) 
        {
            C.SetBool("Not", true);
        }
        else if (Input.GetKeyDown(KeyCode.D) || G2scripts.D)
        {
            D.SetBool("Not", true);
        }
        else if (Input.GetKeyDown(KeyCode.E) || G2scripts.E)
        {
            E.SetBool("Not", true);
        }
        ShowCrystal(Which);
    }

    //3->4(C)->1(A)->5(E)->7(B)->6(D)
    //記得調整Time->0.1秒一次
    void FixedUpdate() {
        bool bA = (G2scripts.A || Input.GetKeyDown(KeyCode.A));
        bool bB = (G2scripts.B || Input.GetKeyDown(KeyCode.B));
        bool bC = (G2scripts.C || Input.GetKeyDown(KeyCode.C));
        bool bD = (G2scripts.D || Input.GetKeyDown(KeyCode.D));
        bool bE = (G2scripts.E || Input.GetKeyDown(KeyCode.E));

        #region 打開用這個
        AnimatorStateInfo infoGirl = girl.GetCurrentAnimatorStateInfo(0);
        //AnimatorStateInfo infoA = A.GetCurrentAnimatorStateInfo(0);
        //AnimatorStateInfo infoB = B.GetCurrentAnimatorStateInfo(0);
        //AnimatorStateInfo infoC = C.GetCurrentAnimatorStateInfo(0);
        //AnimatorStateInfo infoD = D.GetCurrentAnimatorStateInfo(0);
        //AnimatorStateInfo infoE = E.GetCurrentAnimatorStateInfo(0);

        if (infoGirl.normalizedTime >= 1f && WHatNow == 'G')
        {
            //print(infoGirl.ToString());
            ShowCrystal(2);
            WHatNow = 'C';
        }
        #region C(1)
        else if (WHatNow == 'C')
        {

            if (bC)
            {
                C.SetBool("FlyC", true);
                Keep = Time.time;
                match.Play();                
                print("C of 4 " + C.GetBool("FlyC") + Keep);
            }

            else if ((bB || bA || bD || bE) && !C.GetBool("FlyC"))
            {
                ShowWrong(2);
                Keep = Time.time;
                //  print("else");
            }
            else if (!C.GetBool("FlyC"))
            {
                Keep = Time.time;
            }
            //print(Time.time - Keep);
            if (Time.time - Keep >= 2.01)
            {
                WHatNow = 'A';
                print("to A");
            }
        }
        #endregion
        #region A(2)
        else if (WHatNow == 'A')
        {

            if (bA && !A.GetBool("FlyA"))
            {
                A.SetBool("FlyA", true);
                print("A" + match.isPlaying.ToString());
                match.Play();
                Keep = Time.time;
                print("A of 4 " + A.GetBool("FlyA") + Keep);
            }

            else if ((bB || bC || bD || bE ) && !A.GetBool("FlyA"))
            {
                ShowWrong(4);
                Keep = Time.time;
                // print("else");
            }
            else if (!A.GetBool("FlyA"))
            {
                Keep = Time.time;
            }
            //print(Time.time - Keep);
            if (Time.time - Keep >= 2.10f)
            {
                WHatNow = 'E';
                print("to E");
            }

        }
        #endregion
        #region E(3)
        else if (WHatNow == 'E')
        {

            if (bE && !E.GetBool("FlyE"))
            {
                E.SetBool("FlyE", true);
                print("E" + match.isPlaying.ToString());
                match.Play();
                Keep = Time.time;
                print("E of 4 " + E.GetBool("FlyE") + Keep);
            }

            else if ((bB || bC || bD || bA ) && !E.GetBool("FlyE"))
            {
                ShowWrong(6);
                Keep = Time.time;
                // print("else");
            }
            else if (!E.GetBool("FlyE"))
            {
                Keep = Time.time;
            }
            //print(Time.time - Keep);
            if (Time.time - Keep >= 2.10f)
            {
                WHatNow = 'B';
                print("to B");
            }

        }
        #endregion
        #region B(4)
        else if (WHatNow == 'B')
        {

            if (bB && !B.GetBool("FlyB"))
            {
                B.SetBool("FlyB", true);
                match.Play();
                print("B" + match.isPlaying.ToString());
                Keep = Time.time;
                print("B of 4 " + B.GetBool("FlyB") + Keep);
            }

            else if ((bA || bC || bD || bE) && !B.GetBool("FlyB"))
            {
                ShowWrong(8);
                Keep = Time.time;
                //  print("else");
            }
            else if (!B.GetBool("FlyB"))
            {
                Keep = Time.time;
            }
           // print(Time.time - Keep);
            if (Time.time - Keep >= 2.10f)
            {
                WHatNow = 'D';
                print("in");
            }

        }
        #endregion
        #region D(5)
        else if (WHatNow == 'D')
        {

            if (bD && !D.GetBool("FlyD"))
            {
                D.SetBool("FlyD", true);
                match.Play();
                print("D" + match.isPlaying.ToString());
                Keep = Time.time;
                print("D of 4 " + D.GetBool("FlyD") + Keep);
            }

            else if ((bB || bC || bA || bE ) && !D.GetBool("FlyD"))
            {
                ShowWrong(10);
                Keep = Time.time;
                //   print("else");
            }
             else if (!D.GetBool("FlyD"))
            {
                Keep = Time.time;
            }
           // print(Time.time - Keep);
            if (Time.time - Keep >= 2.10f)
            {
                WHatNow = 'W';
                print("in");
            }

        }
        #endregion

        if (Input.GetKeyDown(KeyCode.N))
        {
            C.SetBool("Not", true);
            B.SetBool("Not", true);
            E.SetBool("Not", true);
            D.SetBool("Not", true);
            A.SetBool("Not", true);
            //C.SetBool("NotC", false);
            // ShowCrystal(4);
            print("C of 4 " + C.GetBool("Not"));
        }
        #endregion
    }

    //3->4(C)->1(A)->5(E)->7(B)->6(D)
    void Update() {
        bool bA = (G2scripts.A || Input.GetKeyDown(KeyCode.A));
        bool bB = (G2scripts.B || Input.GetKeyDown(KeyCode.B));
        bool bC = (G2scripts.C || Input.GetKeyDown(KeyCode.C));
        bool bD = (G2scripts.D || Input.GetKeyDown(KeyCode.D));
        bool bE = (G2scripts.E || Input.GetKeyDown(KeyCode.E));

#region 更新動畫
       // if(SparkD.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f){

       // }

#endregion

        #region 預防萬一用
        AnimatorStateInfo infoGirl = girl.GetCurrentAnimatorStateInfo(0);
        //AnimatorStateInfo infoA = A.GetCurrentAnimatorStateInfo(0);
        //AnimatorStateInfo infoB = B.GetCurrentAnimatorStateInfo(0);
        //AnimatorStateInfo infoC = C.GetCurrentAnimatorStateInfo(0);
        //AnimatorStateInfo infoD = D.GetCurrentAnimatorStateInfo(0);
        //AnimatorStateInfo infoE = E.GetCurrentAnimatorStateInfo(0);

        if (infoGirl.normalizedTime >= 1f && WHatNow == 'G')
        {
            //print(infoGirl.ToString());
            ShowCrystal(2);
            WHatNow = 'C';
        }
        #region C(1)
        else if (WHatNow == 'C')
        {

            if (bC)
            {
                C.SetBool("FlyC", true);
                match.Play();
                Keep = Time.time;
                print("C of 4 " + C.GetBool("FlyC") + Keep);
            }

            else if ((bB || bA || bD || bE) && !C.GetBool("FlyC"))
            {
                ShowWrong(2);
                Keep = Time.time;
                //  print("else");
            }
            else if (!C.GetBool("FlyC"))
            {
                Keep = Time.time;
            }
            //print(Time.time - Keep);
            if (Time.time - Keep >= 2.01)
            {
                WHatNow = 'A';
                print("to A");
            }
        }
        #endregion
        #region A(2)
        else if (WHatNow == 'A')
        {

            if (bA && !A.GetBool("FlyA"))
            {
                A.SetBool("FlyA", true);
                match.Play();
                Keep = Time.time;
                print("A of 4 " + A.GetBool("FlyA") + Keep);
            }

            else if ((bB || bC || bD || bE ) && !A.GetBool("FlyA"))
            {
                ShowWrong(4);
                Keep = Time.time;
                // print("else");
            }
            else if (!A.GetBool("FlyA"))
            {
                Keep = Time.time;
            }
            //print(Time.time - Keep);
            if (Time.time - Keep >= 2.10f)
            {
                WHatNow = 'E';
                print("to E");
            }

        }
        #endregion
        #region E(3)
        else if (WHatNow == 'E')
        {

            if (bE && !E.GetBool("FlyE"))
            {
                E.SetBool("FlyE", true);
                match.Play();
                Keep = Time.time;
                print("E of 4 " + E.GetBool("FlyE") + Keep);
            }

            else if ((bB || bC || bD || bA ) && !E.GetBool("FlyE"))
            {
                ShowWrong(6);
                Keep = Time.time;
                // print("else");
            }
            else if (!E.GetBool("FlyE"))
            {
                Keep = Time.time;
            }
           // print(Time.time - Keep);
            if (Time.time - Keep >= 2.10f)
            {
                WHatNow = 'B';
                print("to B");
            }

        }
        #endregion
        #region B(4)
        else if (WHatNow == 'B')
        {

            if (bB && !B.GetBool("FlyB"))
            {
                B.SetBool("FlyB", true);
                match.Play();
                Keep = Time.time;
                print("B of 4 " + B.GetBool("FlyB") + Keep);
            }

            else if ((bA || bC || bD || bE) && !B.GetBool("FlyB"))
            {
                ShowWrong(8);
                Keep = Time.time;
                //  print("else");
            }
            else if (!B.GetBool("FlyB"))
            {
                Keep = Time.time;
            }
           // print(Time.time - Keep);
            if (Time.time - Keep >= 2.10f)
            {
                WHatNow = 'D';
                print("in");
            }

        }
        #endregion
        #region D(5)
        else if (WHatNow == 'D')
        {

            if (bD && !D.GetBool("FlyD"))
            {
                D.SetBool("FlyD", true);
                match.Play();
                Keep = Time.time;
                print("D of 4 " + D.GetBool("FlyD") + Keep);
            }

            else if ((bB || bC || bA || bE ) && !D.GetBool("FlyD"))
            {
                ShowWrong(10);
                Keep = Time.time;
                //   print("else");
            }
             else if (!D.GetBool("FlyD"))
            {
                Keep = Time.time;
            }
          //  print(Time.time - Keep);
            if (Time.time - Keep >= 2.10f)
            {
                WHatNow = 'W';
                print("in");
            }

        }
        #endregion

        if (Input.GetKeyDown(KeyCode.N))
        {
            C.SetBool("Not", true);
            B.SetBool("Not", true);
            E.SetBool("Not", true);
            D.SetBool("Not", true);
            A.SetBool("Not", true);
            //C.SetBool("NotC", false);
            // ShowCrystal(4);
            print("C of 4 " + C.GetBool("Not"));
        }
        #endregion
    }

}
