using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableUserData : ScriptableObject, ISerializationCallbackReceiver {
  public string userName;
  public string userId;

  public Dictionary<string, int> purchases = new Dictionary<string, int>();

  [SerializeField]
  public List<string> purchasesKeysList = new List<string>();

  [SerializeField]
  public List<int> purchasesValuesList = new List<int>();

  public void OnBeforeSerialize() {
    purchasesKeysList.Clear();
    purchasesValuesList.Clear();
    foreach (KeyValuePair<string, int> kvp in purchases) {
      if(!purchasesKeysList.Contains(kvp.Key)) {
        purchasesKeysList.Add(kvp.Key);
        purchasesValuesList.Add(kvp.Value);
      }
    }
  }

  public void OnAfterDeserialize() {
    purchases.Clear();
    if(purchasesKeysList.Count != purchasesValuesList.Count) {
      throw new Exception("Invalid data");
    }
    for (int i = 0; i < purchasesKeysList.Count; i++) {
      if(!purchases.ContainsKey(purchasesKeysList[i])) {
        purchases.Add(purchasesKeysList[i], purchasesValuesList[i]);
      }
    }
  }

  public void AddPurchase() {
    string key = "KEY";
    if(!purchases.ContainsKey(key)) {
      purchases.Add(key, 0);
    }
  }

  public void RemovePurchase(string key) {
    if(purchases.ContainsKey(key)) {
      purchases.Remove(key);
    }
    if(purchasesKeysList.Contains(key)){
      int index = purchasesKeysList.IndexOf(key);
      purchasesKeysList.RemoveAt(index);
      purchasesValuesList.RemoveAt(index);
    }
  }
}
