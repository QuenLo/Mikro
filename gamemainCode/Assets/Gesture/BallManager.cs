using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Windows.Kinect;
using System.Text;
using System.Runtime.Serialization;


public class BallManager: MonoBehaviour
{
    //public Text GestureTextGameObject;
    //public Text ConfidenceTextGameObject;
    //public Text Text;

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

    private float[,] head_Zindex = new float[6, 2];
    private float[,] bodyindex_id = new float[6, 2];



    float minposition_z = 8;
    public int choosen;

    private DepthFrameReader depthFrameReader;

    private FrameDescription depthFrameDescription;

    //private List<Tuple<JointType, JointType>> bones;

    //private WriteableBitmap depthBitmap;

    // GUI output
    private UnityEngine.Color[] bodyColors;
    //private string[] bodyText;

    /// <summary> List of gesture detectors, there will be one detector created for each potential body (max of 6) </summary>
    private List<GestureDetector> gestureDetectorList = null;

    // Use this for initialization
    void Start()
    {

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
                    if (body.IsTracked)
                    {
                        //print("bodyindex is " + bodyIndex);
                        //print("trackingidold! " + head_Zindex[bodyIndex, 1] + " BODYINDEX " + bodyIndex);
                    }
                    //print("min" + minposition_z);
                    //print("true or false : " + this.gestureDetectorList[bodyIndex].IsPaused);

                    if (trackingId != this.gestureDetectorList[bodyIndex].TrackingId)
                    {
                        print("different!!! so please change");
                        this.gestureDetectorList[bodyIndex].TrackingId = trackingId;

                        // if the current body is tracked, unpause its detector to get VisualGestureBuilderFrameArrived events
                        // if the current body is not tracked, pause its detector so we don't waste resources trying to get invalid gesture results
                        this.gestureDetectorList[bodyIndex].IsPaused = (trackingId == 0);
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
                        print("tracked " + bodyIndex);
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
                }
            }

            //print("choosen us + " + choosen);
            if (this.bodies[choosen].IsTracked)
            {
                CameraSpacePoint right_hand = this.bodies[choosen].Joints[JointType.HandRight].Position;
                CameraSpacePoint left_hand = this.bodies[choosen].Joints[JointType.HandLeft].Position;

                double x1 = Math.Round(right_hand.X, 3);
                double y1 = Math.Round(right_hand.Y, 3);
                double z1 = Math.Round(right_hand.Z, 3);


                //print("choosen is " + choosen);
                //print("right hand position: X" + x1 + " y" + y1);

                G2scripts.positionX = x1;
                G2scripts.positionY = y1;
                G2scripts.positionZ = z1;
            if(z1 <= 1.8 && z1 > 1.6){
                if (y1 <= -0.65 && y1 > -0.83)
                {
                    if (x1 <= -0.9 && x1 > -1.2)
                    {
                        print("A");
                        G2scripts.ChoosenBall = 'A';
                        //G2scriptsscript.ChoosenBall = 'C';
                        // G2scripts.positionX = x1;
                        G2scripts.A = true;
                        G2scripts.B = false;
                        G2scripts.C = false;
                        G2scripts.D = false;
                        G2scripts.E = false;
                    }

                    else if (x1 <= -0.43 && x1 > -0.56)
                    {
                        print("B");
                        G2scripts.ChoosenBall = 'B';
                        //G2scriptsscript.ChoosenBall = 'C';
                        // G2scripts.positionX = x1;
                        G2scripts.A = false;
                        G2scripts.B = true;
                        G2scripts.C = false;
                        G2scripts.D = false;
                        G2scripts.E = false;
                    }

                    else if (x1 <= 0.17 && x1 > -0.1)
                    {
                        print("C");
                        G2scripts.ChoosenBall = 'C';
                        //G2scriptsscript.ChoosenBall = 'C';
                        // G2scripts.positionX = x1;
                        G2scripts.A = false;
                        G2scripts.B = false;
                        G2scripts.C = true;
                        G2scripts.D = false;
                        G2scripts.E = false;
                    }


                    else if (x1 > 0.53 && x1 <= 0.75)
                    {
                        print("D");
                        G2scripts.ChoosenBall = 'D';
                        // G2scripts.positionX = x1;
                        G2scripts.A = false;
                        G2scripts.B = false;
                        G2scripts.C = false;
                        G2scripts.D = true;
                        G2scripts.E = false;
                    }

                    else if (x1 <= 1.25 && x1 > 1.10)
                    {
                        print("E");
                        G2scripts.ChoosenBall = 'E';
                        //G2scripts.positionX = x1;
                        G2scripts.A = false;
                        G2scripts.B = false;
                        G2scripts.C = false;
                        G2scripts.D = false;
                        G2scripts.E = true;
                    }
                    else
                    {
                        G2scripts.A = false;
                        G2scripts.B = false;
                        G2scripts.C = false;
                        G2scripts.D = false;
                        G2scripts.E = false;
                        print("NON");
                    }
                }
            }
                else
                {
                    G2scripts.A = false;
                    G2scripts.B = false;
                    G2scripts.C = false;
                    G2scripts.D = false;
                    G2scripts.E = false;
                    print("NON");
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

    /*
    private EventHandler<GestureEventArgs> CreateOnGestureHandler(int bodyIndex)
    {
        return (object sender, GestureEventArgs e) => OnGestureDetected(sender, e, bodyIndex);
    }
    */
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
