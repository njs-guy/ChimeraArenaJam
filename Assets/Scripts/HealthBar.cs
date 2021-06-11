using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth()
    {
        slider.value = 100;
    }

    public void setNoHealth()
    {
        slider.value = 0;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
