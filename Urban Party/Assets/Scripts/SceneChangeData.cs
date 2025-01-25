using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeData : MonoBehaviour
{
    public static List<(ItemType type, int index, int quantity)> slotStorage;
    public static int confetti;
    public static Vector2 lastPosition = new Vector2(-186, -0.4f);
    public static string lastScene;
    public static SceneChangeData instance;
    public int[] initAliveEnemies;
    public static int[] AliveEnimies;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
        if (slotStorage == null)
        {
            AliveEnimies = initAliveEnemies;
            slotStorage = new List<(ItemType type, int index, int quantity)>();
        }
        DontDestroyOnLoad(gameObject);
    }

    public static void StoreInList(InventoryController inventoryController)
    {
        foreach(InventorySlotController s in inventoryController.slots)
        {
            if (s.isFilled)
            {
                slotStorage.Add((s.type, s.libraryIndex, s.quantity));
            }
        }
    }

    public static void StoreInList(ItemType type, int index, int quantity)
    {
        slotStorage.Add((type, index, quantity));
        Debug.Log("Slot storage: " + slotStorage.Count);
    }

    public static void SetInventoryFromList(InventoryController inventory)
    {
        Debug.Log("Slot storage: " + slotStorage.Count);
        foreach((ItemType type, int index, int quantity) s in slotStorage)
        {
            Debug.Log("Execute");
            inventory.AddItem(s.type, s.index, s.quantity);
        }
    }
}
