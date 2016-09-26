using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class G3scripts : MonoBehaviour {
    public bool Bridge_right;
    public bool Bridge_left;
    public bool dig_true;
    public GameObject LeftBridge;
    public GameObject LeftBridge_S1;
    public GameObject LeftBridge_S2;
    public GameObject LeftBridge_S3;
    public GameObject RightBridge;
    public GameObject RightBridge_S1;
    public GameObject RightBridge_S2;
    private bool LBKeep;
    private bool RBKeep;
    public float gestureprogress;

    //fade
    public float minimum = 0.0f;
    public float maximum = 1f;
    public float duration = 3.0f;
    private float startTimeL;
    private float startTimeR;
    public SpriteRenderer sprite_L;
    public SpriteRenderer sprite_LS1;
    public SpriteRenderer sprite_LS2;
    public SpriteRenderer sprite_LS3;
    public SpriteRenderer sprite_R;
    public SpriteRenderer sprite_RS1;
    public SpriteRenderer sprite_RS2;

    // Use this for initialization
    void Start () {
        LeftBridge.SetActive(false);
        LeftBridge_S1.SetActive(false);
        LeftBridge_S2.SetActive(false);
        LeftBridge_S3.SetActive(false);
        LBKeep = false;
        RightBridge.SetActive(false);
        RightBridge_S1.SetActive(false);
        RightBridge_S2.SetActive(false);
        RBKeep = false;
        
        startTimeL = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
       
        //Bridge_left
        if (( Bridge_left || Input.GetKeyDown(KeyCode.L) ) && !LBKeep)
        {
            LeftBridge.SetActive(true);
            LeftBridge_S1.SetActive(true);
            LeftBridge_S2.SetActive(true);
            LeftBridge_S3.SetActive(true);
            LBKeep = true;
            startTimeL = Time.time;
            
           // print("lefttrue");
            //print(gestureprogress);
        }
        
        if (LBKeep)
        {
            float t = (Time.time - startTimeL) / duration;
            sprite_L.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(minimum, maximum, t));
            sprite_LS1.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(-3.0f, maximum, t));
            sprite_LS2.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(-5.0f, maximum, t));
            sprite_LS3.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(-5.0f, maximum, t));
        }
       

        //Bridge_right
        if ( (Bridge_right || Input.GetKeyDown(KeyCode.R) ) && !RBKeep)
        {
            RightBridge.SetActive(true);
            RightBridge_S1.SetActive(true);
            RightBridge_S2.SetActive(true);
            RBKeep = true;
            startTimeR = Time.time;
           // print("righttrue");
            //print(gestureprogress);
        }
        if (RBKeep)
        {
            float t2 = (Time.time - startTimeR) / duration;
            //print(t2);
            sprite_R.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(minimum, maximum, t2));
            sprite_RS1.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(-3.0f, maximum, t2));
            sprite_RS2.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(-5.0f, maximum, t2));
        }
        if (Input.GetKeyDown(KeyCode.N) ||( RBKeep && LBKeep && Time.time >=startTimeL+4.0f && Time.time >=startTimeR+4.0f))
        {
            SceneManager.LoadScene("G3End", LoadSceneMode.Single);
        }
    }
}
