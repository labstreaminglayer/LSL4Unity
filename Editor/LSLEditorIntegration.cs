using UnityEngine;
using UnityEditor;
using System.IO;

namespace Assets.LSL4Unity.EditorExtensions
{
    public class LSLEditorIntegration
    {
        public static readonly string wikiURL = "https://github.com/xfleckx/LSL4Unity/wiki";
        public static readonly string lib64Name = "liblsl64.dll";
        public static readonly string lib32Name = "liblsl32.dll";
        static readonly string wrapperFileName = "LSL.cs";
        static readonly string assetSubFolder = "LSL4Unity";
        static readonly string libFolder = assetSubFolder + @"/lib";

        [MenuItem("LSL/Show Streams")]
        static void OpenLSLWindow()
        {
            var window = EditorWindow.GetWindow<LSLShowStreamsWindow>(true);

            window.Init();

            window.ShowUtility();
        }

        [MenuItem("LSL/Show Streams", true)]
        static bool ValidateOpenLSLWindow()
        {
            string root = Application.dataPath;

            bool lib64Available = false;
            bool lib32Available = false;
            bool apiAvailable = false;

            lib32Available = File.Exists(Path.Combine(root, Path.Combine(libFolder, lib32Name)));
            lib64Available = File.Exists(Path.Combine(root, Path.Combine(libFolder, lib64Name)));
            apiAvailable = File.Exists(Path.Combine(root, Path.Combine(assetSubFolder, wrapperFileName)));

            if ((lib64Available || lib32Available) && apiAvailable)
                return true;

            Debug.LogError("LabStreamingLayer libraries not available! See " + wikiURL + " for installation instructions");
            return false;
        }

    }
}