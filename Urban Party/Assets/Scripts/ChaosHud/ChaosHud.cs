using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChaosHud : MonoBehaviour
{
    public List<Destructible> destructibles;
    public Slider chaosSlider;
    public bool activated = false;

    void StartHud()
    {
        chaosSlider.maxValue = destructibles.Count;
        chaosSlider.value = 0;

        activated = true;
    }
    private void Update()
    {
        if (!activated) { 
            StartHud(); 
        }
    }
    public void UpdateHud()
    {
        chaosSlider.value = chaosSlider.maxValue - destructibles.Count;
    }
}
