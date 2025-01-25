using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace com.absence.utilities.editor
{
    /// <summary>
    /// The static class responsible for creating the default directories (with the maximum depth of 2 layers).
    /// </summary>
    public static class CreateDefaultDirectories
    {
        public static List<string> FOLDERS = new()
        {
        "Resources",
        "Textures",
        "_Scripts",
        "Physics",
        "Materials",
        "Editor",
        "Animations",
        "Misc",
        "Plugins",
        "Scriptables",
        "Audio",
        "Shaders",
        "Prefabs",
        };

        public static List<string> SUBFOLDERS = new()
        {
        "_Scripts/Editor",
        "Textures/Sprites",
        };

        public static event Action<List<string>> OnExtraFolderCreation = null;
        public static event Action<List<string>> OnExtraSubfolderCreation = null;

        [MenuItem("absencee_/absent-utilities/Create Default Directories")]
        public static void CreateDirectories()
        {
            for (int i = 0; i < FOLDERS.Count; i++)
            {
                if (AssetDatabase.IsValidFolder(Path.Combine("Assets", FOLDERS[i]))) continue;
                AssetDatabase.CreateFolder("Assets", FOLDERS[i]);
            }

            List<string> extraFolders = new();
            OnExtraFolderCreation?.Invoke(extraFolders);

            for (int i = 0; i < extraFolders.Count; i++)
            {
                if (AssetDatabase.IsValidFolder(Path.Combine("Assets", extraFolders[i]))) continue;
                AssetDatabase.CreateFolder("Assets", extraFolders[i]);
            }

            for (int i = 0; i < SUBFOLDERS.Count; i++)
            {
                if (AssetDatabase.IsValidFolder(Path.Combine("Assets", SUBFOLDERS[i]))) continue;
                string[] seperatedFolderName = SUBFOLDERS[i].Split('/');
                int depth = seperatedFolderName.Length;
                AssetDatabase.CreateFolder(Path.Combine("Assets", seperatedFolderName[depth - 2]), seperatedFolderName[depth - 1]);
            }

            List<string> extraSubfolders = new();
            OnExtraSubfolderCreation?.Invoke(extraSubfolders);

            for (int i = 0; i < extraSubfolders.Count; i++)
            {
                if (AssetDatabase.IsValidFolder(Path.Combine("Assets", extraSubfolders[i]))) continue;
                string[] seperatedFolderName = extraSubfolders[i].Split('/');
                int depth = seperatedFolderName.Length;
                AssetDatabase.CreateFolder(Path.Combine("Assets", seperatedFolderName[depth - 2]), seperatedFolderName[depth - 1]);
            }
        }
    }
}
