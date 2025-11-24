using UnityEngine;
using UnityEditor;
using System.IO;

public class ProjectStructure
{
    private static readonly string root = "Assets/_CardGame";

    private static readonly string[] folders = new string[]
    {
        "Art",
        "Art/Textures",
        "Art/Sprites",
        "Art/Models",

        "Audio",

        "Fonts",

        "Materials",

        "Prefabs",

        "Scenes",

        "Scripts",
        "Scripts/Managers",
        "Scripts/UI",

        "Settings",

        "UI",
        "UI/Images",
        "UI/Prefabs",

        "VFX"
    };

    [MenuItem("Tools/Create Project Folders")]
    public static void CreateProjectFolders()
    {
        if (!AssetDatabase.IsValidFolder(root))
            AssetDatabase.CreateFolder("Assets", "_CardGame");

        foreach (string folder in folders)
        {
            string path = $"{root}/{folder}";
            CreateFolder(path);
        }

        AssetDatabase.Refresh();
        Debug.Log("<color=green>Project folder structure created successfully!</color>");
    }

    private static void CreateFolder(string fullPath)
    {
        string parent = Path.GetDirectoryName(fullPath).Replace("\\", "/");
        string folderName = Path.GetFileName(fullPath);

        if (!AssetDatabase.IsValidFolder(fullPath))
        {
            AssetDatabase.CreateFolder(parent, folderName);
        }
    }
}
