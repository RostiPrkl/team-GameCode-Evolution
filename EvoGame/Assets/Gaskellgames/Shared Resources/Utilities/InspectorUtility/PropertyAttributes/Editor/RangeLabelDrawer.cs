using UnityEditor;
using UnityEngine;

/// <summary>
/// Code created by Gaskellgames
/// </summary>

namespace Gaskellgames
{
    [CustomPropertyDrawer(typeof(RangeLabelAttribute))]
    public class RangeLabelDrawer : PropertyDrawer
    {
        private Color32 textColor = new Color32(128, 128, 128, 255);

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            RangeLabelAttribute rangeLabel = attribute as RangeLabelAttribute;
            int floatWidth = 50;
            int gap = 4;

            // calculate extra height for labels
            float height = EditorGUIUtility.singleLineHeight * 0.4f;
            Rect labelPosition = EditorGUILayout.GetControlRect(false, height);

            // draw slider
            if (fieldInfo.FieldType == typeof(int))
            {
                property.intValue = (int)EditorGUI.Slider(position, label, property.intValue, rangeLabel.min, rangeLabel.max);
            }
            else if(fieldInfo.FieldType == typeof(float))
            {
                property.floatValue = EditorGUI.Slider(position, label, property.floatValue, rangeLabel.min, rangeLabel.max);
            }
            else
            {
                return;
            }

            // draw sub labels
            labelPosition.height = EditorGUIUtility.singleLineHeight;
            labelPosition.y = position.y + (labelPosition.height * 0.75f);
            labelPosition.x += EditorGUIUtility.labelWidth;
            labelPosition.width -= EditorGUIUtility.labelWidth + floatWidth + gap;
            GUIStyle subLabelStyle = new GUIStyle();
            subLabelStyle.fontSize = 10;
            subLabelStyle.normal.textColor = textColor;
            subLabelStyle.alignment = TextAnchor.UpperLeft;
            EditorGUI.LabelField(labelPosition, rangeLabel.minLabel, subLabelStyle);
            subLabelStyle.alignment = TextAnchor.UpperRight;
            EditorGUI.LabelField(labelPosition, rangeLabel.maxLabel, subLabelStyle);

            EditorGUI.EndProperty();
        }

    } // class end
}
