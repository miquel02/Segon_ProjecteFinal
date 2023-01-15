using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public int expWhenDefeated;

    public Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        UpdateMaxHealth(maxHealth);
    }

    // Update is called once per frame
    private void Update()
    {
      
    }

    public void DamageCharacter(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }


       // rigidBody.AddForce(transform.right * 10, ForceMode.Impulse);

    }

    public void UpdateMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
    }

}
