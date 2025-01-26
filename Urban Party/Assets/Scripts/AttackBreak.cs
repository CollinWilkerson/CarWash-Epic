using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBreak : MonoBehaviour
{

    [SerializeField] Sprite damagedSprite;
    [SerializeField] int health;
    [SerializeField] bool required;


    private void Start()
    {
        if (GameManager.instance != null) { GameManager.instance.objectives.Add(this); }
        else { Debug.LogError("No GameManager!"); }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            health--;
            if (health < 1)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = damagedSprite;
                GameManager.instance.objectives.Remove(this);
                /*if (required)
                {
                    LevelController.CompleteReq();
                }*/
            }
        }
    }
}
