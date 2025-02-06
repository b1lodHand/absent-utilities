using UnityEditor;

namespace com.absence.utilities.experimental.databases.editor
{
    public abstract class EditorDatabaseInstance<T1, T2> : DatabaseInstanceBase<T1, T2> where T2 : UnityEngine.Object
    {
        protected string[] m_targetFolders;

        public EditorDatabaseInstance() : base()
        {
            m_targetFolders = null;
            Refresh();
        }

        public EditorDatabaseInstance(string[] searchInFolders) : base()
        {
            m_targetFolders = searchInFolders;
            Refresh();
        }

        public override void Refresh()
        {
            Clear();
            string[] guids = null;

            if (m_targetFolders == null) guids = AssetDatabase.FindAssets($"t:{typeof(T2).Name}");
            else guids = AssetDatabase.FindAssets($"t:{typeof(T2).Name}", m_targetFolders);

            if (guids.Length == 0)
                return;

            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                T2 assetLoaded = AssetDatabase.LoadAssetAtPath<T2>(assetPath);

                if (!TryGenerateKey(assetLoaded, out T1 key))
                    continue;

                Data.Add(key, assetLoaded);
            }
        }
    }
    public class EditorMemberDatabaseInstance<T1, T2> : EditorDatabaseInstance<T1, T2> where T2 : UnityEngine.Object, IDatabaseMember<T1>
    {
        public EditorMemberDatabaseInstance() : base()
        {
        }

        public EditorMemberDatabaseInstance(string[] searchInFolders) : base(searchInFolders)
        {
        }

        protected override bool TryGenerateKey(T2 target, out T1 output)
        {
            output = target.GetDatabaseKey();
            return true;
        }
    }
}
