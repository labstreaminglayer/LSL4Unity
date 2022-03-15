using UnityEngine;
using UnityEditor;
using System.Collections;
using LSL4Unity.Scripts;


namespace LSL4Unity.EditorExtensions
{
    [CustomEditor(typeof(LSLTimeSync))]
    public class TimeSyncEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }

}

