using UnityEngine;

public class Destructible : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject visual;
    public GameObject triggerObject;
    [Header("Prefabs")]
    public GameObject fallingDebrisPrefab;
    public GameObject shatterPrefab;
    [Header("Internal State")]
    public bool inProgress;
    public bool isDestroyed;

    [SerializeField] bool isRequired = false;

    private static ChaosHud hud;
    private void Start()
    {
        if (hud == null)
        {
            hud = FindAnyObjectByType<ChaosHud>();
        }
        if(hud != null) { hud.destructibles.Add(this); }
        else { Debug.LogError("No Chaos Hud Detected!"); }
    }
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
        if(isRequired)
        {
            LevelController.CompleteReq();
        }

        triggerObject.SetActive(false);
        visual.SetActive(false);
        GameObject debrisObject = Instantiate(fallingDebrisPrefab, transform.position, transform.rotation);
        debrisObject.transform.localScale = transform.localScale;
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
        if (hud != null)
        {
            hud.destructibles.Remove(this);
            hud.UpdateHud();
        }
        return;
    }
}
