using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScriptableUserData))]
public class ScriptableUserDataEditor : Editor {

  private SerializedObject _serializedUserData;
  private ScriptableUserData _userData;

  public override void OnInspectorGUI() {
    EditorGUI.BeginChangeCheck();
    SerializedProperty userNameProperty = SerializedUserData.FindProperty(GetMemberName(() => UserData.userName));
    SerializedProperty userIdProperty = SerializedUserData.FindProperty(GetMemberName(() => UserData.userId));
    SerializedProperty purchasesKeysListProperty = SerializedUserData.FindProperty(GetMemberName(() => UserData.purchasesKeysList));
    SerializedProperty purchasesValuesListProperty = SerializedUserData.FindProperty(GetMemberName(() => UserData.purchasesValuesList));

    EditorGUILayout.PropertyField(userNameProperty);
    EditorGUILayout.PropertyField(userIdProperty);

    GUIContent label = new GUIContent();
    label.text = "Purchases count";
    EditorGUILayout.PropertyField(purchasesKeysListProperty, label);
    string toRemove = null;
    if(purchasesKeysListProperty.isExpanded) {
      for (int i = 0; i < purchasesKeysListProperty.arraySize; i++) {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("PackId", GUILayout.Width(50));
        GUILayout.FlexibleSpace();
        EditorGUILayout.PropertyField(purchasesKeysListProperty.GetArrayElementAtIndex(i), GUIContent.none, true, GUILayout.MinWidth(50));
        GUILayout.Label("Count", GUILayout.Width(50));
        GUILayout.FlexibleSpace();
        EditorGUILayout.PropertyField(purchasesValuesListProperty.GetArrayElementAtIndex(i), GUIContent.none, true, GUILayout.MinWidth(50));
        if(GUILayout.Button("-")) {
          toRemove = purchasesKeysListProperty.GetArrayElementAtIndex(i).stringValue;
        }
        EditorGUILayout.EndHorizontal();
      }
      if(GUILayout.Button("+")) {
        AddPurchase();
      }
      if(toRemove != null) {
        RemovePurchase(toRemove);
      }
    }

    if(EditorGUI.EndChangeCheck()) {
      SerializedUserData.ApplyModifiedProperties();
    }
  }

  private void RemovePurchase(string key) {
    UserData.RemovePurchase(key);
    _serializedUserData = null;
  }

  private void AddPurchase() {
    UserData.AddPurchase();
    _serializedUserData = null;
  }

  private SerializedObject SerializedUserData {
    get {
      if(_serializedUserData == null) {
        _serializedUserData = new SerializedObject(target);
      }
      return _serializedUserData;
    }
  }

  public ScriptableUserData UserData {
    get {
      if(_userData == null) {
        _userData = (ScriptableUserData) target;
      }
      return _userData;
    }
  }

  private string GetMemberName<TValue>(Expression<Func<TValue>> memberAccess) {
    return ((MemberExpression)memberAccess.Body).Member.Name;
  }
}
