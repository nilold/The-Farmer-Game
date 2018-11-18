using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{

    public enum PlantTypes { Coffee, Corn };
    public enum DiseaseTypes { CoffeeBorer, CoffeeLeafMiner };
    public enum EnvironmentConditions { Heat, Humidity, Spacing };

    [SerializeField] static GameObject infectionsParent;
    [SerializeField] GameObject infectionPrefab;
    [SerializeField] Disease[] diseasesPrefabs;
    [Tooltip("Number of seconds between new infections attempts")]
    [SerializeField] float infectionPeriod = 5f;
    [Tooltip("Number of seconds between contaminations attempts")]
    [SerializeField] float diseaseSpreadPeriod = 1f;

    // Internal
    float timeSinceLastInfectionRound;
    float timeSinceLastContaminationRound;

    void Start()
    {
        FindParent();
    }

    void Update()
    {
        InfectPlants();
        SpreadDiseases();
    }

    void InfectPlants()
    {
        timeSinceLastInfectionRound += Time.deltaTime;
        if (timeSinceLastInfectionRound < infectionPeriod) return;

        foreach (Disease disease in diseasesPrefabs)
        {
            foreach (Plant plant in FindObjectsOfType<Plant>())
            {
                AtemptInfect(disease, plant, disease.infectionChance);
            }
        }

        timeSinceLastInfectionRound = 0f;
    }

    void SpreadDiseases()
    {
        timeSinceLastContaminationRound += Time.deltaTime;
        if (timeSinceLastContaminationRound < diseaseSpreadPeriod) return;

        foreach (Infection infection in FindObjectsOfType<Infection>())
        {
            Collider2D[] neighbours = Physics2D.OverlapCircleAll(infection.GetInfectionPoint(), 1);
            foreach (Collider2D neighbourCollider in neighbours)
            {
                Plant plant = neighbourCollider.gameObject.GetComponent<Plant>();
                AtemptInfect(infection.disease, plant, infection.disease.spreadChance);
            }
        }

        timeSinceLastContaminationRound = 0f;
    }

    private void AtemptInfect(Disease disease, Plant plant, float chance)
    {
        if (plant.AttemptInfection(disease, chance))
        {
            Debug.Log(string.Format("{0} infected with {1}", plant.name, disease.name));
            GameObject infection = Instantiate(infectionPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            infection.GetComponent<Infection>().plant = plant;
            infection.GetComponent<Infection>().disease = disease;
            infection.transform.parent = infectionsParent.transform;

            //disease.infectionChance = 0; // debug
        }
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
