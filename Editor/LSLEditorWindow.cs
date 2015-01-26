using UnityEngine;
using UnityEditor;
using LSL;
using System.Collections.Generic;

public class LSLEditor : EditorWindow
{
    public double WaitOnResolveStreams = 2;
    
    public void Init() 
    {
        
    
    }

    void OnGui() 
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel); 

        liblsl.StreamInfo[] streamInfos = null;
        GUILayout.BeginVertical();

        if (GUILayout.Button("LSL Stream Lookup"))
        {
            streamInfos = liblsl.resolve_streams(WaitOnResolveStreams);

            List<liblsl.StreamInfo> listOfStreamInfos = new List<liblsl.StreamInfo>(streamInfos);

            if (listOfStreamInfos.Count == 0){
                Debug.LogWarning("no streams found");
            }
            else { 
                foreach (var item in listOfStreamInfos)
                {

                }
            }
        }

        GUILayout.EndVertical();

    }
}