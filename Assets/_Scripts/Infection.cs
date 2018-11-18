using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infection : MonoBehaviour {

    public Plant plant;
    public Disease disease;

    float infestation = 0.01f; // starts at 1%
    public Vector3Int pos;

    void Start () {
		
	}
	
	void Update () {
	}

    public Vector2 GetInfectionPoint(){
        return new Vector2(plant.pos.x, plant.pos.y);
    }

    void Damage(){

    }
}
