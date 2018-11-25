using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infection : MonoBehaviour {

    public Plant plant;
    public Disease disease;
    PlantManager plantManager;

    // TODO: private
    public float infestation = 0.05f; // starts at 5% TODO: tune
    public Vector3Int pos;
    float timeSinceLastDamage;
    float timeSinceLastInfestation;

    void Start () {
        plantManager = FindObjectOfType<PlantManager>();
	}
	
	void Update () {
        Damage();
        Infestate();
	}

    public Vector2 GetInfectionPoint(){
        return new Vector2(plant.pos.x, plant.pos.y);
    }

    void Infestate()
    {
        timeSinceLastInfestation += Time.deltaTime;
        if (timeSinceLastInfestation < plantManager.diseaseSpreadPeriod) return;

        float protectionLevel = GetProtectionLevel();
        infestation *= (1 + disease.spreadChance - protectionLevel);

        if (infestation > 1f) infestation = 1f;

        if(infestation <= 0)
        {
            plant.infectedBy.Remove(disease.type);
            Destroy(gameObject);
        }

    }

    private float GetProtectionLevel()
    {
        float protectionLevel = 0f;
        foreach (FarmInput input in plant.appliedInputs)
        {
            if (input.targetDiseases.Contains(disease.type))
            {
                protectionLevel += input.antiDiseasePower;
            }
        }

        if (protectionLevel > 1f) protectionLevel = 1f;
        return protectionLevel;
    }

    void Damage(){
        timeSinceLastDamage += Time.deltaTime;

        if (infestation < disease.infestationThreshold) return;
        if (timeSinceLastDamage < plantManager.diseaseDamagePeriod) return;

        plant.yieldRate *= (1 - disease.yieldAffliction * infestation);
        plant.maxYield *= (1 - disease.yieldAffliction * infestation);
        plant.quality *= (1 - disease.qualityAffliction * infestation);
        plant.evolutionPeriod *= (1 - disease.developmentAffliction * infestation);

        timeSinceLastDamage = 0f;
    }
}
