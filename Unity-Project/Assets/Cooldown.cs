using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
public void SetCoolDownTime(float time)
    {
        if (time != 0)
        {
            slider.value = time;
        }
        if (time == 0)
        {
            slider.value = slider.maxValue;
        }
        
    }
    public void SetCoolDownTime(bool statement)
    {
        if (statement)
        {
            slider.value = 1f;
        }
        else
        {
            slider.value = 0f;
        } 

    }
    public void SetMaxCooldownTime(float maxTime)
    {
        slider.maxValue = maxTime;
    }

public void SetEnabled(bool statement)
    {
        gameObject.SetActive(statement);
    }
}
