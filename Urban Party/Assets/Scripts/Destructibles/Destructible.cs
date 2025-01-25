using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject visual;
    public GameObject triggerObject;
    [Header("Prefabs")]
    public GameObject fallingDebrisPrefab;
    public GameObject shatterPrefab;

    public bool inProgress;
    public bool isDestroyed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (inProgress)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            inProgress = true;
            Activate();
        }
    }
    public void Activate()
    {
        triggerObject.SetActive(false);
        visual.SetActive(false);
        GameObject debrisObject = Instantiate(fallingDebrisPrefab, transform.position, transform.rotation);
        FallingDebris debris = debrisObject.GetComponent<FallingDebris>();
        debris.parent = this;
        debris.shatterPrefab = shatterPrefab;
    }

    //This is for the chaos things, after the object shatters
    //Should add chaos points
    public void DoChaos()
    {
        isDestroyed = true;
        // Implement
        return;
    }
}
