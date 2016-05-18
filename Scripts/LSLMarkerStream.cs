using UnityEngine;
using System.Collections;

namespace Assets.LSL4Unity.Scripts
{
    public class LSLMarkerStream : MonoBehaviour
    {
        private const string unique_source_id = "D3F83BB699EB49AB94A9FA44B88882AB";

        public string lslStreamName = "Unity_<Paradigma_Name_here>";
        public string lslStreamType = "LSL_Marker_Strings";

        private LSL.liblsl.StreamInfo lslStreamInfo;
        private LSL.liblsl.StreamOutlet lslOutlet;
        private int lslChannelCount = 1;
        private double nominalRate = 0;
        private const LSL.liblsl.channel_format_t lslChannelFormat = LSL.liblsl.channel_format_t.cf_string;

        private string[] sample;

        // Use this for initialization
        void Start()
        {
            sample = new string[lslChannelCount];

            lslStreamInfo = new LSL.liblsl.StreamInfo(
                    lslStreamName,
                    lslStreamType,
                    lslChannelCount,
                    nominalRate,
                    lslChannelFormat,
                    unique_source_id);

            lslOutlet = new LSL.liblsl.StreamOutlet(lslStreamInfo);
        }

        public void Write(string marker)
        {
            sample[0] = marker;
            lslOutlet.push_sample(sample);
        }

        public void Write(string marker, float customTimeStamp)
        {
            sample[0] = marker;
            lslOutlet.push_sample(sample, customTimeStamp);
        }

        private string pendingMarker;

        public void WriteBeforeFrameIsDisplayed(string marker)
        {
            pendingMarker = marker;
            StartCoroutine(WriteMarkerAfterImageIsRendered());
        }

        IEnumerator WriteMarkerAfterImageIsRendered()
        {
            yield return new WaitForEndOfFrame();

            Write(pendingMarker);

            yield return null;
        }

    }
}