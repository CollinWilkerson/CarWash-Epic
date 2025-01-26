using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //This should control our internal game state.
    // Aka loss, win, and stuff.

    // Go!
    public static GameManager instance;
    public bool won;
    public string gameName;
    private int destructibleAmount;
    public int destructiblesToWin;
    private void Awake()
    {
        instance = this;
    }

    public List<Destructible> destructibles;
    public List<AttackBreak> objectives;

    private void Start()
    { }

    public void SetTotalDestructibleAmount(int amount)
    {
        destructibleAmount = amount;
    }

    public void DoWin()
    {
        LevelController.level++; 
        if (LevelController.level == 1)
        {
            SceneManager.LoadScene("LivingRoom");
        }
        else if (LevelController.level == 2)
        {
            SceneManager.LoadScene("WashRoom");
        }
        else if(LevelController.level == 3)
        {
            SceneManager.LoadScene("WinScreen");
        }
        Debug.Log("We Won!");
        won = true;
        //implement this
        return;
    }

    public void DoLose()
    {
        if (!won)
        {
            Debug.Log("I will never forgive you, player");
            SceneManager.LoadScene("LoseScreen");
        }
    }

    public void CheckWinStatus()
    {
        //Temp
        if (!won) { 
            if((destructibles.Count == 0 && objectives.Count == 0)|| (destructibleAmount - destructibles.Count < destructiblesToWin && destructiblesToWin > 0))
            {
                DoWin();
            }
        }
    }
}
