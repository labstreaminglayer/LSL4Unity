using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class LSLEditorIntegration : ScriptableObject
{
    static readonly string wikiURL = "https://github.com/xfleckx/LSL4Unity/wiki";
    static readonly string lib64Name = "liblsl64.dll";
    static readonly string lib32Name = "liblsl32.dll";
    static readonly string wrapperFileName = "LSL.cs";
    static readonly string assetSubFolder = "LSL4Unity";

    [MenuItem("LSL/Open LSL Window")]
    static void OpenLSLWindow()
    {
        LSLEditor window = (LSLEditor)EditorWindow.GetWindow(typeof(LSLEditor));
        window.Init();
        
    }

    [MenuItem("LSL/Open LSL Window", true)]
    static bool ValidateOpenLSLWindow()
    {
        string root = Application.dataPath;

        bool lib64Available = false; 
        bool lib32Available = false;
        bool wrapperFileAvailable = false;
        
        lib32Available = File.Exists(Path.Combine(root, Path.Combine(assetSubFolder, lib32Name)));
        lib64Available = File.Exists(Path.Combine(root, Path.Combine(assetSubFolder, lib64Name)));
        wrapperFileAvailable = File.Exists(Path.Combine(root, Path.Combine(assetSubFolder, wrapperFileName)));

        if (lib64Available && lib32Available && wrapperFileAvailable)
            return true;
        
        Debug.LogError("LabStreamingLayer libraries not available! See " + wikiURL + " for installation instructions");
        return false;
    }

}
