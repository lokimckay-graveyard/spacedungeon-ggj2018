using UnityEditor;
using UnityEngine;
using System.IO;
using System;
using LitJson;
using System.Collections.Generic;

class BuildScript : MonoBehaviour
{
    //VAR
    //-------------------------------------------------------------------------------------------------

    //Paths
    private static DirectoryInfo path_unity_assets = new DirectoryInfo(Application.dataPath);
    private static string path_proj_root = path_unity_assets.Parent.Parent.Parent.Parent.ToString();

    //Config loading
    private static string jsonConfigFilePath = path_proj_root + "\\ctrl\\resources\\config.json";
    private static string jsonConfigFileContent = File.ReadAllText(jsonConfigFilePath);
    private static JsonData jsonConfig = JsonMapper.ToObject(jsonConfigFileContent);

    //Scenes
    private static string sceneBase = jsonConfig["scene"]["base"].ToString();
    private static string[] allScenes = GetAllScenePaths(sceneBase);

    //Config-based paths
    private static string path_build_dir = path_proj_root + jsonConfig["path"]["buildDir"];
    private static string path_version_file = path_proj_root + jsonConfig["path"]["versionFile"];

    //PUBLIC CALLABLE
    //-------------------------------------------------------------------------------------------------
    static void DevBuildAndRun_Win64()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();

        buildPlayerOptions.scenes = allScenes;
        buildPlayerOptions.locationPathName = GetBuildTargetPath(jsonConfig["platformExt"]["win_64"].ToString());
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
        buildPlayerOptions.options = BuildOptions.AutoRunPlayer | BuildOptions.Development;

        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }

    //PRIVATE FUNC
    //-------------------------------------------------------------------------------------------------

    //Returns the incremented path to build target as string
    private static string GetBuildTargetPath(string platformExt)
    {
        string version = (GetVersion()).ToString();
        return path_build_dir + "\\" + jsonConfig["meta"]["project"] + "-" + jsonConfig["meta"]["version"] + "-" + version + "\\" + platformExt;
    }

    //Gets the latest build version from text file, increments the version if it already exists
    private static int GetVersion()
    {
        int retVal = 0;
        if (File.Exists(path_version_file))
        {
            retVal = Int32.Parse(System.IO.File.ReadAllText(path_version_file)) + 1;
            File.Delete(path_version_file);
            CreateBuildVersionFile(retVal);
        }
        else
        {
            CreateBuildVersionFile(retVal);
        }

        return retVal;
    }

    //Creates a build version text file
    private static void CreateBuildVersionFile(int version)
    {
        using (StreamWriter sw = File.CreateText(path_version_file))
        {
            sw.WriteLine(version);
        }
    }

    //Returns a string array of all relative scene paths
    private static string[] GetAllScenePaths(string inSceneBase)
    {
        List<string> allScenePaths = new List<string>();
        for (int i = 0; i < jsonConfig["scene"]["list"].Count; i++)
        {
            allScenePaths.Add(inSceneBase + jsonConfig["scene"]["list"][i] + ".unity");
        }
        return allScenePaths.ToArray();
    }
}