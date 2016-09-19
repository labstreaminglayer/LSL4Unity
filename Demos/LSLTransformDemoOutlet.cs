using UnityEngine;
using LSL;
using Assets.LSL4Unity.Scripts;
using Assets.LSL4Unity.Scripts.Common;

namespace Assets.LSL4Unity.Demo
{
    /// <summary>
    /// An reusable example of an outlet which provides the orientation of an entity to LSL
    /// </summary>
    public class LSLTransformDemoOutlet : MonoBehaviour
    {
        private const string unique_source_id = "D256CFBDBA3145978CFA641403219531";

        private liblsl.StreamOutlet outlet;
        private liblsl.StreamInfo streamInfo;
        public liblsl.StreamInfo GetStreamInfo()
        {
            return streamInfo;
        }
        /// <summary>
        /// Use a array to reduce allocation costs
        /// </summary>
        private float[] currentSample;

        private double dataRate;

        public double GetDataRate()
        {
            return dataRate;
        }

        public bool HasConsumer()
        {
            if(outlet != null)
                return outlet.have_consumers();

            return false;
        }

        public string StreamName = "BeMoBI.Unity.Orientation.<Add_a_entity_id_here>";
        public string StreamType = "Unity.Quaternion";
        public int ChannelCount = 4;

        public MomentForSampling sampling;

        public Transform sampleSource;

        void Start()
        {
            // initialize the array once
            currentSample = new float[ChannelCount];

            dataRate = LSLUtils.GetSamplingRateFor(sampling);

            streamInfo = new liblsl.StreamInfo(StreamName, StreamType, ChannelCount, dataRate, liblsl.channel_format_t.cf_float32, unique_source_id);

            outlet = new liblsl.StreamOutlet(streamInfo);
        }

        private void pushSample()
        {
            if (outlet == null)
                return;
            var rotation = sampleSource.rotation;

            // reuse the array for each sample to reduce allocation costs
            currentSample[0] = rotation.x;
            currentSample[1] = rotation.y;
            currentSample[2] = rotation.z;
            currentSample[3] = rotation.w;

            outlet.push_sample(currentSample, liblsl.local_clock());
        }

        void FixedUpdate()
        {
            if (sampling == MomentForSampling.FixedUpdate)
                pushSample();
        }

        void Update()
        {
            if (sampling == MomentForSampling.Update)
                pushSample();
        }

        void LateUpdate()
        {
            if (sampling == MomentForSampling.LateUpdate)
                pushSample();
        }
    }
}