using UnityEngine;
using System.Collections;
using LSL;

namespace LSL4Unity.Scripts
{
    [HelpURL("https://github.com/labstreaminglayer/LSL4Unity/wiki#using-a-marker-stream")]
    public class LSLMarkerStream : MonoBehaviour
    {
        private const string unique_source_id = "D3F83BB699EB49AB94A9FA44B88882AB";

        public string lslStreamName = "Unity_<Paradigma_Name_here>";
        public string lslStreamType = "LSL_Marker_Strings";

        private StreamInfo lslStreamInfo;
        private StreamOutlet lslOutlet;
        private int lslChannelCount = 1;

        //Assuming that markers are never send in regular intervalls
        private double nominal_srate = LSL.LSL.IRREGULAR_RATE;

        private const channel_format_t lslChannelFormat = channel_format_t.cf_string;

        private string[] sample;
 
        void Awake()
        {
            sample = new string[lslChannelCount];

            lslStreamInfo = new StreamInfo(
                                        lslStreamName,
                                        lslStreamType,
                                        lslChannelCount,
                                        nominal_srate,
                                        lslChannelFormat,
                                        unique_source_id);
            
            lslOutlet = new StreamOutlet(lslStreamInfo);
        }

        public void Write(string marker)
        {
            sample[0] = marker;
            lslOutlet.push_sample(sample);
        }

        public void Write(string marker, double customTimeStamp)
        {
            sample[0] = marker;
            lslOutlet.push_sample(sample, customTimeStamp);
        }

        public void Write(string marker, float customTimeStamp)
        {
            sample[0] = marker;
            lslOutlet.push_sample(sample, customTimeStamp);
        }

        public void WriteBeforeFrameIsDisplayed(string marker)
        {
            StartCoroutine(WriteMarkerAfterImageIsRendered(marker));
        }

        IEnumerator WriteMarkerAfterImageIsRendered(string pendingMarker)
        {
            yield return new WaitForEndOfFrame();

            Write(pendingMarker);

            yield return null;
        }

    }
}