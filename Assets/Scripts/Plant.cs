using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Plant : MonoBehaviour
{
    // Prefabs
    [SerializeField] Tilemap cropTileMap;
    [SerializeField] Tile[] evolutionTiles;
    [SerializeField] float defaultYieldRate = 1f;
    [SerializeField] float yieldQuantity = 1f;
    [SerializeField] float maxYield = 10f;

    // Design configs
    [Tooltip("Number of seconds between each evolutions")]
    [SerializeField] float evolutionPeriod = 1f;

    // Internal (public vars are for debug purposes)
    int evolutionIndex;
    bool isEvolved;
    bool reachedMaxYield;
    float timeSinceLastEvolution;
    Vector3Int pos;
    float yieldRate;

    public float yield;
    public float harvestedYield = 0f;

    //Debug TODO: remove
    [SerializeField] Text yieldText;
    [SerializeField] Text harvestedYieldText;

    // Use this for initialization
    void Start()
    {
        yieldRate = defaultYieldRate;
        GetComponent<SpriteRenderer>().enabled = false;
        InitTile();
    }

    void Update()
    {
        Evolve();
        Yield();

        yieldText.text = yield.ToString();
        harvestedYieldText.text = harvestedYield.ToString();
    }

    private void Yield()
    {
        if (!isEvolved) return;
        if (reachedMaxYield) return;

        yield += Time.deltaTime * yieldRate;
        reachedMaxYield = yield >= maxYield;

    }

    public void buttonHarvest(float lostRate = 0)
    {
        Harvest(lostRate);
    }

    public void buttonCrop(float lostRate = 0)
    {
        Crop(lostRate);
    }

    // Get yield, returns 1 evolution stage adn resets
    public float Harvest(float lostRate = 0){
        float harvestYield = yield * (1 - lostRate);

        evolutionIndex--;
        cropTileMap.SetTile(pos, evolutionTiles[evolutionIndex]);
        Reset();
        harvestedYield = harvestYield;
        return harvestYield;
    }

    // Get yield, returns to initial evolution state nad resets
    public float Crop(float lostRate = 0)
    {
        float cropYield = yield * (1 - lostRate);

        evolutionIndex = 0;
        cropTileMap.SetTile(pos, evolutionTiles[evolutionIndex]);
        Reset();
        harvestedYield = cropYield;
        return cropYield;
    }

    private void Evolve()
    {
        if (isEvolved) return;

        timeSinceLastEvolution += Time.deltaTime;
        if (timeSinceLastEvolution < evolutionPeriod) return;

        evolutionIndex++;
        cropTileMap.SetTile(pos, evolutionTiles[evolutionIndex]);

        isEvolved = evolutionIndex == evolutionTiles.Length - 1;
        timeSinceLastEvolution = 0;

    }

    private void InitTile()
    {
        GridLayout gridLayout = FindObjectOfType<GridLayout>();
        pos = gridLayout.WorldToCell(transform.position);
        if (evolutionTiles.Length > 0)
        {
            cropTileMap.SetTile(pos, evolutionTiles[evolutionIndex]);
            Reset();
        }
        else
        {
            Debug.LogError(gameObject.name + " has no evolutionTiles!");
            isEvolved = true; //avoid trying to use any tiles
        }

    }

    private void Reset()
    {
        yield = 0f;
        timeSinceLastEvolution = 0f;
        yieldRate = defaultYieldRate;
        isEvolved = false;
    }
}
