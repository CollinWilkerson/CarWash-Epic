using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;  
    private float elapsedTime;        
    private bool isTimerRunning;      

    public void StartTimer()
    {
        elapsedTime = 0f;
        isTimerRunning = true;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerUI(elapsedTime);
        }
    }

    private void UpdateTimerUI(float time)
    {
        if (GameManager.instance.won)
        {
            timerText.text = "YOU WON!";
        }
        else
        {
            // Format time as minutes:seconds.milliseconds (e.g., "1:23.45")
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            int milliseconds = Mathf.FloorToInt((time * 100) % 100);
            if(minutes == 2)
            {
                GameManager.instance.DoLose();
            }

            timerText.text = $"{minutes:0}:{seconds:00}.{milliseconds:00}";
        }
        // Format time as minutes:seconds.milliseconds (e.g., "1:23.45")
    }
    public void ResetTimer()
    {
        elapsedTime = 0f;
        UpdateTimerUI(0f);
    }
}
