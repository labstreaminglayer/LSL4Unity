using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSL4Unity.Samples.SimplePhysicsEvent
{ 
    public class OscillatingSphere : MonoBehaviour
    {
        void Update()
        {
            gameObject.transform.position = new Vector3(Mathf.Sin(Time.time), 0, 0);
        }
    }
}