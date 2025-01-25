using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public int confetti;
    public string lastScene;
    public Vector3 lastPosition;
    public bool backToPosition = false;
    public InventoryController playerInventory;
    [SerializeField] private TextMeshProUGUI confettiText;

    private void Awake()
    {
        Debug.Log("Player Awake");
        instance = this;
    }

    private void Start()
    {
        transform.position = SceneChangeData.lastPosition;
        if (FindFirstObjectByType<VendorController>() != null)
        {
            gameObject.SetActive(false);
        }
        playerInventory.gameObject.SetActive(true);
        playerInventory.gameObject.SetActive(false);
        confetti = SceneChangeData.confetti;
        if (backToPosition)
        {
            Debug.Log("Back to position: " + lastPosition);
            transform.position = lastPosition;
        }
    }

    public void GetConfetti(int amount)
    {
        confetti += amount;
        confettiText.text = "Confetti: " + confetti;
    }
}
