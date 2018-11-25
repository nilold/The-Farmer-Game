using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmInput : MonoBehaviour {

    [SerializeField] public FarmManager.InputType inputType;

    [SerializeField] public List<PlantManager.DiseaseTypes> targetDiseases = new List<PlantManager.DiseaseTypes>();
    [Range(0, 1)] [SerializeField] public float antiDiseasePower; //insecticides and fungicides
    [Range(0, 1)] [SerializeField] public float yieldPower; // fertilizers,
    [Range(0, 1)] [SerializeField] public float evolutionPower; // fertilizers, 

}
