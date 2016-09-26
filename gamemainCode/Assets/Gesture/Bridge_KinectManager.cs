using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Windows.Kinect;
using System.Text;
using System.Runtime.Serialization;
using System.Linq;



public class Bridge_KinectManager : MonoBehaviour
{
    
    public GameObject Player;
    private G3scripts g3Script;
    

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
    float maxright;
    float minright;
    public int right_choosen;
    public int left_choosen;

    private DepthFrameReader depthFrameReader;

    private FrameDescription depthFrameDescription;

    //private List<Tuple<JointType, JointType>> bones;

    //private WriteableBitmap depthBitmap;

    //public List<person> person_new = new List<person>();

    // GUI output
    private UnityEngine.Color[] bodyColors;
    //private string[] bodyText;

    /// <summary> List of gesture detectors, there will be one detector created for each potential body (max of 6) </summary>
    private List<GestureDetector> gestureDetectorList = null;

    // Use this for initialization

    public class person
    {
        public int bodyindex { get; set; }
        public double positionX { get; set; }
        public double positionZ { get; set; }
        public double shoulder_left { get; set; }
        public double elbow_left { get; set; }
        public double wrist_left { get; set; }
        public double shoulder_right { get; set; }
        public double elbow_right { get; set; }
        public double wrist_right { get; set; }

        public person(int a, double b, double c, double d, double e, double f, double g, double h, double i)
        {
            bodyindex = a;
            positionX = b;
            positionZ = c;
            shoulder_left = d;
            elbow_left = e;
            wrist_left = f;
            shoulder_right = g;
            elbow_right = h;
            wrist_right = i;
        }
    }



    void Start()
    {

        g3Script = Player.GetComponent<G3scripts>();

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
            List<person> person_new = new List<person>();

            // update gesture detectors with the correct tracking id
            for (int bodyIndex = 0; bodyIndex < this.bodyCount; bodyIndex++)
            {
                var body = this.bodies[bodyIndex];
                if (body != null)
                {
                    var trackingId = body.TrackingId;
                    //print("trackingID is " + trackingId);
                    //print("bodyindex is " + bodyIndex);
                    //print("trackingidold! " + head_Zindex[bodyIndex, 1] + " BODYINDEX "  + bodyIndex);
                    //print("true or false : " + this.gestureDetectorList[bodyIndex].IsPaused);

                    if (trackingId != this.gestureDetectorList[bodyIndex].TrackingId)
                    {
                        //print("different!!! so please change");
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
                        double position_shoulderleft = body.Joints[JointType.ShoulderLeft].Position.Y;
                        double position_shoulderright = body.Joints[JointType.ShoulderRight].Position.Y;
                        double position_wristleft = body.Joints[JointType.WristLeft].Position.Y;
                        double position_wristright = body.Joints[JointType.WristRight].Position.Y;
                        double position_elbowleft = body.Joints[JointType.ElbowLeft].Position.Y;
                        double position_elbowright = body.Joints[JointType.ElbowRight].Position.Y;

                        head_Zindex[bodyIndex, 0] = bodyIndex;
                        head_Zindex[bodyIndex, 1] = position_head.Z;
                        bodyindex_id[bodyIndex, 0] = bodyIndex;
                        bodyindex_id[bodyIndex, 1] = (float)trackingId;



                        print("body index: " + bodyIndex + " X: " + position_head.X + " Z: " + position_head.Z);
                        person_new.Add(new person(bodyIndex, position_head.X, position_head.Z, position_shoulderleft, position_elbowleft, position_wristleft, position_shoulderright, position_elbowright, position_wristright));
                    }
                }
            }

            List<person> SortedList = person_new.OrderBy(o => o.positionZ).ToList();
            for (var i = 0; i < SortedList.Count; i++) {
                print("sortedlist.z:  " + SortedList[i].positionZ);
                print("aoe.X : " + SortedList[i].positionX);
                print("sortedlist.bodyIndex:  " + SortedList[i].bodyindex);
                print("sortedlist.left1:  " + SortedList[i].shoulder_left);
                print("sortedlist.left2:  " + SortedList[i].elbow_left);
                print("sortedlist.left3:  " + SortedList[i].wrist_left);
                print("sortedlist.right1:  " + SortedList[i].shoulder_right);
                print("sortedlist.right2:  " + SortedList[i].elbow_right);
                print("sortedlist.right3:  " + SortedList[i].wrist_right);
            }
            if (SortedList.Count >= 2)
            {
                if (Math.Abs(SortedList[0].positionZ - SortedList[SortedList.Count - 1].positionZ) < 0.15)
                {
                    SortedList = SortedList.OrderBy(o => o.positionX).ToList();

                    if (SortedList[0].shoulder_left + 0.1 <= SortedList[0].elbow_left && SortedList[0].elbow_left + 0.1 < SortedList[0].wrist_left)
                    {
                        g3Script.Bridge_left = true;
                        print("lefttrue " + SortedList[0].bodyindex);
                    }
                    else { g3Script.Bridge_left = false; }

                    if (SortedList[SortedList.Count-1].shoulder_right +0.1 <= SortedList[SortedList.Count - 1].elbow_right && SortedList[SortedList.Count - 1].elbow_right  +0.1 < SortedList[SortedList.Count - 1].wrist_right)
                    {
                        g3Script.Bridge_right = true;
                        print("rightrue " +SortedList[SortedList.Count-1].bodyindex);
                    }
                    else { g3Script.Bridge_right = false; }

                }
            }
        }
    }



    /*

    private EventHandler<GestureEventArgs> CreateOnGestureHandler(int bodyIndex)
    {
        return (object sender, GestureEventArgs e) => OnGestureDetected(sender, e, bodyIndex);
    }
    */
    /*
    #region 測試姿勢有沒有符合
    void OnGestureDetected(object sender, GestureEventArgs e, int bodyIndex)
    {
        
        print("comehere");
        var isDetected = e.IsBodyTrackingIdValid && e.IsGestureDetected;

        print("gesturedetected is" + bodyIndex);

        if (e.GestureID == LiftHandRightName)
        {
            //NEW UI FOR GESTURE DETECTed
            print("right");
            print(e.DetectionConfidence + "gesturedetected is" + bodyIndex);
            if (e.DetectionConfidence > 0.40f)
            {
                g3Script.Bridge_right = true;
            }
            else
            {
                g3Script.Bridge_right = false;
            }
        }

        if (e.GestureID == LiftHandLeftName)
        {
            //NEW UI FOR GESTURE DETECTed
            print("left");
            print(e.DetectionConfidence + "gesturedetected is" + bodyIndex);
            //print("gesturedetected is" + bodyIndex);
            if (e.DetectionConfidence > 0.40f)
            {
                g3Script.Bridge_left = true;
            }
            else
            {
                g3Script.Bridge_left = false;
            }
        }
        
    }
    #endregion 
    */

    private void print(string v, float detectionConfidence)
    {
        throw new NotImplementedException();
    }

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
