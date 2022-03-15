using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using UnityEngine.Assertions;

namespace LSL4Unity.EditorExtensions
{
    public class LSLEditorIntegration
    {
        public static readonly string wikiURL = "https://github.com/xfleckx/LSL4Unity/wiki";
        public static readonly string lib64Name = "liblsl64";
        public static readonly string lib32Name = "liblsl32";
        
        public const string DLL_ENDING = ".dll";
        public const string SO_ENDING = ".so";
        public const string BUNDLE_ENDING = ".bundle";

        [MenuItem("LSL/Show Streams")]
        static void OpenLSLWindow()
        {
            var window = EditorWindow.GetWindow<LSLShowStreamsWindow>(true);

            window.Init();

            window.ShowUtility();
        }
    }
}