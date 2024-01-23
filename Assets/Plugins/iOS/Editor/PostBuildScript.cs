using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;
using System.IO;

namespace Assets.Plugins.iOS
{
    public class PostBuildScript
    {
        [PostProcessBuild]
        public static void ChangePlist(BuildTarget buildTarget, string pathToBuiltProject)
        {
            if (buildTarget == BuildTarget.iOS)
            {
                string plistPath = pathToBuiltProject + "/Info.plist";
                PlistDocument plist = new PlistDocument();
                plist.ReadFromString(File.ReadAllText(plistPath));

                PlistElementDict rootDict = plist.root;

                rootDict.SetString("PW_APP_GROUPS_NAME", "com.newbundle.webgeek");
                rootDict.SetString("NSUserTrackingUsageDescription", "${PRODUCT_NAME} for test");
                rootDict.SetString("Pushwoosh_APPID", "2964F-C16D0");
                rootDict.SetString("Pushwoosh_SHOW_ALERT", "YES");

                rootDict.SetBoolean("UIStatusBarHidden", false);

                File.WriteAllText(plistPath, plist.WriteToString());
                Debug.Log($"[PostBuildScript-ChangePlist] {plistPath} was edited");
            }
            else
            {
                Debug.Log($"[PostBuildScript-ChangePlist] Error BuildTarget not IOS: {buildTarget}");
            }
        }

        [PostProcessBuild]
        public static void ChangeBundleIdentifier(BuildTarget buildTarget, string pathToBuiltProject)
        {
            if (buildTarget == BuildTarget.iOS)
            {
                string projPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);
                PBXProject proj = new PBXProject();
                proj.ReadFromFile(projPath);

                string currentBundleId = PlayerSettings.applicationIdentifier;

                string mainTargetGuid = proj.GetUnityMainTestTargetGuid();
                string frameworkTargetGuid = proj.GetUnityFrameworkTargetGuid();

                string mainTestBundleId = proj.GetBuildPropertyForAnyConfig(mainTargetGuid, "PRODUCT_BUNDLE_IDENTIFIER");
                string frameworkBundleId = proj.GetBuildPropertyForAnyConfig(frameworkTargetGuid, "PRODUCT_BUNDLE_IDENTIFIER");

                string mainTestBundleIdNew = mainTestBundleId.Replace("com.unity3d", currentBundleId);
                string frameworkBundleIdNew = frameworkBundleId.Replace("com.unity3d", currentBundleId);

                proj.SetBuildProperty(mainTargetGuid, "PRODUCT_BUNDLE_IDENTIFIER", mainTestBundleIdNew);
                proj.SetBuildProperty(frameworkTargetGuid, "PRODUCT_BUNDLE_IDENTIFIER", frameworkBundleIdNew);

                Debug.Log("[PostBuildScript-ChangeBundleIdentifier] Set new PRODUCT_BUNDLE_IDENTIFIER\n" +
                    $"{mainTargetGuid} change {mainTestBundleId} => {mainTestBundleIdNew}\n" +
                    $"{frameworkBundleId} change {frameworkBundleId} => {frameworkBundleIdNew}\n" +
                    $"File path: {projPath}");

                proj.WriteToFile(projPath);
            }
            else
            {
                Debug.Log($"[PostBuildScript-ChangeBundleIdentifier] Error BuildTarget not IOS: {buildTarget}");
            }
        }
    }
}