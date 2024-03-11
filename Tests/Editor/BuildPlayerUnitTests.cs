using System;
using System.IO;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.TestTools;

namespace LSL4Unity.Tests.Editor
{
    public class BuildPlayerUnitTests
    {
        private const string BuildPath = "Builds/BuildUnitTest.exe";

        private static BuildResult GetBuildResult(BuildTarget buildTarget, BuildTargetGroup buildTargetGroup,
            string buildOutputPath = BuildPath, BuildOptions buildOptions = BuildOptions.CleanBuildCache)
        {
            // Check if package is in Assets or Packages dir
            bool existInAssetsDir = Directory.Exists(Path.Combine(Environment.CurrentDirectory, "Assets/LSL4Unity"));

            var scenes = existInAssetsDir
                ? new[] { "Assets/LSL4Unity/Tests/Editor/UnitTestScene.unity" }
                : new[] { "Packages/LSL4Unity/Tests/Editor/UnitTestScene.unity" };

            var buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = scenes,
                target = buildTarget,
                targetGroup = buildTargetGroup,
                locationPathName = buildOutputPath,
                options = buildOptions
            };

            return BuildPipeline.BuildPlayer(buildPlayerOptions).summary.result;
        }

        [RequirePlatformSupport(BuildTarget.StandaloneWindows, BuildTarget.StandaloneWindows64,
            BuildTarget.StandaloneLinux64, BuildTarget.StandaloneOSX)]
        [Test]
        [TestCase(BuildTarget.StandaloneWindows)]
        [TestCase(BuildTarget.StandaloneWindows64)]
        [TestCase(BuildTarget.StandaloneLinux64)]
        [TestCase(BuildTarget.StandaloneOSX)]
        public void BuildPlayerStandalone(BuildTarget buildTarget,
            BuildTargetGroup buildTargetGroup = BuildTargetGroup.Standalone)
        {
            var result = GetBuildResult(buildTarget, buildTargetGroup);
            Assert.AreEqual(BuildResult.Succeeded, result);
        }

        [RequirePlatformSupport(BuildTarget.Android, BuildTarget.iOS)]
        [Test]
        [TestCase(BuildTarget.Android, BuildTargetGroup.Android)]
        [TestCase(BuildTarget.iOS, BuildTargetGroup.iOS)]
        public void BuildPlayerMobile(BuildTarget buildTarget, BuildTargetGroup buildTargetGroup)
        {
            var result = GetBuildResult(buildTarget, buildTargetGroup);
            Assert.AreEqual(BuildResult.Succeeded, result);
        }
    }
}