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
    [SerializeField] public float infectionChance;
    [Tooltip("Probability of infect a neighbour plant")]
    [SerializeField] public float spreadChance;
    [SerializeField] float infestationThreshold;
    [SerializeField] float qualityAffliction;
    [SerializeField] float yieldAffliction;
    [SerializeField] float developmentAffliction;

    [SerializeField] bool treatedByCropping;
    [SerializeField] bool treatedByHarvesting;


}
