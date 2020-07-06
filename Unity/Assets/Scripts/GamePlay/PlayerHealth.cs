using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 100 ;
    public int currentHealth;
    public HealthBar healthBar;

    public bool isInvincible = false;
    public SpriteRenderer graphics;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            GetHeal(20);
        }

        if (isInvincible)
        {
            healthBar.setColorInvincible();
        }
        else
        {
            healthBar.setColorDefault();
        }
    }
    
    public void TakeDamage(int damage)
    {

        if (!isInvincible)
        {
            currentHealth -= damage;
            healthBar.setHealth(currentHealth);
            isInvincible = true;
            StartCoroutine(invincible());
        }
        
       // GetComponent<hPDisplayScript>().ChangeHitPoints(currentHealth);
        if (currentHealth <= 0)
        {
            FindObjectOfType<GameManager>().EndGame();
        } 
    }
    
    public IEnumerator invincible()
    {
        yield return new WaitForSeconds(0.5f);
        isInvincible = false;
    }
    
    public void GetHeal(int heal)
    {
        if (currentHealth < 90)
        {
            currentHealth += heal;
            healthBar.setHealth(currentHealth);
            //currentHealth = maxHealth;
        } 
    }
    
    
}
