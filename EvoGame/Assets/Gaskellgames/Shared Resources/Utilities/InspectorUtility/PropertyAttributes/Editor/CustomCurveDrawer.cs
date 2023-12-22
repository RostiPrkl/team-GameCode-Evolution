using UnityEditor;
using UnityEngine;

/// <summary>
/// Code created by Gaskellgames
/// </summary>

namespace Gaskellgames
{
    [CustomPropertyDrawer(typeof(CustomCurveAttribute))]
    public class CustomCurveDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            CustomCurveAttribute customCurve = attribute as CustomCurveAttribute;
            Color32 lineColor = new Color32(customCurve.R, customCurve.G, customCurve.B, customCurve.A);

            if (property.propertyType == SerializedPropertyType.AnimationCurve)
            {
                if (!customCurve.hideLabel && customCurve.customLabel != "")
                {
                    // draw curve with custom label
                    EditorGUI.CurveField(position, property, lineColor, default, new GUIContent(customCurve.customLabel));
                }
                else if (customCurve.hideLabel && customCurve.customLabel == "")
                {
                    // draw curve with no label
                    EditorGUI.CurveField(position, property, lineColor, default, GUIContent.none);
                }
                else
                {
                    // draw curve
                    EditorGUI.CurveField(position, property, lineColor, default);
                }
            }

            EditorGUI.EndProperty();
        }

    } // class end
}
