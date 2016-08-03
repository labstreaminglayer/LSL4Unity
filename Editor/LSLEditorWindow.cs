using UnityEngine;
using UnityEditor;
using LSL;
using System.Collections.Generic;

namespace Assets.LSL4Unity.EditorExtensions
{
    public class LSLShowStreamsWindow : EditorWindow
    {
        public double WaitOnResolveStreams = 2;

        private const string noStreamsFound = "No streams found!";
        private const string clickLookUpFirst = "Click lookup first";
        private const string nStreamsFound = " Streams found";

        private List<string> listNamesOfStreams = new List<string>();

        private Vector2 scrollVector;
        private string streamLookUpResult;

        private liblsl.ContinuousResolver resolver;
        private string lslVersionInfos;

        public void Init()
        {
            resolver = new liblsl.ContinuousResolver();

            var libVersion = liblsl.library_version();
            var protocolVersion = liblsl.protocol_version();

            var lib_major = libVersion / 100;
            var lib_minor = libVersion % 100;
            var prot_major = protocolVersion / 100;
            var prot_minor = protocolVersion % 100;

            lslVersionInfos = string.Format("You are using LSL library: {0}.{1} implementing protocol version: {2}.{3}", lib_major, lib_minor, prot_major, prot_minor);
            
            this.titleContent = new GUIContent("LSL Utility");
        }

        liblsl.StreamInfo[] streamInfos = null;

        void OnGUI()
        {
            if (resolver == null)
                Init();

            UpdateStreams();

            EditorGUILayout.BeginVertical();

            EditorGUILayout.Space();

            EditorGUILayout.LabelField(lslVersionInfos, EditorStyles.miniLabel);

            EditorGUILayout.BeginHorizontal();
            
            EditorGUILayout.LabelField(streamLookUpResult, EditorStyles.boldLabel);
            
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.Separator();

            scrollVector = EditorGUILayout.BeginScrollView(scrollVector, GUILayout.Width(EditorGUIUtility.currentViewWidth));
            GUILayoutOption fieldWidth = GUILayout.Width(EditorGUIUtility.currentViewWidth / 4.3f);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Name", EditorStyles.boldLabel, fieldWidth);
            EditorGUILayout.LabelField("Type", EditorStyles.boldLabel, fieldWidth);
            EditorGUILayout.LabelField("HostName", EditorStyles.boldLabel, fieldWidth);
            EditorGUILayout.LabelField("Data Rate", EditorStyles.boldLabel, fieldWidth);
            EditorGUILayout.EndHorizontal();

            foreach (var item in listNamesOfStreams)
            {
                string[] s = item.Split(' ');

                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(new GUIContent(s[0], s[0]), fieldWidth);
                EditorGUILayout.LabelField(new GUIContent(s[1], s[1]), fieldWidth);
                EditorGUILayout.LabelField(new GUIContent(s[2], s[2]), fieldWidth);
                EditorGUILayout.LabelField(new GUIContent(s[3], s[3]), fieldWidth);


                EditorGUILayout.EndHorizontal();

            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();

        }

        private void UpdateStreams()
        {
            listNamesOfStreams.Clear();

            streamInfos = resolver.results();

            if (streamInfos.Length == 0)
            {
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

}