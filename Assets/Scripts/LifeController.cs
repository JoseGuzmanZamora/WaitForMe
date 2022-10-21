using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    public int life = 100;

    public void ReceiveDamage(int amount)
    {
        life -= amount;

        if (life < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("YOU DIED");
    }
}
