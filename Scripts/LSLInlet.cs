using UnityEngine;
using System.Collections;
using LSL;
using System;

public class LSLInlet : MonoBehaviour {

    public enum UpdateMoment { FixedUpdate, Update }

    public UpdateMoment moment;

    public string StreamName;
    
    liblsl.StreamInfo[] results;
    liblsl.StreamInlet inlet;
    liblsl.ContinuousResolver resolver;

    int expectedChannels = 0;
    
    void Start () {
         
        Debug.Log("Creating LSL resolver for stream " + StreamName);

        resolver = new liblsl.ContinuousResolver("name", StreamName);

        StartCoroutine(ResolveExpectedStream());
    }
    
    IEnumerator ResolveExpectedStream()
    {
        var results = resolver.results();

        yield return new WaitUntil(() => results.Length > 0);

        inlet = new liblsl.StreamInlet(results[0]);
        
        yield return null;
    }

    private void pullSamples()
    {
        // read samples
        float[] sample = new float[expectedChannels];

        while (inlet.pull_sample(sample, 0.0f) != 0)
        {
            foreach (float f in sample)
                Debug.Log(string.Format("\t{0}", f));
        }
    }

    void FixedUpdate()
    {
        if (moment == UpdateMoment.FixedUpdate && inlet != null)
            pullSamples();
    }
    
    void Update()
    {
        if (moment == UpdateMoment.Update && inlet != null)
            pullSamples();
    }
}
