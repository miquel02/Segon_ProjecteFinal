using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaManager
{
    public float currentStamina;
    float maxStamina;
    float staminaRegenSpeed;
    bool pauseStaminaRegen = false;


    public float Stamina
    {
        get
        {
            return currentStamina;
        }
        set
        {
            currentStamina = value;
        }
    }

    public float MaxStamina
    {
        get
        {
            return maxStamina;
        }
        set
        {
            maxStamina = value;
        }
    }

    public float StaminaegenSpeed
    {
        get
        {
            return staminaRegenSpeed;
        }
        set
        {
            staminaRegenSpeed = value;
        }
    }

    public bool PauseStaminaRegen
    {
        get
        {
            return pauseStaminaRegen;
        }
        set
        {
            pauseStaminaRegen = value;
        }
    }

    // Constructor
    public StaminaManager(float _stamina, float _masxStamina, float _staminaRegenSpeed, bool _pauseStaminaRegen)
    {
        currentStamina = _stamina;
        maxStamina = _masxStamina;
        staminaRegenSpeed = _staminaRegenSpeed;
        pauseStaminaRegen = _pauseStaminaRegen;
    }

    //Methods
    public void UseStamina(float  _staminaAmount)
    {
        if(currentStamina > 0)
        {
            currentStamina -= _staminaAmount * Time.deltaTime;
        }
    }

    public void UseInstantStamina(float _staminaAmount)
    {
        if (currentStamina > 0)
        {
            currentStamina -= _staminaAmount;
        }
    }

    public void RegenStamina()
    {
        if (currentStamina < maxStamina && !pauseStaminaRegen)
        {
            currentStamina += staminaRegenSpeed * Time.deltaTime;
        }
    }
}
