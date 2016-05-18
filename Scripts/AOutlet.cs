using System;
using UnityEngine; 
using LSL;

namespace Assets.LSL4Unity.Scripts
{
    public abstract class AOutlet<SampleType> : MonoBehaviour
    {
        protected liblsl.StreamOutlet outlet;
        protected liblsl.StreamInfo streamInfo;
        protected SampleType[] currentSample;

        public int ChannelCount = -1;

        public string StreamType = "Add a stream type name";

        public string StreamName = "Add a stream name!";

        void Awake()
        {
           Util.CheckIfTheRequestedTypeIsAvailable<SampleType>();
        }
    }
}
