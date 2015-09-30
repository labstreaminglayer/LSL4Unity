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
        LookUp();

        this.titleContent = new GUIContent("LSL Utility");
    }

    liblsl.StreamInfo[] streamInfos = null;

    void OnGUI() 
    {
         EditorGUILayout.BeginVertical();
         EditorGUILayout.BeginHorizontal();

         EditorGUILayout.LabelField(streamLookUpResult, EditorStyles.boldLabel);

         if (GUILayout.Button("LSL Stream Lookup", GUILayout.Height(40f)))
         {
             LookUp();
         }

         EditorGUILayout.HelpBox("The lookup process for LSL Streams might take a few seconds!", MessageType.Info);

         EditorGUILayout.EndHorizontal();

         EditorGUILayout.Space();
         EditorGUILayout.Separator();

         scrollVector = EditorGUILayout.BeginScrollView(scrollVector, GUILayout.Width(EditorGUIUtility.currentViewWidth), GUILayout.Height(150));
         GUILayoutOption fieldWidth = GUILayout.Width( EditorGUIUtility.currentViewWidth / 4.3f);
        // GUILayoutOption[] parameter = new GUILayoutOption[]{ };
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
             EditorGUILayout.LabelField(new GUIContent(s[0],s[0]), fieldWidth);
             EditorGUILayout.LabelField(new GUIContent(s[1],s[1]), fieldWidth);
             EditorGUILayout.LabelField(new GUIContent(s[2],s[2]), fieldWidth);
             EditorGUILayout.LabelField(new GUIContent(s[3],s[3]), fieldWidth);
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

    private void LookUp()
    {
        listNamesOfStreams.Clear();

        streamInfos = liblsl.resolve_streams(WaitOnResolveStreams);

        if (streamInfos.Length == 0)
        { 
            Debug.LogWarning(noStreamsFound); 
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

    }
}