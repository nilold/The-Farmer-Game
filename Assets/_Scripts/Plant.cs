using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Plant : MonoBehaviour
{
    // Statics 
    [SerializeField] String plantDisplayName = "Coffe";
    [SerializeField] PlantManager.PlantTypes plantType;

    // Prefabs
    [SerializeField] Tilemap cropTileMap;
    [SerializeField] Tile[] evolutionTiles;

    // Design configs
    [Tooltip("How many units yielded per second.")]
    [SerializeField] float defaultYieldRate = 1f;
    [SerializeField] float defaultMaxYield = 10f;
    [Tooltip("Number of seconds between each evolutions")]
    [SerializeField] float defaultEvolutionPeriod = 1f;

    // Internal (public vars are for debug purposes)
    int evolutionIndex;
    bool isHarvested;
    bool isEvolved;
    bool reachedMaxYield;
    float timeSinceLastEvolution;
    Vector3Int pos;

    // Afflicted by diseases
    float yieldRate;
    float maxYield;
    float quality;
    float evolutionPeriod;

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
    // TODO: average game frequency may be slower, to reduce cpu usage
    void Update()
    {
        Evolve();
        Yield();

        yieldText.text = yield.ToString();
        harvestedYieldText.text = harvestedYield.ToString();
    }

    private void Yield()
    {
        if (reachedMaxYield) yield = maxYield; // its important to be here, case the plant is afflicted by a maxYield disease
        if (!isEvolved) return;
        if (reachedMaxYield) return;

        yield += Time.deltaTime * yieldRate;
        reachedMaxYield = yield >= maxYield;
    }

    // Get yield, returns 1 evolution stage adn resets
    public Yield Harvest(float lostRate = 0, bool crop = false){
        float harvestYield = yield * (1 - lostRate);

        evolutionIndex = crop ? 0 : evolutionIndex - 1;

        cropTileMap.SetTile(pos, evolutionTiles[evolutionIndex]);
        Reset();
        harvestedYield = harvestYield;

        return new Yield(plantType, harvestYield, quality);
    }


    // Evole to next stage, if period has passed
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

    // Finds its own position on grid, set first tile and resets
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
        quality = 1;
        timeSinceLastEvolution = 0f;

        yieldRate = defaultYieldRate;
        maxYield = defaultMaxYield;
        evolutionPeriod = defaultEvolutionPeriod;

        isEvolved = false;
        reachedMaxYield = false;
    }



    // Debug functions ------------------------------
    public void buttonHarvest(float lostRate = 0)
    {
        Harvest(lostRate);
    }

    public void buttonCrop(float lostRate = 0)
    {
        Harvest(lostRate, true);
    }
}
