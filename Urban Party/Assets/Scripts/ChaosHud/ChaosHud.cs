using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ChaosHud : MonoBehaviour
{
    public List<Destructible> destructibles;
    public Slider chaosSlider;
    public bool activated = false;
    private LevelTimer timer;
    void StartHud()
    {
        chaosSlider.maxValue = destructibles.Count;
        chaosSlider.value = 0;
        timer = GetComponent<LevelTimer>();
        timer.StartTimer();
        InformGameManager();

        activated = true;
    }
    private void Update()
    {
        if (!activated)
        {
            StartHud();
        }
    }
    public void UpdateHud()
    {
        chaosSlider.value = chaosSlider.maxValue - destructibles.Count;
        InformGameManager();
    }
    public void InformGameManager()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.destructibles = destructibles;
            GameManager.instance.CheckWinStatus();
        }
    }
}
