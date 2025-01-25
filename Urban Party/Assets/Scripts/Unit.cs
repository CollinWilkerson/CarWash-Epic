using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public BattleHUD hud;

    public string unitName;

    public int damage;
    
    public int maxHP;
    public int currentHP;

    public int defense;

    private void Start()
    {

    }
    public bool TakeDamage(int amount)
    {
        currentHP -= (amount - defense);
        hud.SetHP(currentHP);
        Debug.Log($"{unitName} took damage");
        if (currentHP <= 0)
        {
            currentHP = 0;
            return true;
        }
        return false;
    }
    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }
}
