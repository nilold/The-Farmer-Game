using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disease : MonoBehaviour {

    [SerializeField] public PlantManager.DiseaseTypes type;
    [SerializeField] public List<PlantManager.PlantTypes> affectedPlantTypes;

    [SerializeField] PlantManager.EnvironmentConditions[] positiveEnvironmentFactors;
    [SerializeField] PlantManager.EnvironmentConditions[] negativeEnvironmentFactors;
    [SerializeField] float defaultAppearanceRate = 1f;

    [Range(0, 1)]
    [Tooltip("Probability of infect a target plant")]
    [SerializeField] public float infectionChance;
    [Range(0, 1)]
    [Tooltip("Probability of infect a neighbour plant")]
    [SerializeField] public float spreadChance;
    [Range(0, 1)]
    [Tooltip("Infestation level above which afflictions begin")]
    [SerializeField] public float infestationThreshold;
    [Range(0, 0.1f)]
    [Tooltip("Yield quality affliction level")]
    [SerializeField] public float qualityAffliction;
    [Range(0, 0.1f)]
    [Tooltip("Afects both plant's yield rate and max yield")]
    [SerializeField] public float yieldAffliction;
    [Range(0, 0.1f)]
    [Tooltip("Afects plant`s development rate")]
    [SerializeField] public float developmentAffliction;

    [SerializeField] public bool treatedByCropping;
    [SerializeField] public bool treatedByHarvesting;


}
