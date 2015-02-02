using UnityEngine;
using UnityEditor;
using LSL;
using System.Collections.Generic;

public class LSLEditor : EditorWindow
{
    public double WaitOnResolveStreams = 2;

    private const string noStreamsFound = "No streams found!";
    private const string clickLookUpFirst = "Click lookup first";
    private const string nStreamsFound = " Streams found";

    private List<string> listNamesOfStreams = new List<string>();
 
    private Vector2 scrollVector;
    private string streamLookUpResult;

    public void Init() 
    {
        
    
    }

    void OnGUI() 
    { 
         liblsl.StreamInfo[] streamInfos = null;

         EditorGUILayout.BeginVertical();

         streamLookUpResult = clickLookUpFirst;

         if (GUILayout.Button("LSL Stream Lookup"))
         {
             listNamesOfStreams.Clear();

             streamInfos = liblsl.resolve_streams(WaitOnResolveStreams);

             if (streamInfos.Length == 0)
             {
#if UNITY_EDITOR
                 Debug.LogWarning(noStreamsFound);
#endif
                 streamLookUpResult = noStreamsFound;
             }
             else
             {
                 foreach (var item in streamInfos)
                 {
                     listNamesOfStreams.Add(string.Format("{0} {1} {2} {3}", item.name(), item.type(), item.hostname(), item.nominal_srate()));
                 }

                 streamLookUpResult = listNamesOfStreams.Count + nStreamsFound;
             }

             EditorGUILayout.LabelField(streamLookUpResult, EditorStyles.boldLabel);
         }

         scrollVector = EditorGUILayout.BeginScrollView(scrollVector, GUILayout.Width(EditorGUIUtility.currentViewWidth), GUILayout.Height(150));
         GUILayoutOption fieldWidth = GUILayout.Width( EditorGUIUtility.currentViewWidth / 4);
         GUILayoutOption[] parameter = new GUILayoutOption[]{ };
         EditorGUILayout.BeginHorizontal();
         EditorGUILayout.LabelField("Name", EditorStyles.boldLabel, fieldWidth);
         EditorGUILayout.LabelField("Type", EditorStyles.boldLabel, fieldWidth);
         EditorGUILayout.LabelField("HostName", EditorStyles.boldLabel, fieldWidth);
         EditorGUILayout.LabelField("Data Rate", EditorStyles.boldLabel, fieldWidth);
         EditorGUILayout.EndHorizontal();
         foreach (var item in listNamesOfStreams)
         {
             string[] s = item.Split(' ');

             EditorGUILayout.BeginHorizontal();
             EditorGUILayout.LabelField(s[0], fieldWidth);
             EditorGUILayout.LabelField(s[1], fieldWidth);
             EditorGUILayout.LabelField(s[2], fieldWidth);
             EditorGUILayout.LabelField(s[3], fieldWidth);
             EditorGUILayout.EndHorizontal();
             if (GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
             {
                if (Event.current.type == EventType.MouseUp) 
                { 
                    Debug.Log("Mouse click on :" + item);
                }
             }
         }
         EditorGUILayout.EndScrollView();
         EditorGUILayout.EndVertical();

    }
}