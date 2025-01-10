using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{ 
    [SerializeField]
    private Image HealthBar, StaminaBar;

    public void DisplayHealth(float Health)
    {
        Health /= 100f;
        HealthBar.fillAmount = Health;
    }

    public void DisplayStamina(float Stamina)
    {
        Stamina /= 100f;        
        StaminaBar.fillAmount = Stamina;
    }
}
