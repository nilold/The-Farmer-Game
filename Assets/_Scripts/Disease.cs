using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disease : MonoBehaviour {

    [SerializeField] public PlantManager.DiseaseTypes type;
    [SerializeField] public List<PlantManager.PlantTypes> affectedPlantTypes;

    [SerializeField] PlantManager.EnvironmentConditions[] positiveEnvironmentFactors;
    [SerializeField] PlantManager.EnvironmentConditions[] negativeEnvironmentFactors;
    [SerializeField] float defaultAppearanceRate = 1f;

    [Tooltip("Probability of infect a target plant or spread to neighbour")]
    [SerializeField] public float infectionChance;
    [SerializeField] float infestationThreshold;
    [SerializeField] float qualityAffliction;
    [SerializeField] float yieldAffliction;
    [SerializeField] float developmentAffliction;

    [SerializeField] bool treatedByCropping;
    [SerializeField] bool treatedByHarvesting;


}
