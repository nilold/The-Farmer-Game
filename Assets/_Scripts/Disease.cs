using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disease : MonoBehaviour {

    [SerializeField] public PlantManager.DiseaseTypes type;
    [SerializeField] public List<PlantManager.PlantTypes> affectedPlantTypes;

    [SerializeField] PlantManager.EnvironmentConditions[] positiveEnvironmentFactors;
    [SerializeField] PlantManager.EnvironmentConditions[] negativeEnvironmentFactors;
    [SerializeField] float defaultAppearanceRate = 1f;

    [Tooltip("Probability of infect a target plant")]
    [Range(0, 1)] 
    [SerializeField] public float infectionChance;
    [Tooltip("Probability of infect a neighbour plant")]
    [Range(0, 1)] 
    [SerializeField] public float spreadChance;
    [SerializeField] public float infestationThreshold;
    [SerializeField] public float qualityAffliction;
    [SerializeField] public float yieldAffliction;
    [SerializeField] public float developmentAffliction;

    [SerializeField] public bool treatedByCropping;
    [SerializeField] public bool treatedByHarvesting;


}
