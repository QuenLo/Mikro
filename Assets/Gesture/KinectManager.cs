using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Windows.Kinect;
using System.Text;
using System.Runtime.Serialization;





public class KinectManager : MonoBehaviour
{
    public Text GestureTextGameObject;
    public Text ConfidenceTextGameObject;
    public GameObject Player;
  //  private Turning turnScript;
   // private RRDD rrddScript;
    private G1scripts seedScript;


    // Kinect 
    private KinectSensor kinectSensor;

    //Coordinate mapper to map one type of point to another
    private CoordinateMapper coordinateMapper;

    // color frame and data 
    private ColorFrameReader colorFrameReader;
    private byte[] colorData;
    private Texture2D colorTexture;

    //Reader for body frames
    private BodyFrameReader bodyFrameReader;
    private int bodyCount;
    private Body[] bodies;

    private float[,] head_Zindex = new float[6,2];
    private float[,] bodyindex_id = new float[6, 2];



    float minposition_z = 8;
    public int choosen;

    private DepthFrameReader depthFrameReader;

    private FrameDescription depthFrameDescription;

    //private List<Tuple<JointType, JointType>> bones;

    //private WriteableBitmap depthBitmap;

    

    //private string BridgeName = "Bridge";
    //private string BridgeDownName = "Bridge_Down";
    //private string BridgeProgressName = "BridgeProgress";

    private readonly string DigName = "Dig";
    private readonly string SeedName = "Seed";
    private readonly string PutName = "Put";
    private readonly string RainName = "Rain";


    // GUI output
    private UnityEngine.Color[] bodyColors;
    //private string[] bodyText;

    /// <summary> List of gesture detectors, there will be one detector created for each potential body (max of 6) </summary>
    private List<GestureDetector> gestureDetectorList = null;

    // Use this for initialization
    void Start()
    {
        
       // turnScript = Player.GetComponent<Turning>();
       // rrddScript = Player.GetComponent<RRDD>();
        seedScript = Player.GetComponent<G1scripts>();

        // get the sensor object

        this.kinectSensor = KinectSensor.GetDefault();
        this.coordinateMapper = this.kinectSensor.CoordinateMapper;
        FrameDescription frameDescription = this.kinectSensor.DepthFrameSource.FrameDescription;

        if (this.kinectSensor != null)
        {
            this.bodyCount = this.kinectSensor.BodyFrameSource.BodyCount;
            //bodycount is always 6

            // color reader
            this.colorFrameReader = this.kinectSensor.ColorFrameSource.OpenReader();

            // create buffer from RGBA frame description
            var desc = this.kinectSensor.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Rgba);


            // body data    
            this.bodyFrameReader = this.kinectSensor.BodyFrameSource.OpenReader();

            // body frame to use
            this.bodies = new Body[this.bodyCount];

            // initialize the gesture detection objects for our gestures
            this.gestureDetectorList = new List<GestureDetector>();
            for (int bodyIndex = 0; bodyIndex < this.bodyCount; bodyIndex++)
            {
                //PUT UPDATED UI STUFF HERE FOR NO GESTURE
                //GestureTextGameObject.text = "none";
                //this.bodyText[bodyIndex] = "none";
                this.gestureDetectorList.Add(new GestureDetector(this.kinectSensor));
            }

            // start getting data from runtime
            this.kinectSensor.Open();
        }
        else
        {
            //kinect sensor not connected
        }
    }

    // Update is called once per frame
    void Update()
    {
        // process bodies
        bool newBodyData = false;
        using (BodyFrame bodyFrame = this.bodyFrameReader.AcquireLatestFrame())
        {
            if (bodyFrame != null)
            {
                bodyFrame.GetAndRefreshBodyData(this.bodies);
                newBodyData = true;
            }
        }

        if (newBodyData)
        {
            // update gesture detectors with the correct tracking id

            minposition_z = 8;
            for (int bodyIndex = 0; bodyIndex < this.bodyCount; bodyIndex++)
            {
                var body = this.bodies[bodyIndex];

                if (body != null)
                {
                    //var trackingId = this.bodies[choosen].TrackingId;
                    var trackingId = body.TrackingId;
                    //print("trackingID is " + trackingId);
                    //if (body.IsTracked)
                    //{
                    //    print("bodyindex is " + bodyIndex);
                    //    print("trackingidold! " + head_Zindex[bodyIndex, 1] + " BODYINDEX " + bodyIndex);
                    //}
                 //   print("true or false : " + this.gestureDetectorList[bodyIndex].IsPaused);

                    if (!this.gestureDetectorList[bodyIndex].IsPaused)
                    {
                        this.gestureDetectorList[bodyIndex].OnGestureDetected += CreateOnGestureHandler(bodyIndex);
                   //     print("detected go to gesture detected" + bodyIndex);
                       
                    }


                    if (trackingId != this.gestureDetectorList[bodyIndex].TrackingId)
                    {
                        //print("different!!! so please change");
                        this.gestureDetectorList[bodyIndex].TrackingId = trackingId;

                        // if the current body is tracked, unpause its detector to get VisualGestureBuilderFrameArrived events
                        // if the current body is not tracked, pause its detector so we don't waste resources trying to get invalid gesture results
                        //this.gestureDetectorList[bodyIndex].IsPaused = (trackingId == 0);
                        //this.gestureDetectorList[bodyIndex].OnGestureDetected += CreateOnGestureHandler(bodyIndex);
                    }

                    
                    // if the current body TrackingId changed, update the corresponding gesture detector with the new value
                    if (trackingId == 0)
                    {
                        // this bodyIndex isn't tracked
                        head_Zindex[bodyIndex, 0] = bodyIndex;
                        head_Zindex[bodyIndex, 1] = 0;

                        bodyindex_id[bodyIndex, 0] = bodyIndex;
                        bodyindex_id[bodyIndex, 1] = 0;
                    }
                    else
                    //already tracked!!
                    {
                    //    print("tracked " + bodyIndex);
                        CameraSpacePoint position_head = body.Joints[JointType.Head].Position;
                        head_Zindex[bodyIndex, 0] = bodyIndex;
                        head_Zindex[bodyIndex, 1] = position_head.Z;

                        bodyindex_id[bodyIndex, 0] = bodyIndex;
                        bodyindex_id[bodyIndex, 1] = (float)trackingId;
                    }
                }
            }

            for (int bodyIndex = 0; bodyIndex < this.bodyCount; bodyIndex++)
            {
                if (minposition_z > head_Zindex[bodyIndex, 1] && head_Zindex[bodyIndex, 1] != 0)
                {
                    choosen = bodyIndex;
                    minposition_z = head_Zindex[bodyIndex, 1];
                }
            }

            for (int bodyIndex = 0; bodyIndex < this.bodyCount; bodyIndex++)
            {
                if (bodyIndex != choosen)
                {
                    this.gestureDetectorList[bodyIndex].IsPaused = true;
                }

                else
                {
                    this.gestureDetectorList[bodyIndex].IsPaused = false;
                    //this.gestureDetectorList[bodyIndex].OnGestureDetected += CreateOnGestureHandler(bodyIndex);
                //    print("choosen is" + choosen);
                }
            }
        }
                /*
                     if (trackingId != this.gestureDetectorList[bodyIndex].TrackingId)
                        {
                            GestureTextGameObject.text = "none";
                            //this.bodyText[bodyIndex] = "none";
                            this.gestureDetectorList[bodyIndex].TrackingId = trackingId;

                            // if the current body is tracked, unpause its detector to get VisualGestureBuilderFrameArrived events
                            // if the current body is not tracked, pause its detector so we don't waste resources trying to get invalid gesture results
                            this.gestureDetectorList[bodyIndex].IsPaused = (trackingId == 0);
                            this.gestureDetectorList[bodyIndex].OnGestureDetected += CreateOnGestureHandler(bodyIndex);
                        }
                    }
                    */
                    /*

                    if(body.IsTracked)
                    {
                        CameraSpacePoint position_head = body.Joints[JointType.Head].Position;
                        print("tracked" + bodyIndex);
                        //print("gesturedetector.body is " + gestureDetectorList[bodyIndex].TrackingId);
                        //print("position_head" + position_head.Z);
                    }

                    */ 
    }


    


    private EventHandler<GestureEventArgs> CreateOnGestureHandler(int bodyIndex)
    {
        return (object sender, GestureEventArgs e) => OnGestureDetected(sender, e, bodyIndex);
    }
    
    /*
    public void onGestureDetectedProgress(float progress)
    {
        print("gesture progress detected!!");
        digScript.gestureprogress = progress;
    }
    */

    #region 測試姿勢有沒有符合
     void OnGestureDetected(object sender, GestureEventArgs e, int bodyIndex)
    {

      //  print("comehere");
        var isDetected = e.IsBodyTrackingIdValid && e.IsGestureDetected;

      //  print("gesturedetected is" + bodyIndex);
        //print("id is " + e.GestureID);
        //if (e.DetectionConfidence > 0.5f)
        //{
            ConfidenceTextGameObject.text = "confidence is " + e.DetectionConfidence + "id is " + e.GestureID;
        //}


        if (e.GestureID == RainName)
        {


        //    print("inside Rain");

            if (e.DetectionConfidence > 0.50f)
            {
                seedScript.RRain = true;
            }
            else
            {
                seedScript.RRain = false;
            }
        }

        if (e.GestureID == DigName)
        {


        //    print("inside Dig");

            if (e.DetectionConfidence > 0.60f)
            {
                seedScript.DDigging = true;
            }
            else
            {
                seedScript.DDigging = false;
            }
        }


        if (e.GestureID == SeedName)
        {

       //     print("inside seed");


            if (e.DetectionConfidence > 0.50f)
            {
                seedScript.SSeed = true;
            }
            else
            {
                seedScript.SSeed = false;
            }
        }

        if (e.GestureID == PutName)
        {

        //    print("inside put");

            if (e.DetectionConfidence > 0.50f)
            {
                seedScript.PPut = true;
            }
            else
            {
                seedScript.PPut = false;
            }
        }

        /*
        if (e.GestureID == "Bridge")
        {
            //NEW UI FOR GESTURE DETECTed
            //GestureTextGameObject.text = "Gesture Detected: " + e.GestureID;
            //StringBuilder text = new StringBuilder(string.Format("Gesture Detected? {0}\n", isDetected));
            ConfidenceTextGameObject.text = "Confidence: " + e.DetectionConfidence;
            //text.Append(string.Format("Confidence: {0}\n", e.DetectionConfidence));

            print("Bridge");
            //print("detectionconfidence" + e.DetectionConfidence + "gesturedetected is" + bodyIndex);
            //print("gesturedetected is" + bodyIndex);
            //print(this.gestureprogress);
            if (e.DetectionConfidence > 0.40f)
            {
                seedScript.RRain = true;
            }
            else
            {
                seedScript.RRain = false;
            }
        }
        */

        //if (e.GestureID == BridgeDownName)
        //{

        //    print("Bridge_Down");
        //    print(e.DetectionConfidence);

        //    if (e.DetectionConfidence > 0.30f)
        //    {
        //        seedScript.RRain = true;
        //    }
        //    else
        //    {
        //        seedScript.RRain = false;
        //    }
        //}

        /*
        if (e.GestureID == LiftHandName)
        {
            //NEW UI FOR GESTURE DETECTed
            GestureTextGameObject.text = "Gesture Detected: " + e.GestureID;
            //StringBuilder text = new StringBuilder(string.Format("Gesture Detected? {0}\n", isDetected));
            ConfidenceTextGameObject.text = "Confidence: " + e.DetectionConfidence;
            //text.Append(string.Format("Confidence: {0}\n", e.DetectionConfidence));

            print("lifthand");
            print(e.DetectionConfidence);
            //turnScript.lift = true;
            if (e.DetectionConfidence > 0.50f)
            {
                //turnScript.turnLeft = true;
                digScript.dig_true = true;
                print("dig_true change");
            }
            else
            {
                //turnScript.turnLeft = false;
                digScript.dig_true = false; 
                print("dosen't bigger than");
            }
         
        //print("lefthandoutout");
        }
        */
        /*if (e.GestureID == leanLeftGestureName)
        {
            //NEW UI FOR GESTURE DETECTed
            GestureTextGameObject.text = "Gesture Detected: " + e.GestureID;
            //StringBuilder text = new StringBuilder(string.Format("Gesture Detected? {0}\n", isDetected));
            ConfidenceTextGameObject.text = "Confidence: " + e.DetectionConfidence;
            //text.Append(string.Format("Confidence: {0}\n", e.DetectionConfidence));

            print("lean_left_gest");
            print(e.DetectionConfidence);
            if (e.DetectionConfidence > 0.65f)
            {
                print("haha");
                turnScript.turnLeft = true;
            }
            else
            {
                turnScript.turnLeft = false;
            }
       }//end if lean_left_gest
       */


        //this.bodyText[bodyIndex] = text.ToString();
    }
    #endregion 


    private void print(string v, float detectionConfidence)
    {
        throw new NotImplementedException();
    }

    //0 refereence ???
    /*
    private void OnRightLeanGestureDetected(object sender, GestureEventArgs e, int bodyIndex)
    {
        var isDetected = e.IsBody
        Valid && e.IsGestureDetected;

        //NEW UI FOR GESTURE DETECTed
        GestureTextGameObject.text = "Gesture Detected: " + isDetected;
        //StringBuilder text = new StringBuilder(string.Format("Gesture Detected? {0}\n", isDetected));
        ConfidenceTextGameObject.text = "Confidence: " + e.DetectionConfidence;
        //text.Append(string.Format("Confidence: {0}\n", e.DetectionConfidence));
        if (e.DetectionConfidence > 0.65f)
        {
            turnScript.turnRight = true;
        }
        else
        {
            turnScript.turnRight = false;
        }

        //this.bodyText[bodyIndex] = text.ToString();
    }
    */
    void OnApplicationQuit()
    {
        if (this.colorFrameReader != null)
        {
            this.colorFrameReader.Dispose();
            this.colorFrameReader = null;
        }

        if (this.bodyFrameReader != null)
        {
            this.bodyFrameReader.Dispose();
            this.bodyFrameReader = null;
        }

        if (this.kinectSensor != null)
        {
            if (this.kinectSensor.IsOpen)
            {
                this.kinectSensor.Close();
            }

            this.kinectSensor = null;
        }
    }

}
