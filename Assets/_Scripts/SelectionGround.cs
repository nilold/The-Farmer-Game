using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionGround : MonoBehaviour {

    //TODO: detect collision to avoid plating over other items
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with " + collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Collifing");
    }
}
