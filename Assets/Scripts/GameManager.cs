using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    public StaminaManager playerStamina = new StaminaManager(100f, 100f, 30f, false);
}
