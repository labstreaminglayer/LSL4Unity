using UnityEngine;
using UnityEditor.Callbacks;
using UnityEditor;
using System.IO;

namespace Assets.LSL4Unity.EditorExtensions
{ 

    public class BuildHooks {

        const string LIB_LSL_NAME = "liblsl";
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
                RenameLibFile(pluginDirectory, LSLEditorIntegration.lib32Name, LSLEditorIntegration.lib64Name, LSLEditorIntegration.DLL_ENDING);
            }
            else if(target == BuildTarget.StandaloneWindows64)
            {
                RenameLibFile(pluginDirectory, LSLEditorIntegration.lib64Name, LSLEditorIntegration.lib32Name, LSLEditorIntegration.DLL_ENDING);
            }

            if (target == BuildTarget.StandaloneLinux)
            {
                RenameLibFile(pluginDirectory, LSLEditorIntegration.lib32Name, LSLEditorIntegration.lib64Name, LSLEditorIntegration.SO_ENDING);
            }
            else if (target == BuildTarget.StandaloneLinux64)
            {
                RenameLibFile(pluginDirectory, LSLEditorIntegration.lib64Name, LSLEditorIntegration.lib32Name, LSLEditorIntegration.SO_ENDING);
            }

            if (target == BuildTarget.StandaloneOSXIntel)
            {
                RenameLibFile(pluginDirectory, LSLEditorIntegration.lib32Name, LSLEditorIntegration.lib64Name, LSLEditorIntegration.BUNDLE_ENDING);
            }
            else if (target == BuildTarget.StandaloneOSXIntel64)
            {
                RenameLibFile(pluginDirectory, LSLEditorIntegration.lib64Name, LSLEditorIntegration.lib32Name, LSLEditorIntegration.BUNDLE_ENDING);
            }
        }

        private static void RenameLibFile(string pluginDirectory , string sourceName, string nameOfObsoleteFile, string fileEnding)
        {
            var obsoleteFile = Path.Combine(pluginDirectory, nameOfObsoleteFile + fileEnding);

            Debug.Log("[LSL BUILD Hook] Delete obsolete file: " + obsoleteFile);

            File.Delete(obsoleteFile);

            var sourceFile = Path.Combine(pluginDirectory, sourceName + fileEnding);

            var targetFile = Path.Combine(pluginDirectory, LIB_LSL_NAME + fileEnding);
            
            Debug.Log(string.Format("[LSL BUILD Hook] Renaming: {0} to {1}", sourceFile, targetFile));

            File.Move(sourceFile, targetFile);
        }
    }
}