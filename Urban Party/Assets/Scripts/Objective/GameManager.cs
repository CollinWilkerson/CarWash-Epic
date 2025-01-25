using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //This should control our internal game state.
    // Aka loss, win, and stuff.

    // Go!
    public static GameManager instance;
    private bool won;
    public string gameName;
    private int destructibleAmount;
    public int destructiblesToWin;
    private void Awake()
    {
        instance = this;
    }

    public List<Destructible> destructibles;
    //List<Objectives> objectives;

    public void SetTotalDestructibleAmount(int amount)
    {
        destructibleAmount = amount;
    }

    public void DoWin()
    {
        Debug.Log("We Won!");
        //implement this
        return;
    }

    public void DoLose()
    {
        if (!won)
        {
            Debug.Log("I will never forgive you, player");
        }
    }

    public void CheckWinStatus()
    {
        //Temp
        if (!won) { 
            if(destructibles.Count == 0 || (destructibleAmount - destructibles.Count < destructiblesToWin && destructiblesToWin > 0))
            {
                DoWin();
            }
        }
    }
}
