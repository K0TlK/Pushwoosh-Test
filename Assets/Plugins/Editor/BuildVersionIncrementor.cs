using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;

namespace Assets.Plugins.Editor
{
    public class BuildVersionIncrementor
    {
        [PostProcessBuild]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            if (target == BuildTarget.iOS)
            {
                CreateBuildFile(pathToBuiltProject);
                IncrementBuildNumber();
            }
        }

        private static void IncrementBuildNumber()
        {
            int buildNumber = int.Parse(PlayerSettings.iOS.buildNumber);
            buildNumber++;
            PlayerSettings.iOS.buildNumber = buildNumber.ToString();

            Debug.Log("<b>Build number incremented</b> to: " + buildNumber);
        }

        private static void CreateBuildFile(string pathToBuiltProject)
        {
            string buildFilePath = Path.Combine(pathToBuiltProject, "Build" + PlayerSettings.iOS.buildNumber);
            File.Create(buildFilePath).Dispose();

            Debug.Log("Build file created: " + buildFilePath);
        }
    }
}