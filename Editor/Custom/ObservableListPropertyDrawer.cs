using com.absence.utilities.experimental.observablelists;
using UnityEditor;
using UnityEngine;

namespace com.absence.utilities.editor
{
    [CustomPropertyDrawer(typeof(ObservableList), true)]
    public class ObservableListPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty listProp = property.FindPropertyRelative("m_internalList");

            return EditorGUI.GetPropertyHeight(listProp, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty listProp = property.FindPropertyRelative("m_internalList");

            EditorGUI.BeginProperty(position, label, property);

            EditorGUI.PropertyField(position, listProp, label, true);

            EditorGUI.EndProperty();
        }
    }
}
