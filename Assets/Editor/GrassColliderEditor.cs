using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(GrassManager))]
public class GrassColliderEditor : Editor
{
    private ReorderableList list;
    private SerializedProperty Intensity;
    private SerializedProperty Spec;


    private SerializedProperty SwaySpeed;
    private SerializedProperty SwayBend;
    private SerializedProperty SwayTime;
    private SerializedProperty SwayDis;

    private void OnEnable()
    {
        Intensity = serializedObject.FindProperty("GrassBendingIntensity");
        //Spec = serializedObject.FindProperty("GrassSpecularIntensity");
        SwaySpeed = serializedObject.FindProperty("GrassSwaySpeed");
        SwayBend = serializedObject.FindProperty("GrassSwayBendIntensity");
        SwayTime = serializedObject.FindProperty("GrassSwayBendTime");
        SwayDis = serializedObject.FindProperty("GrassSwayDisplacement");

        //Initialize view
        list = new ReorderableList(serializedObject,
                serializedObject.FindProperty("TerrainGrassCollider"),
                true, true, true, true);

        //Adding grass struct properties
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => 
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            
            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, 180, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("Collider"), GUIContent.none);

            EditorGUI.LabelField(new Rect(rect.x + 190, rect.y, 180, EditorGUIUtility.singleLineHeight), "> Transform");

            EditorGUI.PropertyField(
                new Rect(rect.x + 290, rect.y, 50, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("Radius"), GUIContent.none);

            EditorGUI.LabelField(new Rect(rect.x + 350, rect.y, 55, EditorGUIUtility.singleLineHeight), "> Radius");
        };

        //Little tweaks
        list.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "List of grass colliders");
        };

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(SwaySpeed);
        EditorGUILayout.PropertyField(SwayBend);
        EditorGUILayout.PropertyField(SwayTime);
        EditorGUILayout.PropertyField(SwayDis);
        EditorGUILayout.Separator();
        EditorGUILayout.Separator();

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();
        EditorGUILayout.HelpBox("(Bend Intensity and Radius may vary if your object has an unusual size)", MessageType.Info);
        //Draw updated List of Colliders
        list.DoLayoutList();
        //Draw Intensity Slider
        EditorGUILayout.PropertyField(Intensity);

        serializedObject.ApplyModifiedProperties();
    }
}
