using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpawner : MonoBehaviour {

    [SerializeField] GameObject plantsParent;
    GridLayout gridLayout;


    void Start () {
       gridLayout = FindObjectOfType<GridLayout>();
    }
	
	void Update () {
		
	}

    private void OnMouseDown()
    {
        Plant selectedPlant = (Plant) GameManager.selectedItem;
        if(selectedPlant){
            float starCost = selectedPlant.GetComponent<Plant>().cost;
            CreatePlants();
        }
            
        //if (starDisplay.UseStars(starCost) == StarDisplay.Status.SUCCESS)
        //{

        //}
    }

    private void CreatePlants()
    {
        Plant plant = (Plant) GameManager.selectedItem;

        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        Debug.Log(mouseX.ToString() + " - " + mouseY.ToString());

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        Vector3Int pos = gridLayout.WorldToCell(worldPoint);

        Debug.Log(pos);
        Quaternion zeroRot = Quaternion.identity;
        Plant newPlant = Instantiate(plant, pos, zeroRot);
        newPlant.transform.parent = plantsParent.transform;
        //Vector2 worldPos = SnapToGrid(CalculateWorldPointOfMouseClick());

        //GameObject newDefender = Instantiate(defender, worldPos, zeroRot);
        //newDefender.transform.parent = Button.defenderParent.transform;
    }
}
