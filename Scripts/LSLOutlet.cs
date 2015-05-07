using UnityEngine;
using System.Collections;
using LSL;

public class LSLOutlet : MonoBehaviour
{
    private liblsl.StreamOutlet outlet;
    private liblsl.StreamInfo streamInfo;
    private float[] currentSample;

    public string StreamName = "beMoBI.Unity.ExampleStream";
    public string StreamType = "Unity.Random01f";
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
        currentSample[0] = Random.value;
        currentSample[1] = Random.value;
        currentSample[2] = Random.value;
        currentSample[3] = Random.value; 
        outlet.push_sample(currentSample);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
