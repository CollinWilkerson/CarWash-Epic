using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelController
{
    public static int level = 0;
    private static int completedReq = 0;

    public static void CompleteReq()
    {
        completedReq++;
        if(level == 0 && completedReq == 1)
        {
            level++;
            completedReq = 0;
            SceneManager.LoadScene("LivingRoom");
        }
        else if(level == 1 && completedReq == 2)
        {
            level++;
            completedReq = 0;
            SceneManager.LoadScene("WashRoom");
        }
    }
}
