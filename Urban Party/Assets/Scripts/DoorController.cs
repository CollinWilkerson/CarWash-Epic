using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    private bool inRange = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && inRange)
        {
            SceneChangeData.lastPosition = PlayerController.instance.gameObject.transform.position;
            SceneChangeData.confetti = PlayerController.instance.confetti;
            SceneChangeData.lastScene = SceneManager.GetActiveScene().name;
            Debug.Log(PlayerController.instance.gameObject.transform.position);
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
