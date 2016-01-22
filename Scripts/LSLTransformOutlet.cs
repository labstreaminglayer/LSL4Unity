using UnityEngine;
using LSL;

/// <summary>
/// An reusable example of an outlet which provides the orientation of an entity to LSL
/// </summary>
public class LSLTransformOutlet : MonoBehaviour {

    private const string unique_source_id = "63CE5B03731944F6AC30DBB04B451A94";

    private liblsl.StreamOutlet outlet;
    private liblsl.StreamInfo streamInfo;
    
    /// <summary>
    /// Use a array to reduce allocation costs
    /// </summary>
    private float[] currentSample;

    public string StreamName = "BeMoBI.Unity.Orientation.<Add_a_entity_id_here>";
    public string StreamType = "Unity.Quarternion";
    public int ChannelCount = 4;

    public Transform sampleSource;

    void Start()
    {
        // initialize the array once
        currentSample = new float[ChannelCount];

        streamInfo = new liblsl.StreamInfo(StreamName, StreamType, ChannelCount, Time.fixedDeltaTime, liblsl.channel_format_t.cf_float32, unique_source_id);

        outlet = new liblsl.StreamOutlet(streamInfo);
    }

    void LateUpdate()
    {
        if (outlet == null)
            return;

        var rotation = sampleSource.rotation;

        // reuse the array for each sample to reduce allocation costs
        currentSample[0] = rotation.x;
        currentSample[1] = rotation.y;
        currentSample[2] = rotation.z;
        currentSample[3] = rotation.w;

        outlet.push_sample(currentSample, LSLTimeSync.Instance.UpdateTimeStamp);
    }
}
