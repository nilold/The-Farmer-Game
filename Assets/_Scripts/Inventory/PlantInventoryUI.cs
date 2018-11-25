using UnityEngine;

public class PlantInventoryUI : MonoBehaviour {

    [SerializeField] Transform itemsParent;
    [SerializeField] GameObject inventoryUI;

    Inventory inventory;
    ItemSlot[] itemSlots;

   	void Start () {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        inventoryUI.SetActive(false);
        itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
    }
	
	void Update () {
        if(Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    void UpdateUI(){
        //Debug.Log("Updating UI");
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                Debug.Log("add " + inventory.items[i].name + " to " + itemSlots[i].name);
                itemSlots[i].AddItem(inventory.items[i]);
            }
            else
            {
                itemSlots[i].ClearSlot();
            }
        }
    }
}
