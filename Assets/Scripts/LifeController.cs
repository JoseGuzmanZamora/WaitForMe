using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{
    public int life = 100;
    public Text lifeText;
    public MessageManager deathNote;

    public void ReceiveDamage(int amount)
    {

        if (life <= 0)
        {
            Die();
        }
        else
        {
            life -= amount;
        }
    }

    private void Update() {
        lifeText.text = $"Life: {life.ToString()}%";
    }

    public void Die()
    {
        deathNote.ShowMe();
    }
}
