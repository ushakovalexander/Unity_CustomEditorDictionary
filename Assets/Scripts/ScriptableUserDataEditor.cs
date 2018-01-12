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

		SerializedProperty userNameProperty = SerializedUserData.FindProperty(GetMemberName(() => UserData.userName));
		SerializedProperty userIdProperty = SerializedUserData.FindProperty(GetMemberName(() => UserData.userId));
		SerializedProperty purchasesProperty = SerializedUserData.FindProperty(GetMemberName(() => UserData.purchases));

		EditorGUILayout.PropertyField(userNameProperty);
		EditorGUILayout.PropertyField(userIdProperty);

		SerializableDictionary<string, int> dict = UserData.purchases;
		foreach(KeyValuePair<string, int> kvp in dict) {
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.TextField(kvp.Key);
			EditorGUILayout.TextField(kvp.Value.ToString());
			EditorGUILayout.EndHorizontal();
		}

		if(dict.Keys.Count < 3) {
			string newKey = UnityEngine.Random.Range(0, 10).ToString();
			if(!dict.ContainsKey(newKey)) {
				int newValue = UnityEngine.Random.Range(0, 10);
				dict.Add(newKey, newValue);
			}
		}

		SerializedUserData.ApplyModifiedProperties();
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
