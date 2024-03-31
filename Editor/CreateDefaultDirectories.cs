using System.IO;
using UnityEditor;

namespace com.absence.utilities.Editor
{
    public static class CreateDefaultDirectories
    {
        private static readonly string[] FOLDERS = {
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

        private static readonly string[] SUBFOLDERS = {
        "_Scripts/Editor",
        "Textures/Sprites",
        };

        [MenuItem("absence/absent-utilities/Create Default Directories")]
        public static void CreateDirectories()
        {
            for (int i = 0; i < FOLDERS.Length; i++)
            {
                if (AssetDatabase.IsValidFolder(Path.Combine("Assets", FOLDERS[i]))) continue;
                AssetDatabase.CreateFolder("Assets", FOLDERS[i]);
            }

            for (int i = 0; i < SUBFOLDERS.Length; i++)
            {
                if (AssetDatabase.IsValidFolder(Path.Combine("Assets", SUBFOLDERS[i]))) continue;
                string[] seperatedFolderName = SUBFOLDERS[i].Split('/');
                int depth = seperatedFolderName.Length;
                AssetDatabase.CreateFolder(Path.Combine("Assets", seperatedFolderName[depth - 2]), seperatedFolderName[depth - 1]);
            }
        }
    }
}
