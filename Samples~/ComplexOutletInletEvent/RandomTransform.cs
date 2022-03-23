using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSL4Unity.Samples.Complex
{
    public class RandomTransform : MonoBehaviour
    {
        public float ResetInterval = 2.0f;
        private float elapsed_time = 0.0f;
        public Rigidbody rb;
        private Vector3 start_position;

        // Start is called before the first frame update
        void Start()
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            // rb.isKinematic = true;
            Vector3 p = gameObject.transform.position;
            start_position = new Vector3(p.x, p.y, p.z);
        }

        // Update is called once per frame
        void Update()
        {
            elapsed_time += Time.deltaTime;
            if (elapsed_time >= ResetInterval)
            {
                gameObject.transform.position = start_position;
                rb.velocity = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f));
                rb.angularVelocity = new Vector3(Random.Range(-6.0f, 6.0f), Random.Range(-6.0f, 6.0f), Random.Range(-6.0f, 6.0f));
                elapsed_time = 0.0f;
            }
        }
    }
}