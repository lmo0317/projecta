#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FIMSpace.Generating
{
    public partial class FieldVariable
    {
        public static void Editor_DrawTweakableVariable(FieldVariable toDraw)
        {
            if (toDraw == null) return;
            var v = toDraw;

            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(3);

            GUIContent cName = new GUIContent(v.Name);
            float width = EditorStyles.boldLabel.CalcSize(cName).x + 6;
            if (width > 220) width = 220;

            EditorGUILayout.LabelField(v.Name, EditorStyles.boldLabel, GUILayout.Width(width));
            GUILayout.Space(6);

            if (v.ValueType == FieldVariable.EVarType.Float)
            {
                EditorGUIUtility.labelWidth = 10;
                if (v.helper == Vector3.zero) v.Float = EditorGUILayout.FloatField(" ", v.Float);
                else v.Float = EditorGUILayout.Slider(" ", v.Float, v.helper.x, v.helper.y);
            }
            else if (v.ValueType == FieldVariable.EVarType.Bool)
            {
                EditorGUIUtility.labelWidth = 70;
                v.SetValue(EditorGUILayout.Toggle("Default:", v.GetBoolValue()));
            }
            else if (v.ValueType == FieldVariable.EVarType.Material)
            {
                EditorGUIUtility.labelWidth = 70;
                v.SetValue((Material)EditorGUILayout.ObjectField("Material:", v.GetMaterialRef(), typeof(Material), false));
            }
            else if (v.ValueType == FieldVariable.EVarType.GameObject)
            {
                EditorGUIUtility.labelWidth = 70;
                v.SetValue((GameObject)EditorGUILayout.ObjectField("Object:", v.GetGameObjRef(), typeof(GameObject), false));
            }
            else if (v.ValueType == FieldVariable.EVarType.Vector3)
            {
                EditorGUIUtility.labelWidth = 70;
                v.SetValue(EditorGUILayout.Vector3Field("", v.GetVector3Value()));
            }

            EditorGUIUtility.labelWidth = 0;
            GUILayout.Space(3);

            EditorGUILayout.EndHorizontal();
        }
    }
}
#endif