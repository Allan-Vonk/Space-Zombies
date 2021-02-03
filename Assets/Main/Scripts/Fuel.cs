using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fuel : MonoBehaviour
{
    public int fuelAmount = 100;

    int currentFuel;
    public Image fuelbar;
    void Start()
    {
        currentFuel = fuelAmount;
    }

    public int GetCurrentFuel()
    {
        return currentFuel;
    }

    public void AddFule(int amount)
    {
        currentFuel += amount;
        currentFuel = Mathf.Clamp(currentFuel ,0, fuelAmount);
        float percentage = ((float)currentFuel / fuelAmount);
        fuelbar.DOFillAmount(percentage, 0.2f);
    }

    public void UseFuel(int amount)
    {
        currentFuel -= amount;
        currentFuel = Mathf.Clamp(currentFuel, 0, fuelAmount);

        float percentage = ((float)currentFuel/fuelAmount);
        fuelbar.DOFillAmount(percentage, 0.2f);
    }
}
