using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour {

    public enum PlantTypes { Coffee, Corn };
    public enum DiseaseTypes { CoffeeBorer, CoffeeLeafMiner };
    public enum EnvironmentConditions { Heat, Humidity, Spacing };

    [SerializeField] static GameObject infectionsParent;
    [SerializeField] GameObject infectionPrefab;
    [SerializeField] Disease[] diseasesPrefabs;
    [Tooltip("Number of seconds between every contamination attempt")]
    [SerializeField] float diseaseContaminationPeriod = 1f;

    // Internal
    float timeSinceLastInfectionRound;

    void Start () {
        FindParent();
    }
	
	void Update () {
        InfectPlants();
	}

    void InfectPlants(){
        timeSinceLastInfectionRound += Time.deltaTime;
        if (timeSinceLastInfectionRound < diseaseContaminationPeriod) return;

        foreach(Disease disease in diseasesPrefabs)
        {
            foreach(Plant plant in FindObjectsOfType<Plant>())
            {
                if (plant.AttemptInfection(disease))
                {
                    Debug.Log(string.Format("{0} infected with {1}", plant.name, disease.name));
                    GameObject infection = Instantiate(infectionPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    infection.GetComponent<Infection>().plant = plant;
                    infection.GetComponent<Infection>().disease = disease;
                    infection.transform.parent = infectionsParent.transform;
                }
            }
        }

        timeSinceLastInfectionRound = 0f;
    }

    private void FindParent()
    {
        infectionsParent = GameObject.Find("Infections");
        if (!infectionsParent)
        {
            infectionsParent = new GameObject("Infections");
        }
    }
}
