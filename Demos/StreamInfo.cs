using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets.LSL4Unity.Scripts;

namespace Assets.LSL4Unity.Demo
{ 
    public class StreamInfo : MonoBehaviour
    {
        public LSLTransformDemoOutlet outlet;

        public Text StreamNameLabel;

        public Text StreamTypeLabel;

        public Text DataRate;

        public Text HasConsumerLabel;

        // Use this for initialization
        void Start()
        {
            StreamNameLabel.text = outlet.StreamName;
            StreamTypeLabel.text = outlet.StreamType;
            DataRate.text = string.Format("Data Rate: {0}", outlet.GetDataRate());
            HasConsumerLabel.text = "Has no consumers";
        }

        // Update is called once per frame
        void Update()
        {
            if (outlet.HasConsumer()) { 
                HasConsumerLabel.text = "Has consumers";
                HasConsumerLabel.color = Color.green;
            }
            else { 

                HasConsumerLabel.text = "No Consumers";
                HasConsumerLabel.color = Color.black;
            }
        }
    }

}