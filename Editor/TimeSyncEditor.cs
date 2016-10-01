using UnityEngine;
using UnityEditor;
using System.Collections;
using Assets.LSL4Unity.Scripts;


namespace Assets.LSL4Unity.EditorExtensions
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

