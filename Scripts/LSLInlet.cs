using UnityEngine;
using System.Collections;
using LSL;

public class LSLInlet : MonoBehaviour {

    liblsl.StreamInfo[] results;
    liblsl.StreamInlet inlet;

    int expectedChannels = 0;

    void Start () {

        // wait until an EEG stream shows up
        results = liblsl.resolve_stream("type", "Unity.FixedUpdateTime");
        // open an inlet and print some interesting info about the stream (meta-data, etc.)
        inlet = new liblsl.StreamInlet(results[0]);

        expectedChannels = inlet.info().channel_count();

        Debug.Log(inlet.info().as_xml());
    }
     
    void FixedUpdate()
    {
        // read samples
        float[] sample = new float[expectedChannels];

        while (inlet.samples_available() > 0)
        {
            inlet.pull_sample(sample);

            foreach (float f in sample)
                Debug.Log(string.Format("\t{0}", f));

        }
    }

}
