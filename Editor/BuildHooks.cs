using UnityEngine;
using UnityEditor.Callbacks;
using UnityEditor;
using Assets.LSL4Unity.EditorExtensions;
using System.IO;

namespace Assets.LSL4Unity.Editor { 

    public class BuildHooks {

        const string LIB_LSL_NAME = "liblsl.dll";
        const string PLUGIN_DIR = "Plugins";

        [PostProcessBuildAttribute(1)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            var buildName = Path.GetFileNameWithoutExtension(pathToBuiltProject);

            var buildHostDirectory = pathToBuiltProject.Replace(Path.GetFileName(pathToBuiltProject), "");

            var dataDirectoryName = buildName + "_Data";

            var pathToDataDirectory = Path.Combine(buildHostDirectory, dataDirectoryName);

            var pluginDirectory = Path.Combine(pathToDataDirectory, PLUGIN_DIR);

            if (target == BuildTarget.StandaloneWindows)
            {
                RenameLibFile(pluginDirectory, LSLEditorIntegration.lib32Name, LSLEditorIntegration.lib64Name);
            }
            else if(target == BuildTarget.StandaloneWindows64)
            {
                RenameLibFile(pluginDirectory, LSLEditorIntegration.lib64Name, LSLEditorIntegration.lib32Name);
            }
        }

        private static void RenameLibFile(string pluginDirectory , string sourceName, string nameOfObsoleteFile)
        {
            var obsoleteFile = Path.Combine(pluginDirectory, nameOfObsoleteFile);

            Debug.Log("[BUILD] Delete obsolete file: " + obsoleteFile);

            File.Delete(obsoleteFile);

            var sourceFile = Path.Combine(pluginDirectory, sourceName);

            var targetFile = Path.Combine(pluginDirectory, LIB_LSL_NAME);
            
            Debug.Log(string.Format("[BUILD] Renaming: {0} to {1}", sourceFile, targetFile));

            File.Move(sourceFile, targetFile);
        }
    }
}