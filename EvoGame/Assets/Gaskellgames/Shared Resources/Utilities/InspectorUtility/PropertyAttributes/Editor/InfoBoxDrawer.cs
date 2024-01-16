using UnityEditor;
using UnityEngine;

/// <summary>
/// Code created by Gaskellgames
/// </summary>

namespace Gaskellgames
{
    [CustomPropertyDrawer(typeof(InfoBoxAttribute))]
    public class InfoBoxDrawer : PropertyDrawer
    {
        private Color32 defaultColor;
        private Color32 warningTextColor = new Color32(223, 179, 000, 255);
        private Color32 errorTextColor = new Color32(223, 050, 050, 255);
        private Color32 infoTextColor = new Color32(223, 223, 223, 255);

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            InfoBoxAttribute infoBox = attribute as InfoBoxAttribute;
            defaultColor = GUI.contentColor;

            // info box [below property]
            if (infoBox.type == InfoBoxAttribute.InfoBoxType.None)
            {
                GUI.contentColor = Color.white;
                EditorGUILayout.HelpBox(infoBox.message, MessageType.None);
                GUI.contentColor = defaultColor;
            }
            else if (infoBox.type == InfoBoxAttribute.InfoBoxType.Error)
            {
                GUI.contentColor = Color.white;
                EditorGUILayout.HelpBox(infoBox.message, MessageType.Error);
                GUI.contentColor = errorTextColor;
            }
            else if (infoBox.type == InfoBoxAttribute.InfoBoxType.Warning)
            {
                GUI.contentColor = Color.white;
                EditorGUILayout.HelpBox(infoBox.message, MessageType.Warning);
                GUI.contentColor = warningTextColor;
            }
            else // infoBox.type == InfoBoxAttribute.InfoBoxType.Info
            {
                GUI.contentColor = Color.white;
                EditorGUILayout.HelpBox(infoBox.message, MessageType.Info);
                GUI.contentColor = infoTextColor;
            }

            // draw property
            Rect propertyRect = new Rect(position.x, position.y, position.width, position.height);
            EditorGUI.PropertyField(propertyRect, property, label, true);
            GUI.contentColor = defaultColor;

            EditorGUI.EndProperty();
        }
        
    } // class end
}
