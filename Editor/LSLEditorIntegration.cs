using UnityEngine;
using System.Collections;
using UnityEditor;

public class LSLEditorIntegration : ScriptableObject
{
    static readonly string wikiURL = "https://github.com/xfleckx/LSL4Unity/wikihttps://github.com/xfleckx/LSL4Unity/wiki";
    static readonly string lib64Name = "liblsl64.dll";
    static readonly string lib32Name = "liblsl32.dll";
    static readonly string wrapperFileName = "LSL.cs";

    [MenuItem("LSL/Open LSL Window")]
    static void OpenLSLWindow()
    { 

    }

    [MenuItem("LSL/Open LSL Window", true)]
    static bool ValidateOpenLSLWindow()
    {
        string root = Application.dataPath;

        bool lib64Available = false; 
        bool lib32Available = false;
        bool wrapperFileAvailable = false;

        // Check if Libraries available

        if (lib64Available && lib32Available && wrapperFileAvailable)
        {
            return true;
        }

        Debug.LogError("LabStreamingLayer libraries not available! See " + wikiURL + " for installation instructions");

        return false;
    }

}
