using UnityEngine;
using System.Collections;
using LSL;

public class LSLOutlet : MonoBehaviour
{
    private liblsl.StreamOutlet outlet;
    private liblsl.StreamInfo streamInfo;
    private float[] currentSample;

    public string StreamName = "beMoBI.Unity.OVR";
    public string StreamType = "Unity.OVR.Orientation";
    public int ChannelCount = 4;

    // Use this for initialization
    void Start()
    {
        currentSample = new float[ChannelCount];

        streamInfo = new liblsl.StreamInfo(StreamName, StreamType, ChannelCount, Time.fixedDeltaTime);

        outlet = new liblsl.StreamOutlet(streamInfo);
    }

    public void FixedUpdate()
    {
        OVRPose pose = OVRManager.display.GetHeadPose();
        currentSample[0] = pose.orientation.x;
        currentSample[1] = pose.orientation.y;
        currentSample[2] = pose.orientation.z;
        currentSample[3] = pose.orientation.w; 
        outlet.push_sample(currentSample);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
