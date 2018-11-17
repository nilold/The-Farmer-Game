using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Plant : MonoBehaviour
{
    // Prefabs
    [SerializeField] Tilemap cropTileMap;
    [SerializeField] Tile[] evolutionTiles;

    // Design configs
    [SerializeField] float evolutionPeriod = 1f;

    // Internal
    int evolutionIndex;
    bool isEvolved;
    float timeSinceLastEvolution;
    Vector3Int pos;

    // Use this for initialization
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        InitTile();
    }

    void Update()
    {
        Evolve();
    }

    private void Evolve()
    {
        if (!isEvolved)
        {
            timeSinceLastEvolution += Time.deltaTime;
            if (timeSinceLastEvolution > evolutionPeriod)
            {
                evolutionIndex++;
                cropTileMap.SetTile(pos, evolutionTiles[evolutionIndex]);

                isEvolved = evolutionIndex == evolutionTiles.Length - 1;
                timeSinceLastEvolution = 0;
            }
        }
    }

    private void InitTile()
    {
        GridLayout gridLayout = FindObjectOfType<GridLayout>();
        pos = gridLayout.WorldToCell(transform.position);
        if (evolutionTiles.Length > 0)
        {
            cropTileMap.SetTile(pos, evolutionTiles[evolutionIndex]);
        }
        else
        {
            Debug.LogError(gameObject.name + " has no evolutionTiles!");
            isEvolved = true; //avoid trying to use any tiles
        }

    }
}
