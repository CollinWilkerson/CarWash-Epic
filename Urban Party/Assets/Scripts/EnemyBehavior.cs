using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehavior : MonoBehaviour
{
    public int arrayIndex;

    private void Start()
    {
        if (SceneChangeData.AliveEnimies[arrayIndex] == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            SceneChangeData.lastPosition = PlayerController.instance.gameObject.transform.position;
            SceneChangeData.confetti = PlayerController.instance.confetti;
            SceneChangeData.lastScene = SceneManager.GetActiveScene().name;
            Debug.Log(PlayerController.instance.gameObject.transform.position);
            SceneChangeData.AliveEnimies[arrayIndex] = 0;
            SceneManager.LoadScene("Battle");
        }
    }
}
