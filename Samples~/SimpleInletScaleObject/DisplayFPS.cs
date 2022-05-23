using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LSL4Unity.Samples.SimpleInlet
{
    // You probably don't need this namespace. We do it to avoid contaminating the global namespace of your project.
    public class DisplayFPS : MonoBehaviour
    {
        private Text m_Text;

        // Start is called before the first frame update
        void Start()
        {
            m_Text = GetComponent<Text>();
            InvokeRepeating(nameof(GetFPS), 1, 1);
        }

        void GetFPS()
        {
            float fps = (int)(1f / Time.unscaledDeltaTime);
            m_Text.text = "FPS: " + fps;
        }
    }
}
