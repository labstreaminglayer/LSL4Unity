using UnityEngine;
using UnityEditor;
using System.Collections;
using LSL4Unity.Utils;


namespace LSL4Unity.EditorExtensions
{
    [CustomEditor(typeof(TimeSync))]
    public class TimeSyncEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }

}

