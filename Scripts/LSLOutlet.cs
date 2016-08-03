using UnityEngine;
using System.Collections;
using LSL;
using System.Diagnostics;

namespace Assets.LSL4Unity.Scripts
{
    public enum MomentForSampling { Update, FixedUpdate, LateUpdate }


    public class LSLOutlet : MonoBehaviour
    {
        private liblsl.StreamOutlet outlet;
        private liblsl.StreamInfo streamInfo;
        private float[] currentSample;

        public string StreamName = "Unity.ExampleStream";
        public string StreamType = "Unity.FixedUpdateTime";
        public int ChannelCount = 1;

        Stopwatch watch;

        // Use this for initialization
        void Start()
        {
            watch = new Stopwatch();

            watch.Start();

            currentSample = new float[ChannelCount];

            streamInfo = new liblsl.StreamInfo(StreamName, StreamType, ChannelCount, Time.fixedDeltaTime * 1000);

            outlet = new liblsl.StreamOutlet(streamInfo);
        }

        public void FixedUpdate()
        {
            if (watch == null)
                return;

            watch.Stop();

            currentSample[0] = watch.ElapsedMilliseconds;

            watch.Reset();
            watch.Start();

            outlet.push_sample(currentSample);
        }
    }
}