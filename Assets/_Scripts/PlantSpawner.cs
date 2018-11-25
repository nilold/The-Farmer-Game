using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlantSpawner : MonoBehaviour {

    [SerializeField] GameObject plantsParent;
    [SerializeField] Tilemap groundTile;
    [SerializeField] GameObject selectedGround;

    GridLayout gridLayout;
    GameObject selectionImage;

    bool settingTiles;
    Vector3Int startPos;
    Vector3Int endPos;
    List<Vector3Int> selectedTiles;

    void Start () {
       gridLayout = FindObjectOfType<GridLayout>();
    }
	
	void Update () {
        if(settingTiles)
        {
            UpdateSelectedTiles();
            //TODO: verify if its not colliding with something else
            UpdateSelectionArea();
        }
    }

    private void UpdateSelectionArea()
    {
        Vector3Int mousePos = GetGridPostion();
        Vector3 newPos = new Vector3(startPos.x + (mousePos.x - startPos.x) / 2, startPos.y + (mousePos.y - startPos.y) / 2, 0);
        Vector2 selectionSize = new Vector2(mousePos.x - startPos.x, mousePos.y - startPos.y);
        selectionImage.transform.position = groundTile.WorldToCell(newPos);
        selectionImage.GetComponent<SpriteRenderer>().size = selectionSize;
    }

    private void OnMouseDown()
    {
        Plant selectedPlant = (Plant) GameManager.selectedItem;

        if(selectedPlant){
            startPos = GetGridPostion();
            selectionImage = Instantiate(selectedGround, startPos, Quaternion.identity);
            settingTiles = true;
        }
            
        //if (starDisplay.UseStars(starCost) == StarDisplay.Status.SUCCESS)
        //{

        //}
    }

    private void OnMouseUp()
    {
        if(settingTiles)
        {
            settingTiles = false;
            //TODO: Show confirm Button
            CreateSelectedPlants();
            Destroy(selectionImage);
        }

    }

    public void CreateSelectedPlants()
    {
        Plant plant = (Plant)GameManager.selectedItem;
        Quaternion zeroRot = Quaternion.identity;
        foreach (Vector3Int pos in selectedTiles)
        {
            Plant newPlant = Instantiate(plant, pos, zeroRot);
            newPlant.transform.parent = plantsParent.transform;
        }
    }

    private void UpdateSelectedTiles(){
        selectedTiles = new List<Vector3Int>();
        endPos = GetGridPostion();

        int startX = startPos.x;
        int endX = endPos.x;
        int startY = startPos.y;
        int endY = endPos.y;

        if(startX > endX){
            endX = startX;
            startX = endPos.x;
        }
        if(startY > endY){
            endY = startY;
            startY = endPos.y;
        }

        for (int i = startX; i < endX; i++){
            for (int j = startY; j < endY; j++){
                selectedTiles.Add(new Vector3Int(i, j, 0));
            }
        }
    }

    private Vector3Int GetGridPostion()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        return gridLayout.WorldToCell(worldPoint);
    }
}
