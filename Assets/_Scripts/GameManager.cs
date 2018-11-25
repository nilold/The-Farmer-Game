using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] Plant[] activePlants;
    [SerializeField] Inventory plantInventory;

    public static Item selectedItem;

	// Use this for initialization
	void Start ()
    {
        Invoke("PopulatePlantInventory", 1f);
    }

    private void PopulatePlantInventory()
    {
        foreach (Plant plant in activePlants)
        {
            plantInventory.Add(plant);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
