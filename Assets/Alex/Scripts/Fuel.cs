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

    public void AddFule(int amount)
    {
        currentFuel += amount;
        currentFuel = Mathf.Clamp(currentFuel ,0, fuelAmount);
        print(currentFuel);
    }

    public void UseFuel(int amount)
    {
        currentFuel -= amount;
        currentFuel = Mathf.Clamp(currentFuel, 0, fuelAmount);
    }
}
