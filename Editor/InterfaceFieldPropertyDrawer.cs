using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(InterfaceField<>))]
public class IFieldPropertyDrawer : PropertyDrawer
{
    private const string Field_Name = "assignedObject";
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var interfaceProperty = property.FindPropertyRelative(Field_Name);
        EditorGUI.PropertyField(position, interfaceProperty, label);
        if (GUI.changed)
        {
            var assignedValue = interfaceProperty.objectReferenceValue;
            if (assignedValue != null)
            {
                var interfaceType = fieldInfo.GetFieldType().GetGenericArguments()[0];
                bool isImplementingInteface = assignedValue.GetType().IsAssignableFrom(interfaceType);
                if (!isImplementingInteface)
                {
                    var components = (assignedValue as GameObject).GetComponents(interfaceType);
                    switch (components.Length)
                    {
                        case 0:
                            Debug.LogError($"There are no component implementing {interfaceType.Name} interface!");
                            interfaceProperty.objectReferenceValue = null;
                            break;
                        case 1:
                            interfaceProperty.objectReferenceValue = components[0];
                            break;
                        default:
                            Debug.LogError($"Therer are more than one component of type{interfaceType.Name}");
                            interfaceProperty.objectReferenceValue = null;
                            break;
                    }
                }
                interfaceProperty.serializedObject.ApplyModifiedProperties();
            }
        }
    }
}