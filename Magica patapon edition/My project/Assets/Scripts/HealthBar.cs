using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void SetMaxHealth(int helth)
    {
        slider.maxValue = helth;
        slider.value = helth;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
