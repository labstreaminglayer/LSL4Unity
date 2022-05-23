using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSL4Unity.Utils;

namespace LSL4Unity.Samples.Complex
{
    public class ChangePlaneColour : AStringOutlet
    {
        public float ResetInterval = 3.4f;
        private float elapsed_time = 0.0f;
        private Material mat;

        public override List<string> ChannelNames
        {
            get
            {
                List<string> chanNames = new List<string>{ "Event" };
                return chanNames;
            }
        }

        public void Reset()
        {
            StreamName = "Unity.MatColour";
            StreamType = "Markers";
            moment = MomentForSampling.EndOfFrame;
            IrregularRate = true;
        }

        protected override void Start()
        {
            base.Start();
            Renderer rend = GetComponent<Renderer>();
            mat = rend.material;
        }

        // Update is called once per frame
        protected override bool BuildSample()
        {
            // Called by Update because our moment is EndOfFrame.
            elapsed_time += Time.deltaTime;
            if (elapsed_time >= ResetInterval)
            {
                mat.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                sample[0] = mat.color.ToString();

                elapsed_time = 0.0f;
                return true;
            }
            return false;
        }
    }
}