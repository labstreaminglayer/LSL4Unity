using Assets.LSL4Unity.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;

namespace Assets.LSL4Unity.Editor
{
    [CustomEditor(typeof(AOutlet<>))]
    public class CustomOutletInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        { 

            base.OnInspectorGUI();


        }
    }
}
