using UnityEditor;
using Assets.LSL4Unity.Scripts;


namespace Assets.LSL4Unity.EditorExtensions
{
    [CustomEditor(typeof(LSLTimeSync))]
    public class TimeSyncEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }

}

