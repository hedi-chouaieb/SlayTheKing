using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace SlayTheKing
{
    [CustomEditor(typeof(PropertyListener<>), true)]
    public class PropertyListenerEditor : Editor
    {
        private SerializedProperty propertyToListenProp;
        private string[] properties;

        private void OnEnable()
        {
            propertyToListenProp = serializedObject.FindProperty("propertyToListen");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // Property to Listen dropdown
            properties ??= GetMinionProperties();
            int selectedIndex = GetSelectedIndex(properties, propertyToListenProp.stringValue);
            selectedIndex = EditorGUILayout.Popup("Property to Listen", selectedIndex, properties);
            propertyToListenProp.stringValue = properties[selectedIndex];

            // Property fields using EditorGUILayout.PropertyField
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onValueChanged"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onValueChangedString"), true);

            serializedObject.ApplyModifiedProperties();
        }

        private string[] GetMinionProperties()
        {
            Type baseType = typeof(CardData);

            var derivedTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(t => t != baseType && baseType.IsAssignableFrom(t));

            List<string> propertyNames = new List<string>();
            Type targetType = target.GetType();
            Type[] genericArgs = targetType.BaseType?.GetGenericArguments();

            foreach (Type derivedType in derivedTypes)
            {
                PropertyInfo[] derivedTypeProperties = derivedType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var derivedTypeProp in derivedTypeProperties)
                {
                    Type minionType = derivedTypeProp.PropertyType;
                    PropertyInfo[] properties = minionType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                    if (genericArgs != null && genericArgs.Length > 0)
                    {
                        Type genericType = genericArgs[0];

                        foreach (var prop in properties)
                        {
                            Type propType = prop.PropertyType;
                            if (IsStatProperty(propType, genericType))
                            {
                                propertyNames.Add(prop.Name);
                            }
                        }
                    }
                }
            }

            return propertyNames.ToArray();
        }

        private bool IsStatProperty(Type propType, Type genericType)
        {
            return propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Stat<>) &&
                   propType.GetGenericArguments()[0] == genericType;
        }

        private int GetSelectedIndex(string[] properties, string selectedProperty)
        {
            return Array.IndexOf(properties, selectedProperty);
        }
    }
}
