using UnityEditor;
using UnityEngine;
using System.IO;
using System.Diagnostics;

public class Menu_BuildAutomation : MonoBehaviour
{
    private static string path_current = Directory.GetCurrentDirectory();
    private static string path_proj_root = Directory.GetParent(path_current).FullName;
    private static string path_proj_ctrl = path_proj_root + "\\ctrl";

    [MenuItem("Build and Run / Win64 / Dev")]
    static public void BuildRun_Win64_Dev()
    {
        Process buildScriptProc = new Process();
        buildScriptProc.StartInfo.FileName = path_proj_ctrl + "\\buildAndRun_win64.cmd";
        buildScriptProc.StartInfo.WorkingDirectory = path_proj_ctrl;
        buildScriptProc.Start();
    }
}
