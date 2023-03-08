using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //Script to display health on the health bar
    Slider healthSlider;

    void Start()
    {
        healthSlider = GetComponent<Slider>();
    }


    public void SetMaxHealth(float _maxHealth)
    {
        healthSlider.maxValue = _maxHealth;
        healthSlider.value = _maxHealth;
    }

    public void SetHealth(float _health)
    {
        healthSlider.value = _health;
    }
}

