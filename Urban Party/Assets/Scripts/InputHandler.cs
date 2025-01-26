using Unity.VisualScripting;
using UnityEngine;
public class InputHandler : MonoBehaviour
{
    [SerializeField]
    PlayerMovement player;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            player.Move(Direction.left);
            player.isInput = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            player.Move(Direction.right);
            player.isInput = true;
        }
        else
            player.isInput = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            player.TryJump();
        if (Input.GetKeyDown(KeyCode.I))
        {
            PlayerController.instance.playerInventory.gameObject.SetActive(!PlayerController.instance.playerInventory.gameObject.activeSelf);
        }
    }
}
