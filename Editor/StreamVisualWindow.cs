using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using LSL;
using UnityEngine;

namespace Assets.LSL4Unity.Editor
{
    public class StreamVisualWindow : EditorWindow
    {
        private liblsl.StreamInlet inlet;
        private liblsl.StreamInfo info;
        private Graph graph;
        private List<int> selectedChannels;
        private liblsl.ContinuousResolver resolver;
        private float[] sample;

        public string StreamName;

        Material lineMaterial;

        private void Initialize(string streamName)
        {
            if(streamName != null) {
                StreamName = streamName;

                resolver = new liblsl.ContinuousResolver("name", StreamName);
            }
        }

        void OnEnable()
        {
            if(resolver == null)
                Initialize(StreamName);
        }

        
        private void GetLineMaterial()
        {
            lineMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/LSL4Unity/Editor/GraphVisualisations/GraphMaterial.mat");
        }

        void OnGUI()
        {
            if (inlet == null)
            {
                var results = resolver.results();

                if (results.Length == 0)
                {
                    this.Close();
                    return;
                }

                info = results[0];

                inlet = new liblsl.StreamInlet(info);

                var channelCount = info.channel_count();
                graph = new Graph(channelCount);
                sample = new float[channelCount];
            }


            EditorGUILayout.BeginHorizontal(GUILayout.MinWidth(500));

            RenderSelection(info.channel_count());

            EditorGUILayout.BeginVertical(GUILayout.MinWidth(500), GUILayout.MinHeight(300));
            var rect = new Rect(100, 0, 500, 300);
                RenderGraphsForChannels(rect);
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();


            UpdateValues();

            
            Repaint();
        }

        private void UpdateValues()
        {
            while (inlet.pull_sample(sample, 0.0f) != 0)
            {
                for (int i = 0; i < graph.channels.Count; i++)
                {
                    var val = sample[i];
                    graph.channels[i].Feed(val);
                }
            }

        }

        private void RenderGraphsForChannels(Rect renderingArea)
        {
            int W = (int)renderingArea.width;
            int H = (int)renderingArea.height;
            
            foreach (var channel in graph.channels)
            {
                if (!channel.isActive)
                    continue;
                
                for (int h = 0; h < this.graph.MAX_HISTORY; h++)
                {
                    int xPix = (W - 1) - h;

                    if (xPix >= 0)
                    {
                        float y = channel._data[h];

                        float y_01 = Mathf.InverseLerp(graph.YMin, graph.YMax, y);

                        int yPix = (int)(y_01 * H);
                        
                        HandlesPlot(xPix, yPix, channel._color);
                    }
                }
            }
            
        }
        
        void HandlesPlot(float x, float y, Color drawColor)
        {
            var temp = Handles.color;

            Handles.color = drawColor;

            Handles.DrawLine(new Vector3(x - 1, y - 1, 0), new Vector3(x + 1, y + 1, 0));

            Handles.color = temp;
        }

        private void RenderSelection(int channelIndex)
        {
            EditorGUILayout.BeginVertical();
            foreach (Channel c in graph.channels)
            {
                c.isActive = EditorGUILayout.Toggle(c.isActive);
            }
            EditorGUILayout.EndVertical();

        }

        internal static EditorWindow GetNewInstanceFor(string streamName)
        {
            var window = EditorWindow.CreateInstance<StreamVisualWindow>();

            window.Initialize(streamName);

            return window;
        }

    }
}
