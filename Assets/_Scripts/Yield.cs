using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yield  {

    PlantManager.PlantTypes plantType;
    float quantity;
    float quality;
    DateTime harvestTime;

    public Yield(PlantManager.PlantTypes plantType, float quantity, float quality)
    {
        this.plantType = plantType;
        this.quantity = quantity;
        this.quality = quality;
        harvestTime = new DateTime();
    }
}
