using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    //Script to display health on the stamina bar
    Slider staminaSlider;

    void Start()
    {
        staminaSlider = GetComponent<Slider>();
    }

 
    public void SetMaxStamina(float _maxStamina)
    {
        staminaSlider.maxValue = _maxStamina;
        staminaSlider.value = _maxStamina;
    }

    public void SetStamina(float _stamina)
    {
        staminaSlider.value = _stamina;
    }
}
