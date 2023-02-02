using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{

    Slider staminaSlider;

    // Start is called before the first frame update
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
