using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantButton : MonoBehaviour
{

    public static GameObject selectedPlant;
    [SerializeField] GameObject plantPrefab;
    [SerializeField] GameObject plantsParent;
    bool isSelected;

    PlantButton[] allButtons;

    private Text costText;


    void Start()
    {
        allButtons = FindObjectsOfType<PlantButton>();
        UnSelectAll();
        FindParent();
        SetCostText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetCostText()
    {
        costText = GetComponentInChildren<Text>();
        if (costText)
        {
            costText.text = plantPrefab.GetComponent<Plant>().cost.ToString();
        }
        else
        {
            Debug.LogWarning(name + " has no costText");
        }
    }

    private void FindParent()
    {
        plantsParent = GameObject.Find("Plants");
        if (!plantsParent)
        {
            plantsParent = new GameObject("Plants");
        }
    }
    private void UnSelectAll()
    {
        if (isSelected)
        {
            Select();
        }
        else
        {
            UnSelect();
        }
    }

    public void UnSelect()
    {
        isSelected = false;
        //gameObject.GetComponent<SpriteRenderer>().color = Color.black;
    }

    public void Select()
    {
        isSelected = true;
        //gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        PlantButton.selectedPlant = plantPrefab;
    }

    private void OnMouseDown()
    {
        foreach (PlantButton button in allButtons)
        {
            button.UnSelect();
        }
        Select();

    }
}
