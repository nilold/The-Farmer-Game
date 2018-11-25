using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    new public string name = "New Item";
    public Sprite icon;

    public virtual void Use(){
        Debug.Log("using item " + name);
    }
}
