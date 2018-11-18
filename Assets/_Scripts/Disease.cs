using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disease : MonoBehaviour {

    [SerializeField] PlantManager.DiseaseTypes type;
    [SerializeField] PlantManager.PlantTypes[] affectedPlantTypes;

    [SerializeField] float infestationThreshold;
    [SerializeField] int infestationPeriod;
    [SerializeField] float qualityAffliction;
    [SerializeField] float yieldAffliction;
    [SerializeField] float developmentAffliction;
    [SerializeField] bool treatedByCropping;
    [SerializeField] bool treatedByHarvesting;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
