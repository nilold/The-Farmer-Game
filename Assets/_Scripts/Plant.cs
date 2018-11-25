using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Plant : Item
{
    // Statics 
    [SerializeField] String plantDisplayName = "Coffe";
    [SerializeField] PlantManager.PlantTypes plantType;
    [SerializeField] public float cost;

    // Prefabs
    [SerializeField] Tilemap cropTileMap;
    [SerializeField] Tile[] evolutionTiles;
    [SerializeField] Tile rottenTile;

    // Design configs
    [Tooltip("How many units yielded per second.")]
    [SerializeField] float defaultYieldRate = 1f;
    [SerializeField] float defaultMaxYield = 10f;
    [Tooltip("Number of seconds between each evolutions")]
    [SerializeField] float defaultEvolutionPeriod = 1f;

    public Vector3Int pos;

    // Internal
    int evolutionIndex;
    bool isHarvested;
    bool isEvolved;
    bool reachedMaxYield;
    float timeSinceLastEvolution;

    // Afflicted by diseases parameters  TODO: private
    public List<PlantManager.DiseaseTypes> infectedBy;
    public float yieldRate;
    public float maxYield;
    public float quality;
    public float evolutionPeriod;

    public FarmInput[] appliedInputs;

    // Yield status TODO: private
    public float yield;
    public float harvestedYield = 0f;

    //DEBUG
    //[SerializeField] Text yieldText;
    //[SerializeField] Text harvestedYieldText;

    // Use this for initialization
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        infectedBy = new List<PlantManager.DiseaseTypes>();
        InitTile();
    }
    // TODO: average game frequency may be slower, to reduce cpu usage
    void Update()
    {
        Evolve();
        Yield();

        //DEBUG
        //yieldText.text = yield.ToString();
        //harvestedYieldText.text = harvestedYield.ToString();
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
    public Yield Harvest(float lostRate = 0, bool crop = false)
    {
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
        cropTileMap = GameObject.FindGameObjectWithTag("CropsTile").GetComponent<Tilemap>();
        GridLayout gridLayout = FindObjectOfType<GridLayout>();
        pos = gridLayout.WorldToCell(transform.position);
        transform.position = gridLayout.CellToWorld(pos);

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

    public bool AttemptInfection(Disease disease, float chance)
    {
        if (infectedBy.Contains(disease.type)) return false;
        if (!disease.affectedPlantTypes.Contains(plantType)) return false;
        if (UnityEngine.Random.value > chance) return false;

        //cropTileMap.SetTile(pos, rottenTile); //actually this should be true only after total destruction
        infectedBy.Add(disease.type);
        return true;
    }


    public override void Use()
    {
        base.Use();
        GameManager.selectedItem = this;
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
