using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    public int fuelAmount = 100;

    int currentFuel;

    void Start()
    {
        currentFuel = fuelAmount;
    }

    void Update()
    {
        
    }

    public int GetCurrentFuel()
    {
        return currentFuel;
    }

    public void UseFuel(int amount)
    {
        currentFuel -= amount;
    }
}
