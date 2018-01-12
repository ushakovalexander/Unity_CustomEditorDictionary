using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableUserData : ScriptableObject {
	public string userName;
	public string userId;
  public SerializableDictionary<string, int> purchases = new SerializableDictionary<string, int>();
  // public List<KeyValuePair<string, int>> kvp = new List<KeyValuePair<string, int>>();
}
