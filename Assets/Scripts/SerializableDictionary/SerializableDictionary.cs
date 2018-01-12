﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver {
	[SerializeField]
	private List<TKey> keys = new List<TKey>();

	[SerializeField]
	private List<TValue> values = new List<TValue>();

  public void OnBeforeSerialize() {
		keys.Clear();
		values.Clear();
		foreach (KeyValuePair<TKey, TValue> data in this) {
			keys.Add(data.Key);
			values.Add(data.Value);
		}
  }

  public void OnAfterDeserialize() {
		Clear();
		if(keys.Count != values.Count) {
			throw new Exception("Invalid data");
		}
		for (int i = 0; i < keys.Count; i++) {
			Add(keys[i], values[i]);
		}
  }
}
