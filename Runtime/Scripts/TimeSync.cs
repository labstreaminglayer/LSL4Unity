using UnityEngine;


namespace LSL4Unity.Utils
{
    /// <summary>
    /// This singleton should provide an dedicated timestamp for each update call or fixed update LSL sample!
    /// So that each sample provided by an Unity3D app has the same timestamp 
    /// Important! Make sure that the script is called before the default execution order!
    /// </summary>
    [ScriptOrder( -1000 )]
    public class TimeSync : MonoBehaviour
    {
        private static TimeSync instance;
        public static TimeSync Instance
        {
            get
            {
                return instance;
            }
        }

        private double fixedUpdateTimeStamp;
        public double FixedUpdateTimeStamp
        {
            get
            {
                return fixedUpdateTimeStamp;
            }
        }

        private double updateTimeStamp;
        public double UpdateTimeStamp
        {
            get
            {
                return updateTimeStamp;
            }
        }

        private double lateUpdateTimeStamp;
        public double LateUpdateTimeStamp
        {
            get { return lateUpdateTimeStamp; }
        }

        void Awake()
        {
            TimeSync.instance = this;
        }

        void FixedUpdate()
        {
            fixedUpdateTimeStamp = LSL.LSL.local_clock();
        }

        void Update()
        {
            updateTimeStamp = LSL.LSL.local_clock();
        }

        void LateUpdate()
        {
            lateUpdateTimeStamp = LSL.LSL.local_clock();
        }
    }
}