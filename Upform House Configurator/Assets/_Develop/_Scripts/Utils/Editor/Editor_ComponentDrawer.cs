
using UnityEngine;
using UnityEditor;

namespace Upform.Editor
{

    [CustomPropertyDrawer(typeof(Component), true)]
    public class Editor_ComponentDrawer : PropertyDrawer
    {
        Rect propertyFieldRect;
        Rect findButtonRect;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (fieldInfo.FieldType.IsArray)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            propertyFieldRect.x = position.x;
            propertyFieldRect.y = position.y;
            propertyFieldRect.width = position.width - 40f;
            propertyFieldRect.height = position.height;

            findButtonRect.x = position.xMax - 40f;
            findButtonRect.y = position.y;
            findButtonRect.width = 40f;
            findButtonRect.height = position.height;

            EditorGUI.PropertyField(propertyFieldRect, property, label);

            if (GUI.Button(findButtonRect, "Find"))
            {
                Component targetComponent = property.serializedObject.targetObject as Component;

                Component findComponent = targetComponent.GetComponentInChildren(fieldInfo.FieldType, true);

                if(findComponent == null)
                {
                    findComponent = targetComponent.GetComponentInParent(fieldInfo.FieldType, true);
                }

                if (findComponent == null)
                {
                    findComponent = Object.FindFirstObjectByType(fieldInfo.FieldType, FindObjectsInactive.Include) as Component;
                }

                EditorUtility.SetDirty(property.serializedObject.targetObject);
                property.objectReferenceValue = findComponent;
                EditorUtility.SetDirty(property.serializedObject.targetObject);

            }
        }
    }

}
