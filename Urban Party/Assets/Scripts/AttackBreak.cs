using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBreak : MonoBehaviour
{

    [SerializeField] Sprite damagedSprite;
    [SerializeField] int health;
    [SerializeField] bool required;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            health--;
            if (health < 1)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = damagedSprite;
                if (required)
                {
                    LevelController.CompleteReq();
                }
            }
        }
    }
}
