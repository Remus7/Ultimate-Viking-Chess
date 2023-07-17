using UnityEngine;
using UnityEditor;

// Author: Remus Rughinis
// Credit: Stephen Domenici (https://www.youtube.com/watch?v=mxqD1B2e4ME)

// A generalized unity editor plugin to display MapStruct data types with varied sizes

[CustomPropertyDrawer(typeof(MapStruct))]
public class EditorMap : PropertyDrawer
{
    // Render the input fields in the Unity Editor
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PrefixLabel(position, label);

        Rect newPosition = position;
        newPosition.y += 25f;
        SerializedProperty rows = property.FindPropertyRelative("rows");

        // Get all rows
        for(int i = 0; i < rows.arraySize; i ++){
            SerializedProperty row = rows.GetArrayElementAtIndex(i).FindPropertyRelative("row");
            // Size of input box for element
            newPosition.height = 20;
            newPosition.width = 20;

            // Get all elements in current row
            for(int j = 0; j < row.arraySize; j ++){
                EditorGUI.PropertyField(newPosition, row.GetArrayElementAtIndex(j), GUIContent.none);
                newPosition.x += newPosition.width;
            }

            // Padding for the input boxes on the next row
            newPosition.x = position.x;
            newPosition.y += 20;
        }
    }

    // How large the display box is expected to be
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label){
        return 20 * (property.FindPropertyRelative("rows").arraySize + 2f);
    }
}
