using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider slider;
    public void SetMaxHelth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    // Start is called before the first frame update
    public void SetHelth(int health)
    {
        slider.value = health;
    }
}
