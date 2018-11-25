using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    #region Singleton

    public static Inventory instance;

    public List<Item> items = new List<Item>();

    private void Awake()
    {
        if(instance != null){
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public bool Add(Item item)
    {
        //Debug.Log("Add item");
        items.Add(item);

        if (onItemChangedCallback != null){
            onItemChangedCallback.Invoke();
            //Debug.Log("Invoke callback");
            return true;
        }
            
        return false;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
